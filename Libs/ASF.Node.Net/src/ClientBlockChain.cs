//
//  ClientBlockChain.cs
//
//  Author:
//       sophia <annasophia.schroeck@outlook.de>
//
//  Copyright (c) 2020 amber-sophia
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
using System;
using ASF.Node.Block;

namespace ASF.Node.Net {
    public class ClientBlockChain : GenericBlockChain<ClientEntry>  {
         public ClientBlockChain (ClientEntry data, String hash, Guid CreaterGuid) 
            : base (data, hash, CreaterGuid) { }

        public ClientBlockChain (SHA512BlockEntry<ClientEntry> data) 
            : base (data) { }

        public override void OnSetNode(Node<SHA512BlockEntry<ClientEntry>, GenericBlockChain<ClientEntry, SHA512BlockEntry<ClientEntry>>> node) {
            
            if(node is ClientBlockChain) {
                ClientBlockChain _cnode = node as ClientBlockChain;
                _cnode.Data.Data.connect(); 
            }
        }
        public override void OnRemoveNode(Node<SHA512BlockEntry<ClientEntry>, GenericBlockChain<ClientEntry, SHA512BlockEntry<ClientEntry>>> node) {
            if(node is ClientBlockChain) {
                ClientBlockChain _cnode = node as ClientBlockChain;
                _cnode.Data.Data.disconnect(); 
            }
        }
        public virtual ClientBlockChain getNodeRemote (string hash) {
            var local_node = getNode(hash);
            if(local_node != null) return local_node as ClientBlockChain;

            string message = "";
            SHA512BlockEntry<ClientEntry> node = null;

            Data.Data.write(string.Format("HAVE {0}", hash));
            Data.Data.read(ref message);
            if(message == "YES") {
                Data.Data.write(string.Format("GET {0}", hash));
                Data.Data.read(ref message);
                //TODO: Convert Message to SHA512BlockEntry<ClientEntry>
                if(node != null)
                    setNode (new GenericBlockChain<ClientEntry, SHA512BlockEntry<ClientEntry>>(node));
            } else {
                if (Next != null)
                    return (m_nodes[1] as ClientBlockChain).getNodeRemote (hash);
                if (Prev != null)
                    return (m_nodes[0] as ClientBlockChain).getNodeRemote (hash);
            }

            return getNode(hash) as ClientBlockChain;
        }
        public virtual ClientBlockChain publishNodeRemote(SHA512BlockEntry<ClientEntry> entry) {
            Data.Data.write("PUBLISH START\n");
            Data.Data.write(entry.ToString());
            Data.Data.write("PUBLISH END\n");

            if (Next != null)
                (m_nodes[1] as ClientBlockChain).publishNodeRemote (entry);
            if (Prev != null)
                (m_nodes[0] as ClientBlockChain).publishNodeRemote (entry);

            return this;
        }
    }
}