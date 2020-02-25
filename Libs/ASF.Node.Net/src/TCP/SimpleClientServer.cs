namespace ASF.Node.Net.TCP {
    public class SimpleClientServer {
        public string Name { get; protected set; }
        public SimpleTCPClient Client { get; protected set; }
        public SimpleTCPServer Server { get; protected set; }


        public SimpleClientServer( string name) {
            Server = new SimpleTCPServer("0.0.0.0", 4988);
            Name = name;
        }
        public bool start(string serverIP, int serverPort) {
            Client = new SimpleTCPClient(serverIP, serverPort);
            return Server.listen();
        }

    }
}