using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Agents
{
    public class Client
    {
        #region Fields

        private Socket _serverSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

        private string _host;

        private int _port;

        #endregion

        #region Constructors

        public Client()
        {
            _host = "127.0.0.1";
            _port = 888;
        }

        #endregion

        #region Methods

        public void Connect()
        {
            _serverSocket.Connect(_host, _port);
            SendMessage("a1");
        }

        public void ExecutionAction(string name, params string[] parmList)
        {
            StringBuilder sb = new StringBuilder();
            if (parmList.Length != 0)
            {
                sb.AppendFormat("{0}(", name);
                for (int i = 0; i < parmList.Length - 1; i++)
                {
                    sb.AppendFormat("{0},", parmList[i]);
                }
                sb.AppendFormat("{0})", parmList[parmList.Length - 1]);
            }
            else
            {
                sb.AppendFormat("{0}()", name);
            }
            SendMessage(sb.ToString());
        }

        private void SendMessage(string message)
        {
            byte[] lengthBuffer = new byte[4];
            byte[] contentBuffer = StringToBytes(message);
            SetBytesLength(lengthBuffer, contentBuffer.Length);
            _serverSocket.Send(lengthBuffer);
            _serverSocket.Send(contentBuffer);

            Console.WriteLine("Send {0}", message);
        }

        private byte[] StringToBytes(string message)
        {
            int length = message.Length;
            byte[] result = new byte[length];
            Parallel.For(0, length, i => result[i] = (byte) message[i]);
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
