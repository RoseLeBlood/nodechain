using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace ASF.Node.Net.TCP {
    public class SimpleTCPClient {
        protected TcpClient m_pClient;
        protected NetworkStream m_pStream;
        protected byte[] m_pBuffer;
        public int Readed { get; protected set; }
        

        public SimpleTCPClient(TcpClient client) {
            m_pClient = client;
            m_pStream = client.GetStream();
            m_pBuffer = new byte[256];
        }
        public SimpleTCPClient(string ip, int port) {
            m_pClient = new TcpClient(ip, port);
            m_pStream = m_pClient.GetStream();
            m_pBuffer = new byte[256];

        }
        public void disconnect() {
            m_pStream.Close();
            m_pClient.Close();
        }
        public void sendText(string text) {
            Byte[] reply = System.Text.Encoding.ASCII.GetBytes(text);
            m_pStream.Write(reply, 0, reply.Length);
        }
        public int read(byte[] buffer, int offset, int length) {
            return m_pStream.Read(buffer, offset, length);
        }
        public void read() {
            Readed = read(m_pBuffer, 0, m_pBuffer.Length);
        }
        public String readText() {
            read();
            string hex = BitConverter.ToString(m_pBuffer);
            return Encoding.ASCII.GetString(m_pBuffer, 0, Readed);
        }
        
        public virtual bool OnClient() {
            Console.WriteLine("Recived: {0}", readText());
            return true;
        }
        public virtual void OnException(Exception ex) {

        }
        internal bool serverHandler() {
            try {
                while (true) {
                    if(OnClient() == false) break;
                }
            }
            catch(Exception ex) {
                OnException (ex); 
                disconnect();
                return false;
            }
            disconnect();

            return true;
        }
    }
}