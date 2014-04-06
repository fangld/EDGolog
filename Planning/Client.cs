using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace Planning
{
    public class Client
    {

       #region Fields

        /// <summary>
        /// The socket of peer
        /// </summary>
        private Socket _socket;

        /// <summary>
        /// The bitfield of peer
        /// </summary>
        private bool[] _bitfield;

        private Timer _timer;

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
        /// The SHA1 hash of the torrent
        /// </summary>
        public byte[] InfoHash { get; set; }

        /// <summary>
        /// The peer id
        /// </summary>
        public string PeerId { get; set; }

        /// <summary>
        /// The flag that represents whether peer supports extension protocol
        /// </summary>
        public bool SupportExtension { get; set; }

        /// <summary>
        /// The flag that represents whether peer supports DHT.
        /// </summary>
        public bool SupportDht { get; set; }

        /// <summary>
        /// The flag that represents whether peer supports peer exchange.
        /// </summary>
        public bool SupportPeerExchange { get; set; }

        /// <summary>
        /// The flag that represents whether peer supports fast peer.
        /// </summary>
        public bool SupportFastPeer { get; set; }

        /// <summary>
        /// The flag that represents whether peer is connected.
        /// </summary>
        public bool IsConnected { get; private set; }

        #endregion

        #region Constructors

        /// <summary>
        /// Create new connected peer with socket
        /// </summary>
        public Client(Socket socket)
        {
            _socket = socket;
            IPEndPoint ipEndPoint = (IPEndPoint)socket.RemoteEndPoint;
            Host = ipEndPoint.Address.ToString();
            Port = ipEndPoint.Port;
            IsConnected = true;
        }

        #endregion
    }
}
