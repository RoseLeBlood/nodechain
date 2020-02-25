using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using ASF.Node.Extras;

namespace ASF.Node.Net.TCP {
    
    
    public class SimpleTCPServer {
        protected TcpListener m_pServer;
        protected Thread m_pServerThread;

        public IPAddress Address { get; set; }
        public int Port { get; set; }

        public Exception LastError { get; protected set; }

        public SimpleTCPServer() : this("127.0.0.1") { }
        public SimpleTCPServer(string ip) : this(ip, 4899) { }
        public SimpleTCPServer(string ip, int port) { 
            Address = IPAddress.Parse(ip);
            Port = port;
        }
        public bool listen() {
            try {
                m_pServer = new TcpListener(Address, Port);
                m_pServerThread = new Thread(new ParameterizedThreadStart(serverThreadFunc));
                m_pServerThread.IsBackground = true;
                m_pServerThread.Start(this);

                
            } catch(Exception ex) {
                LastError = ex;
                return false;
            }
            return true;
        }
        private void serverThreadFunc(Object obj) {
            SimpleTCPServer serverObj = obj as SimpleTCPServer;
            if(serverObj == null) return ;

            serverObj.m_pServer.Start(); 

            try {
                while (true)
                {
                    Console.WriteLine("Waiting for a connection...");
                    TcpClient client = serverObj.m_pServer.AcceptTcpClient();
                    Console.WriteLine("Connected!");

                
                    Thread t = new Thread(new ParameterizedThreadStart(HandleDeivce));
                    t.Start(new SimpleTCPClient(client));
                }
            }
            catch (SocketException ex) {
                LastError = ex;
                serverObj.m_pServer.Stop();
            
            }
        }
        public void HandleDeivce(Object obj) {
            SimpleTCPClient client = null;

            if(obj is SimpleTCPClient) client = obj as SimpleTCPClient;
            else if(obj is TcpClient) client = new SimpleTCPClient(obj as TcpClient);
            
            if(client != null)
                client.serverHandler();
        }
    }
}