using ASF.Node.Binary;

namespace ASF.Node.Net {
    public class ClientBinaryTreeNode : BinaryTreeNode {
        public ClientBinaryTreeNode (string name, ClientEntry data = null) : base (name, data) { }

        public override bool IsGreaterThan (BinaryTreeNode b) {
            return false;
        }
        public StreamBinaryTreeNode ToStreamBinaryTreeNode () {
            if (m_data is ClientEntry) {
                ClientEntry data = m_data as ClientEntry;
                return new StreamBinaryTreeNode (Name, data.Stream);
            } else {
                return null;
            }
        }

        public override void OnSetNode (Node<object, BinaryTreeNode<object>> node) {
            if (node is ClientBinaryTreeNode) {
                ClientBinaryTreeNode cnode = node as ClientBinaryTreeNode;
                (cnode.Data as ClientEntry).connect ();
            }
        }
        public override void OnRemoveNode (Node<object, BinaryTreeNode<object>> node) {
            if (node is ClientBinaryTreeNode) {
                ClientBinaryTreeNode cnode = node as ClientBinaryTreeNode;
                (cnode.Data as ClientEntry).disconnect ();
            }
        }
    }
}