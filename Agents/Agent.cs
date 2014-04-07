using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Agents
{
    public class Agent
    {
        public void SendMessage()
        {
            Socket serverSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            serverSocket.Connect("127.0.0.1", 888);
            byte[] lengthBuffer = new byte[4];
            byte[] contentBuffer = StringToBytes("a1");
            SetBytesLength(lengthBuffer, contentBuffer.Length);
            serverSocket.Send(lengthBuffer);
            serverSocket.Send(contentBuffer);

            contentBuffer = StringToBytes("dunk(bomb1,toilet1)");
            SetBytesLength(lengthBuffer, contentBuffer.Length);
            serverSocket.Send(lengthBuffer);
            serverSocket.Send(contentBuffer);

            Console.WriteLine("Send dunk(bomb1,toilet1)");
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
    }
}
