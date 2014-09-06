using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using Antlr4.Runtime;
using Antlr4.Runtime.Tree;
using LanguageRecognition;
using PAT.Common.Classes.CUDDLib;
using Planning;
using Planning.Servers;
using Action = Planning.Action;

namespace ObjectWorlds.Network
{
    public class Server
    {
        #region Fields

        private Socket _socket;

        private int _backLog;

        private int _port;

        private ServerProblem _problem;

        private Random random;

        //private ObjectBase _objectBase;

        private Dictionary<string, Client> _agentClientDict;

        #endregion

        #region Properties

        public ObjectBase ObjectBase { get; set; }

        #endregion

        #region Events

        public event
            EventHandler<Tuple<IReadOnlyDictionary<string, bool>, string, Action, Response, Observation, Event>>
            ObjectBaseChanged;

        public event EventHandler<ObjectBase> Completed;

        #endregion

        #region Constructors

        public Server(int port, int backlog, ServerProblem problem)
        {
            _socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.IP);
            _port = port;
            _backLog = backlog;
            ObjectBase = new ObjectBase(problem);
            ObjectBase.Changed += ShowObjectBase;
            //ObjectBase.Changed += (sender, tuple) => ObjectBaseChanged(sender, tuple);
            _problem = problem;
            _agentClientDict = new Dictionary<string, Client>();
            random = new Random();
        }

        #endregion

        #region Methods

        public void Run()
        {
            ObjectBase.ShowInfo();
            Console.WriteLine("Wait for an agent");
            Listen();
            Console.WriteLine("Connect 2 agents");
            ReceiveActions();
        }

        public void Listen()
        {
            IPEndPoint localEndPoint = new IPEndPoint(IPAddress.Any, _port);
            _socket.Bind(localEndPoint);
            _socket.Listen(_backLog);

            int i = 0;
            do
            {
                Socket newSocket = _socket.Accept();
                Client client = new Client(newSocket, _problem);
                client.Handshake();
                _agentClientDict.Add(client.AgentId, client);
                Console.WriteLine("Agent {0} connected!", client.AgentId);

                i++;
            } while (i < _problem.AgentList.Count);

            Console.WriteLine("All agents connected!");
        }

        public void ReceiveActions()
        {
            do
            {
                //Console.ReadLine();

                Client client = SelectAnClient();
                if (client == null)
                {
                    if (Completed != null)
                    {
                        Completed(this, null);
                    }
                    break;
                }
                Console.WriteLine("Select agent: {0}", client.AgentId);
                client.SendMessage("action");
                Action action = client.GetAction();
                Response response = ObjectBase.GetActualResponse(action);
                client.SendMessage(response.FullName);
                string message = client.ReceiveMessage();
                Console.WriteLine("receive message: {0}", message);
                if (message == "quit")
                {
                    Console.WriteLine("Agent {0} is terminated", client.AgentId);
                    client.IsTerminated = true;
                }

                string agentName = client.AgentId;
                Event e = ObjectBase.GetActualEvent(response);
                Tuple<IReadOnlyDictionary<string, bool>, string, Action, Response, Observation, Event> tuple = null;
                foreach (var otherAgentName in _problem.AgentList)
                {
                    if (otherAgentName != agentName)
                    {
                        Agent otherAgent = _problem.AgentDict[otherAgentName];
                        Client otherClient = _agentClientDict[otherAgentName];
                        if (!otherClient.IsTerminated)
                        {
                            foreach (var observation in otherAgent.ObservationList)
                            {
                                if (e.ObservationList.Any(obs => obs == observation))
                                {
                                    CUDDNode objectBaseNode = ObjectBase.CurrentCuddNode;
                                    CUDD.Ref(objectBaseNode);
                                    CUDDNode obsPreNode = observation.Precondition;
                                    CUDD.Ref(obsPreNode);
                                    CUDDNode impliesNode = CUDD.Function.Implies(objectBaseNode, obsPreNode);
                                    if (impliesNode.Equals(CUDD.ONE))
                                    {
                                        Console.WriteLine("Send observation {0} to agent {1}.", observation,
                                            otherAgentName);
                                        tuple =
                                            new Tuple
                                                <IReadOnlyDictionary<string, bool>, string, Action, Response,
                                                    Observation, Event
                                                    >(ObjectBase.PredBooleanMap, agentName, action, response,
                                                        observation, e);
                                        otherClient.SendMessage("observation");
                                        otherClient.SendMessage(observation.FullName);
                                        break;
                                    }
                                }
                            }
                        }
                    }
                }

                ObjectBase.Update(e);
                if (ObjectBaseChanged != null)
                {
                    ObjectBaseChanged(this, tuple);
                }
            } while (true);
        }

        private Client SelectAnClient()
        {
            Client result = null;
            List<Client> clientList = new List<Client>();
            foreach (var client in _agentClientDict.Values)
            {
                if (!client.IsTerminated)
                {
                    clientList.Add(client);
                }
            }

            //Console.WriteLine("Remaining agents' number: {0}", clientList.Count);

            if (clientList.Count != 0)
            {
                int index = random.Next(clientList.Count);
                result = clientList[index];
            }
            return result;
        }

        private void ShowObjectBase(object sender, Tuple<IReadOnlyDictionary<string, bool>, Event> tuple)
        {
            Console.WriteLine("Object base:");
            foreach (var pair in tuple.Item1)
            {
                Console.WriteLine("  Predicate: {0}, Value: {1}", pair.Key, pair.Value);
            }

            Console.WriteLine("Executed event: {0}", tuple.Item2);
        }

        #endregion
    }
}