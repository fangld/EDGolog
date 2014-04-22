﻿using System;
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
using ObjectWorlds.Planning;
using PAT.Common.Classes.CUDDLib;

namespace ObjectWorlds.Network
{
    public class Server
    {
        #region Fields
        
        private Socket _socket;

        private int _backLog;

        private int _port;

        private Problem _problem;

        private ObjectBase _objectBase;
        
        private Dictionary<string, Client> _agentClientDict;

        #endregion

        #region Events

        public event EventHandler<Client> NewClient;

        #endregion

        #region Constructors

        public Server(int port, int backlog, Problem problem)
        {
            _socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.IP);
            _port = port;
            _backLog = backlog;
            _objectBase = new ObjectBase(problem);
            _problem = problem;
            _agentClientDict = new Dictionary<string, Client>();
        }

        #endregion

        #region Methods

        public void Run()
        {
            _objectBase.ShowInfo();
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
                Client client = new Client(newSocket, _problem);
                client.Handshake();
                _agentClientDict.Add(client.Name, client);
                Console.WriteLine("Agent {0} connected!", client.Name);

                i++;
            } while (i < _problem.AgentList.Count);

            Console.WriteLine("All agents connected!");
        }

        public void ReceiveActions()
        {
            do
            {
                foreach (var agent in _problem.AgentList)
                {
                    GroundAction gndAction = _agentClientDict[agent].GetAction();
                    Console.WriteLine(gndAction);
                    _objectBase.Update(gndAction);
                    _objectBase.ShowInfo();
                }
            } while (true);
        }

        #endregion
    }
}