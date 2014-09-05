using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using Planning;
//using Planning.Servers;
using Planning.Servers;
using Action =Planning.Action;

namespace ObjectWorlds.Network
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

        //private IReadOnlyDictionary<string, Predicate> _predDict;

        //private IReadOnlyDictionary<string, Action> _actionDict;

        private IReadOnlyDictionary<string, Action> _actionDict;

        #endregion

        #region Properties

        public string AgentId { get; set; }

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

        public bool IsTerminated { get; private set; }

        #endregion

        #region Constructors

        /// <summary>
        /// Create new connected peer with socket
        /// </summary>
        public Client(Socket socket, ServerProblem problem)
        {
            _socket = socket;
            IPEndPoint ipEndPoint = (IPEndPoint)socket.RemoteEndPoint;
            Host = ipEndPoint.Address.ToString();
            Port = ipEndPoint.Port;
            IsConnected = true;
            _actionDict = problem.ActionDict;
        }

        public Client(ServerProblem problem, string agentId)
        {
            AgentId = agentId;
            _actionDict = problem.ActionDict;
            IsConnected = false;
        }

        #endregion

        #region Methods

        public void Handshake()
        {
            byte[] contentBuffer = ReceiveBuffer();
            AgentId = BytesToString(contentBuffer);
        }

        public void SendMessage(string message)
        {
            byte[] lengthBuffer = new byte[4];
            byte[] contentBuffer = StringToBytes(message);
            SetBytesLength(lengthBuffer, contentBuffer.Length);
            _socket.Send(lengthBuffer);
            _socket.Send(contentBuffer);
        }

        public Action GetAction()
        {
            byte[] contentBuffer = ReceiveBuffer();

            int index = Array.FindIndex(contentBuffer, b => b == (byte)'(');
            char[] actionNameChars = new char[index];
            Parallel.For(0, index, i => actionNameChars[i] = (char)contentBuffer[i]);
            string actionName = new string(actionNameChars);

            List<string> constantList = new List<string>();

            int parmFromInclusive = index + 1;
            int parmToExclusive = Array.FindIndex(contentBuffer, parmFromInclusive,
                b => b == (byte)',' || b == (byte)')');
            while (parmToExclusive != -1)
            {
                if (contentBuffer[parmToExclusive - 1] != (byte)'(')
                {
                    char[] parmChars = new char[parmToExclusive - parmFromInclusive];
                    Parallel.For(parmFromInclusive, parmToExclusive,
                        i => parmChars[i - parmFromInclusive] = (char) contentBuffer[i]);
                    string parm = new string(parmChars);
                    constantList.Add(parm);
                    parmFromInclusive = parmToExclusive + 1;
                    parmToExclusive = Array.FindIndex(contentBuffer, parmFromInclusive,
                        b => b == (byte) ',' || b == (byte) ')');
                    continue;
                }
                break;
            }

            string actionFullName = ConstContainer.GetFullName(actionName, constantList.ToArray());
            //Console.WriteLine(constantList.Count);
            //Console.WriteLine(actionFullName);
            return _actionDict[actionFullName];
        }

        private byte[] ReceiveBuffer()
        {
            byte[] lengthBuffer = new byte[LengthBufferSize];
            int offset = 0;
            int remaining = LengthBufferSize;
            do
            {
                int rcvCount = _socket.Receive(lengthBuffer, offset, remaining, SocketFlags.None);
                offset += rcvCount;
                remaining -= rcvCount;
            } while (offset < 4);

            int contentBufferSize = GetLength(lengthBuffer);
            byte[] result = new byte[contentBufferSize];
            offset = 0;
            remaining = contentBufferSize;
            do
            {
                int rcvCount = _socket.Receive(result, offset, remaining, SocketFlags.None);
                offset += rcvCount;
                remaining -= rcvCount;
            } while (offset < contentBufferSize);

            return result;
        }

        private byte[] StringToBytes(string message)
        {
            int length = message.Length;
            byte[] result = new byte[length];
            Parallel.For(0, length, i => result[i] = (byte)message[i]);
            return result;
        }

        private string BytesToString(byte[] bytes)
        {
            return BytesToString(bytes, 0, bytes.Length);
        }

        private string BytesToString(byte[] bytes, int offset, int count)
        {
            char[] chars = new char[count];
            Parallel.For(offset, count, i => chars[i - offset] = (char)bytes[i]);
            return new string(chars);
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
