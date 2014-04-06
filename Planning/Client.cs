using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace Planning
{
    public class Client
    {
        #region Fields

        //private const int Count = 1024;

        private const int LengthBufferSize = 4;

        /// <summary>
        /// The socket of peer
        /// </summary>
        private Socket _socket;

        /// <summary>
        /// The buffer that received
        /// </summary>
        private byte[] _rcvBuffer;

        /// <summary>
        /// The buffer to send
        /// </summary>
        private byte[] _sndBuffer;

        private Timer _timer;

        private Dictionary<string, Predicate> _predDict;

        private Dictionary<string, Action> _actionDict;

        #endregion

        #region Properties

        /// <summary>
        /// The host address of peer
        /// </summary>
        public string Host { get; set; }

        /// <summary>
        /// The port of peer
        /// </summary>
        public int Port { get; set; }

        /// <summary>
        /// The flag that represents whether peer is connected.
        /// </summary>
        public bool IsConnected { get; private set; }

        #endregion

        #region Events

        public event EventHandler<GroundAction> ActionOccur;

        #endregion

        #region Constructors

        /// <summary>
        /// Create new connected peer with socket
        /// </summary>
        public Client(Socket socket, Dictionary<string, Predicate> predDict, Dictionary<string, Action> actionDict)
        {
            _socket = socket;
            IPEndPoint ipEndPoint = (IPEndPoint) socket.RemoteEndPoint;
            Host = ipEndPoint.Address.ToString();
            Port = ipEndPoint.Port;
            IsConnected = true;
            _predDict = predDict;
            _actionDict = actionDict;
            //_rcvBuffer = new byte[Count];
            //_sndBuffer = new byte[Count];
        }

        #endregion

        #region Methods

        public void Handshake()
        {
            
        }

        public void SendMessage()
        {
            
        }

        public void GetMessage()
        {
            byte[] lengthBuffer = new byte[LengthBufferSize];
            int offset = 0;
            do
            {
                int rcvCount = _socket.Receive(lengthBuffer, offset, LengthBufferSize, SocketFlags.None);
                offset += rcvCount;
            } while (offset < 4);

            int contentBufferSize = GetLength(lengthBuffer);
            byte[] contentBuffer = new byte[contentBufferSize];
            offset = 0;
            do
            {
                int rcvCount = _socket.Receive(contentBuffer, offset, contentBufferSize, SocketFlags.None);
                offset += rcvCount;
            } while (offset < contentBufferSize);

            int index = Array.FindIndex(contentBuffer, b => b == (byte) ':');
            char[] actionNameChars = new char[index];
            Parallel.For(0, index, i => actionNameChars[i] = (char) contentBuffer[i]);
            string actionName = new string(actionNameChars);

            Action action = _actionDict[actionName];

            List<string> parmList = new List<string>();

            //int firstParmIndex = index + 1;
            int parmIndex = Array.FindIndex(contentBuffer, index + 1, b => b == (byte) ',');
            while (parmIndex != -1)
            {
                parmIndex = Array.FindIndex(contentBuffer, parmIndex + 1, b => b == (byte)',');
            }

            GroundAction result = new GroundAction();

            char[] chars = new char[contentBufferSize];
            Parallel.For(0, contentBufferSize, i => chars[i] = (char) contentBuffer[i]);
        }

        private void SetBytesLength(byte[] bytes, int length)
        {
            bytes[0] = (byte)(length >> 24);
            bytes[1] = (byte)(length >> 16);
            bytes[2] = (byte)(length >> 8);
            bytes[3] = (byte)(length);
        }

        private int GetLength(byte[] bytes)
        {
            int result = 0;
            result |= (bytes[0] << 24);
            result |= (bytes[1] << 16);
            result |= (bytes[2] << 8);
            result |= (bytes[3]);
            return result;
        }

        #endregion
    }
}
