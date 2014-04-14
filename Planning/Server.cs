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

namespace Planning
{
    public class Server
    {
        #region Fields
        
        private Socket _socket;

        private int _listenBackLog;

        private int _port;

        private DomainLoader _domainLoader;

        private ProblemLoader _problemLoader;

        private Dictionary<string, int> _predIndexMap;

        private Dictionary<string, List<string>> _typeObjectsMap;

        private Dictionary<string, bool> _predBooleanMap;

        private List<Client> _clientList;

        private Dictionary<string, Client> _agentClientDict;

        #endregion

        #region Events

        public event EventHandler<Client> NewClient;

        #endregion

        #region Constructors

        public Server(int port, int listenBacklog, DomainLoader domainLoader, ProblemLoader problemLoader)
        {
            _socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.IP);
            _port = port;
            _listenBackLog = listenBacklog;
            _domainLoader = domainLoader;
            _problemLoader = problemLoader;
            Initial();
        }

        private void Initial()
        {
            _predIndexMap = new Dictionary<string, int>();
            _predBooleanMap = new Dictionary<string, bool>();
            _typeObjectsMap = new Dictionary<string, List<string>>();
            _agentClientDict = new Dictionary<string, Client>();

            foreach (var type in _domainLoader.ListType)
            {
                List<string> objectNames = new List<string>();
                foreach (var pair in _problemLoader.ObjectNameTypeMapMap)
                {
                    if (pair.Value == type || pair.Value == DomainLoader.DefaultType)
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
                    Tuple<string, string> variable = pred.VariableTypeList[j];
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
            _predIndexMap.Add(gndPred, index);
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

            Console.WriteLine("Ground predicates:");

            foreach (KeyValuePair<string, int> pair in _predIndexMap)
            {
                Console.WriteLine("  Name: {0}, Index: {1}, Value: {2}", pair.Key, pair.Value, _predBooleanMap[pair.Key]);
            }
        }

        public void Run()
        {
            Listen();
            ReceiveActions();
        }

        public void Listen()
        {
            IPEndPoint localEndPoint = new IPEndPoint(IPAddress.Any, _port);
            _socket.Bind(localEndPoint);
            _socket.Listen(_listenBackLog);

            int i = 0;
            do
            {
                Socket newSocket = _socket.Accept();
                Client client = new Client(newSocket, _domainLoader.PredicateDict, _domainLoader.ActionDict);
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
                    Ground<Action> gndAction = _agentClientDict[agent].GetAction();
                    Console.WriteLine(gndAction);
                }
            } while (true);
        }

        #endregion
    }
}
