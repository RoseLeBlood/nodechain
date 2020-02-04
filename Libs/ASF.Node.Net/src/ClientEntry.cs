using System;
using System.Net.Sockets;
using System.Threading;

namespace ASF.Node.Net {
    public class ClientEntry {
        protected TcpClient m_pClient;
        protected NetworkStream m_pStream;
        protected String m_strServer;
        protected String m_strErrorString;
        protected bool m_bIsConnected;
        protected Thread m_clientThread;
        protected int m_iThreadDelay;
        protected int m_iPort;

        public TcpClient Client { get { return m_pClient; } }
        public NetworkStream Stream { get { return m_pStream; } }
        public String Error { get { return m_strErrorString; } }
        public bool IsConnected { get { return m_bIsConnected; } }
        public String ServerAddress { get { return m_strServer; } }
        public Int32 ServerPort { get { return m_iPort; } }
        public Int32 ThreadDelay { get { return m_iThreadDelay; } set { m_iThreadDelay = value; } }

        public ClientEntry (string server, int port) {
            m_strServer = server;
            m_iPort = port;
            m_clientThread = new Thread (StaticThreadHelper);
            m_iThreadDelay = 2000;

        }
        public bool connect () {
            try {
                m_pClient = new TcpClient (m_strServer, m_iPort);
                m_pStream = m_pClient.GetStream ();
                m_clientThread.Start (this);
            } catch (Exception e) {
                m_strErrorString = e.ToString ();
                m_bIsConnected = false;
            }
            m_bIsConnected = true;
            return m_bIsConnected;
        }

        public bool disconnect () {
            if (!m_bIsConnected) { m_strErrorString = "Not connected"; return false; }

            try {
                m_pStream.Close ();
                m_pClient.Close ();
            } catch (Exception e) {
                m_strErrorString = e.ToString ();
                return false;
            }
            return true;
        }
        public bool write (string message) {
            if (!m_bIsConnected) { m_strErrorString = "Not connected"; return false; }

            Byte[] data = System.Text.Encoding.ASCII.GetBytes (message);
            return Write (data, 0, data.Length);
        }
        public bool Write (Byte[] data, int offset, int length) {
            try {
                m_pStream.Write (data, offset, length);
            } catch (Exception e) {
                m_strErrorString = e.ToString ();
                return false;
            }
            return true;
        }
        public bool read (ref string message) {
            if (!m_bIsConnected) { m_strErrorString = "Not connected"; return false; }

            Byte[] data = new Byte[256];
            bool _ret = false;

            Int32 bytes = read (data, 0, data.Length, ref _ret);
            message = System.Text.Encoding.ASCII.GetString (data, 0, bytes);
            return _ret;
        }
        public Int32 read (Byte[] data, int offset, int length, ref bool ret) {
            Int32 _readed = 0;

            try {
                _readed = m_pStream.Read (data, offset, length);
            } catch (Exception e) {
                m_strErrorString = e.ToString ();
                ret = false;
            }
            ret = true;
            return _readed;
        }
        public virtual void OnThread () {
            Thread.Sleep (m_iThreadDelay);
        }
        private static void StaticThreadHelper (Object thread) {
            ClientEntry _thread = thread as ClientEntry;

            if (_thread != null) {
                while (_thread.m_bIsConnected) {
                    _thread.OnThread ();
                }
            }
        }
    }
}