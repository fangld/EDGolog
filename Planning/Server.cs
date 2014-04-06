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

        private bool[] _booleans;

        private Socket _socket;

        private bool _running;

        private int _listenBackLog;

        private int _port;

        private DomainLoader _domainLoader;

        private ProblemLoader _problemLoader;

        private Dictionary<string, int> _predIndexMap;

        private Dictionary<string, List<string>> _typeObjectsMap;

        private Dictionary<string, bool> _predBooleanMap;

        private List<Client> _listClient; 

        #endregion

        #region Events

        public event EventHandler<Client> NewClient;

        public event EventHandler<string> ListenFail;

        #endregion

        #region Constructors

        public Server(int port, int listenBacklog, DomainLoader domainLoader, ProblemLoader problemLoader)
        {
            _socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.IP);
            _running = false;
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
            foreach (var predicate in _domainLoader.PredicateDict.Values)
            {
                List<List<string>> collection = new List<List<string>>();

                for (int j = 0; j < predicate.Count; j++)
                {
                    string type = predicate.ListVariablesType[j];
                    List<string> objectList = _typeObjectsMap[type];
                    collection.Add(objectList);
                }

                ScanMixedRadix(ref offset, predicate.Name, collection, AddToPredIndexMap);
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

        /// <summary>
        /// Listen
        /// </summary>
        public async void Listen()
        {
            lock (this)
            {
                _running = true;
                IPEndPoint localEndPoint = new IPEndPoint(IPAddress.Any, _port);
                _socket.Bind(localEndPoint);
                _socket.Listen(_listenBackLog);
                SocketAsyncEventArgs acceptEventArg = new SocketAsyncEventArgs();
                acceptEventArg.Completed += acceptEventArg_Completed;
                if (!_socket.AcceptAsync(acceptEventArg))
                {
                    acceptEventArg_Completed(this, acceptEventArg);
                }
            }
        }

        public async void Stop()
        {
            lock (this)
            {
                _running = false;
                _socket.Dispose();
            }
        }

        void acceptEventArg_Completed(object sender, SocketAsyncEventArgs e)
        {
            if (_running)
            {
                if (e.SocketError == SocketError.Success)
                {
                    Client client = new Client(e.AcceptSocket);
                    client.ActionOccur += client_ActionOccur;
                    _listClient.Add(client);
                    NewClient(this, client);
                }
                else
                {
                    ListenFail(this, e.SocketError.ToString());
                }

                SocketAsyncEventArgs acceptEventArg = new SocketAsyncEventArgs();
                acceptEventArg.Completed += acceptEventArg_Completed;
                if (!_socket.AcceptAsync(acceptEventArg))
                {
                    acceptEventArg_Completed(this, acceptEventArg);
                }
            }
        }

        void client_ActionOccur(object sender, GroundAction e)
        {
            Console.WriteLine(e);
        }

        #endregion
    }
}
