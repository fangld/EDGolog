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

namespace Planning
{
    public class Server
    {
        #region Fields
        
        private Socket _socket;

        private int _backLog;

        private int _port;

        private DomainLoader _domainLoader;

        private ProblemLoader _problemLoader;

        private Dictionary<string, List<string>> _typeObjectsMap;

        private Dictionary<string, bool> _predBooleanMap;

        private List<Client> _clientList;

        private Dictionary<string, Client> _agentClientDict;

        #endregion

        #region Events

        public event EventHandler<Client> NewClient;

        #endregion

        #region Constructors

        public Server(int port, int backlog, DomainLoader domainLoader, ProblemLoader problemLoader)
        {
            _socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.IP);
            _port = port;
            _backLog = backlog;
            _domainLoader = domainLoader;
            _problemLoader = problemLoader;
            Initial();
        }

        private void Initial()
        {
            _predBooleanMap = new Dictionary<string, bool>();
            _typeObjectsMap = new Dictionary<string, List<string>>();
            _agentClientDict = new Dictionary<string, Client>();

            foreach (var type in _domainLoader.TypeList)
            {
                List<string> objectNames = new List<string>();
                foreach (var pair in _problemLoader.ConstantTypeMap)
                {
                    if (pair.Value == type || pair.Value == VariableContainer.DefaultType)
                    {
                        objectNames.Add(pair.Key);
                    }
                }
                _typeObjectsMap.Add(type, objectNames);
            }

            int offset = 0;
            foreach (var pred in _domainLoader.PredicateDict.Values)
            {
                List<List<string>> collection = new List<List<string>>();

                for (int j = 0; j < pred.Count; j++)
                {
                    Tuple<string, string> variable = pred.VariableList[j];
                    List<string> objectList = _typeObjectsMap[variable.Item2];
                    collection.Add(objectList);
                }

                ScanMixedRadix(ref offset, pred.Name, collection, AddToPredIndexMap);
            }
        }

        private void ScanMixedRadix<T>(ref int offset, string predName, List<List<T>> collection, Action<int, T, T[]> action)
        {
            int count = collection.Count;
            T[] scanArray = new T[count];
            int[] index = new int[count];
            int[] maxIndex = new int[count];
            for (int i = 0; i < count; i++)
            {
                maxIndex[i] = collection[i].Count;
            }

            do
            {
                for (int i = 0; i < count; i++)
                {
                    scanArray[i] = collection[i][index[i]];
                }
                AddToPredIndexMap(offset, predName, scanArray);
                offset++;
                int j = count - 1;
                while (j != -1)
                {
                    if (index[j] == maxIndex[j] - 1)
                    {
                        index[j] = 0;
                        j--;
                        continue;
                    }
                    break;
                }
                if (j == -1)
                    return;
                index[j]++;
            } while (true);
        }

        private void AddToPredIndexMap<T>(int index, string predName, T[] array)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("{0}(", predName);
            for (int i = 0; i < array.Length - 1; i++)
            {
                sb.AppendFormat("{0},", array[i]);
            }
            sb.AppendFormat("{0})", array[array.Length - 1]);
            string gndPred = sb.ToString();
            if (_problemLoader.TruePredSet.Contains(gndPred))
            {
                _predBooleanMap.Add(gndPred, true);
            }
            else
            {
                _predBooleanMap.Add(gndPred, false);
            }
        }

        #endregion

        #region Methods

        public void ShowInfo()
        {
            _domainLoader.ShowInfo();

            _problemLoader.ShowInfo();

            //Console.WriteLine("Ground predicates:");

            //foreach (KeyValuePair<string, int> pair in _predIndexMap)
            //{
            //    Console.WriteLine("  Name: {0}, Index: {1}, Value: {2}", pair.Key, pair.Value, _predBooleanMap[pair.Key]);
            //}
        }

        public void Run()
        {
            ShowKnowledgeBase();
            Listen();
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
                Client client = new Client(newSocket, _domainLoader, _problemLoader);
                client.Handshake();
                _agentClientDict.Add(client.Name, client);
                Console.WriteLine("Agent {0} connected!", client.Name);

                i++;
            } while (i < _problemLoader.AgentList.Count);

            Console.WriteLine("All agents connected!");
        }

        public void ReceiveActions()
        {
            do
            {
                foreach (var agent in _problemLoader.AgentList)
                {
                    GroundAction gndAction = _agentClientDict[agent].GetAction();
                    Console.WriteLine(gndAction);
                    List<Tuple<Ground<Predicate>, bool>> gndLiteralList = new List<Tuple<Ground<Predicate>, bool>>();
                    CUDDNode kbNode = GetKnowledgeBase();
                    CUDDNode preconditionNode = CUDD.Function.Implies(kbNode, gndAction.Precondition);
                    
                    Console.WriteLine("  Precondition value:{0}", preconditionNode.GetValue());
                    CUDD.Print.PrintMinterm(kbNode);


                    if (preconditionNode.GetValue() > 0.5)
                    {
                        foreach (var cEffect in gndAction.Effect)
                        {
                            CUDDNode impliesNode = CUDD.Function.Implies(kbNode, cEffect.Item1);
                            if (impliesNode.GetValue() > 0.5)
                            {
                                gndLiteralList.AddRange(cEffect.Item2);
                            }
                        }
                        UpdateKnowledgeBase(gndLiteralList);
                    }
                    else
                    {
                        Console.WriteLine("    Action {0} is not executable now!", gndAction);
                    }
                    CUDD.Deref(kbNode);
                    ShowKnowledgeBase();
                }
            } while (true);
        }

        private CUDDNode GetKnowledgeBase()
        {
            List<CUDDNode> literalNodes = new List<CUDDNode>();

            foreach (var gndPred in _predBooleanMap)
            {
                string name = gndPred.Key;
                int index = _problemLoader.PreviousGroundPredicateDict[name].CuddIndex;
                CUDDNode node;

                if (gndPred.Value)
                {
                    node = CUDD.Var(index);
                }
                else
                {
                    node = CUDD.Function.Not(CUDD.Var(index));
                }
                literalNodes.Add(node);
            }

            CUDDNode result = literalNodes[0];
            for (int i = 1; i < literalNodes.Count; i++)
            {
                CUDDNode literalNode = literalNodes[i];
                CUDDNode andNode = CUDD.Function.And(result, literalNode);
                CUDD.Ref(andNode);
                CUDD.Deref(result);
                CUDD.Deref(literalNode);
                result = andNode;
            }
            return result;
        }

        private void UpdateKnowledgeBase(List<Tuple<Ground<Predicate>, bool>> gndLiteralList)
        {
            foreach (var literal in gndLiteralList)
            {
                string gndPredName = literal.Item1.ToString();
                _predBooleanMap[gndPredName] = literal.Item2;
                //Console.WriteLine("  Predicate:{0}, Value:{1}", gndPredName, _predBooleanMap[gndPredName]);
            }
        }

        private void ShowKnowledgeBase()
        {
            Console.WriteLine("  Knowledge base:");
            foreach (var pair in _predBooleanMap)
            {
                Console.WriteLine("    Predicate name:{0}, Value:{1}", pair.Key, pair.Value);
            }
        }

        #endregion
    }
}
