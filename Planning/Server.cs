using System;
using System.Collections.Generic;
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

        private bool _running;

        private int _listenBackLog;

        private int _port;

        #endregion

        #region Events

        public event EventHandler<Client> NewClient;

        public event EventHandler<string> ListenFail;

        #endregion

        #region Constructors

        public Server(int port, int listenBacklog)
        {
            _socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.IP);
            _running = false;
            _port = port;
            _listenBackLog = listenBacklog;
        }

        #endregion

        #region Methods

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

        #endregion
    }
}
