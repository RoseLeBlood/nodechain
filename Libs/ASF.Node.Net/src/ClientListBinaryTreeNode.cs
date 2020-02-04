//
//  ClientListBinaryTreeNode.cs
//
//  Author:
//       sophia <annasophia.schroeck@outlook.de>
//
//  Copyright (c) 2014 sophia
//
//  This program is free software: you can redistribute it and/or modify
//  it under the terms of the GNU Lesser General Public License as published by
//  the Free Software Foundation, either version 3 of the License, or
//  (at your option) any later version.
//
//  This program is distributed in the hope that it will be useful,
//  but WITHOUT ANY WARRANTY; without even the implied warranty of
//  MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
//  GNU Lesser General Public License for more details.
//
//  You should have received a copy of the GNU Lesser General Public License
//  along with this program.  If not, see <http://www.gnu.org/licenses/>.

using ASF.Node.Binary;
using ASF.Node.List;

namespace ASF.Node.Net.src {
    public class ClientListBinaryTreeNode : ListBinaryTreeNode<ClientEntry> {
        public ClientListBinaryTreeNode (string name, ListNode<ClientEntry> data = null) : base (name, data) { }

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