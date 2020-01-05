using System;

namespace nodechain {
    class BlockObject  {
        protected long m_lIndex;
        protected String m_prevHash;
        protected long m_timeStamp;
        protected Object m_pDate;
        protected String m_strHash;

        protected BlockObject m_pChild;
        protected BlockObject m_pSibling;

    }
}
/* constructor(index, previousHash, timestamp, data, hash) {
        this.index = index;
        this.previousHash = previousHash.toString();
        this.timestamp = timestamp;
        this.data = data;
        this.hash = hash.toString();
    }*/