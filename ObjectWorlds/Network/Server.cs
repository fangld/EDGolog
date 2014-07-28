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

        private ObjectBase _objectBase;

        private Dictionary<string, Client> _agentClientDict;

        #endregion

        #region Events

        public event EventHandler<Client> NewClient;

        #endregion

        #region Constructors

        public Server(int port, int backlog, ServerProblem problem)
        {
            _socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.IP);
            _port = port;
            _backLog = backlog;
            _objectBase = new ObjectBase(problem);
            _objectBase.ObjectBaseChanged += ShowObjectBase;
            _problem = problem;
            _agentClientDict = new Dictionary<string, Client>();
        }

        #endregion

        #region Methods

        public void Run()
        {
            _objectBase.ShowInfo();
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
                foreach (var agentName in _problem.AgentList)
                {
                    Action action = _agentClientDict[agentName].GetAction();
                    Console.WriteLine(action);
                    Response response = _objectBase.GetActualResponse(action);
                    _agentClientDict[agentName].SendMessage(response.FullName);
                    Event e = _objectBase.GetActualEvent(response);

                    foreach (var otherAgentName in _problem.AgentList)
                    {
                        if (otherAgentName != agentName)
                        {
                            Agent otherAgent = _problem.AgentDict[otherAgentName];
                            Console.WriteLine("agent name: {0}, observation list count: {1}", otherAgentName, otherAgent.ObservationList.Count);
                            foreach (var observation in e.ObservationList)
                            {
                                Console.WriteLine("Event's observation: {0}", observation.FullName);
                            }

                            foreach (var observation in otherAgent.ObservationList)
                            {
                                Console.WriteLine("Other agent observation: {0}", observation.FullName);
                                if (e.ObservationList.Any(obs => obs == observation))
                                {
                                    CUDDNode obsPreNode = observation.Precondition;
                                    CUDDNode objectBaseNode = _objectBase.CurrentCuddNode;
                                    CUDD.Ref(obsPreNode);
                                    CUDD.Ref(objectBaseNode);
                                    CUDDNode impliesNode = CUDD.Function.Implies(objectBaseNode, obsPreNode);
                                    if (impliesNode.Equals(CUDD.ONE))
                                    {
                                        _agentClientDict[otherAgentName].SendMessage(observation.FullName);
                                        break;
                                    }
                                }
                            }
                            
                        }
                    }

                    _objectBase.Update(e);
                    //_objectBase.ShowInfo();
                }
            } while (true);
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