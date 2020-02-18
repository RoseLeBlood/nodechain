using System;
using ASF.Node.Block;

namespace nodechain {
    [Serializable]
    public class CircleBlockChain : GenericBlockChain<CircleEntry>  {

        public CircleBlockChain (CircleEntry data, String hash, Guid OwnerGuid) 
            : base (data, hash, OwnerGuid) { }

        public CircleBlockChain (SHA512BlockEntry<CircleEntry> data) 
            : base (data) { }

    }
}