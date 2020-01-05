using System;
using System.Numerics;
using System.Text;
using System.Collections.Generic;
using node;

namespace nodechain { //// 0: root, 1: left, 2:right

    /*public class GenericNodeChainBlock<T> {
        public long Index { get; protected set; }
        public long TimeStamp { get; protected set; }
        public String Hash { get; protected set; }
        public T Data { get; protected set; }

        /*public GenericNodeChainBlock(ListNode<T> root)
            : this(root.Name, root.Data) {
        }*/
       /* public GenericNodeChainBlock(long timeStamp, T data) { }


        public static implicit operator GenericNodeChainBlock<T>(BinaryTreeNode<T> node)
        {
            var _ret =  new GenericNodeChainBlock<T>();

            return _ret;
        }
    }
    public abstract class BinaryNodeChain<T> : Node<T, BinaryNodeChain<T>>, IEnumerator<T>, IList<GenericNodeChainBlock<T>> {
        internal bool m_now = false;

        public int Count {
            get { return this.ToList().Count; }
        }
        public GenericNodeChainBlock<T> this[int index] {
            get { return _ToList()[index]; }
            set { }
        }
        public bool IsReadOnly  {
            get { return false; }
        }
        public int Depth {
            get {
                int depth = 0;
                BinaryTreeNode<T> node = this;
                while(node.Parent != null)
                {
                    node = node.Parent;
                    depth++;
                }
                return depth;
            }
        }
        public override BinaryNodeChain<T> Root {
            get {
                if (m_nodes [0] == null)
                    return this;
                else
                    return m_nodes [0].Root;
            }
            protected set { }
		}
        public BinaryNodeChain<T> Parent 
        {
            get { return m_nodes[0]; }
            set { m_nodes[0] = value; }
        }
		public BinaryNodeChain<T> Sibling 
		{
			get { return m_nodes[2]; }
		}
		public BinaryNodeChain<T> Child
		{
			get { return m_nodes[1]; }
		}

        public BinaryNodeChain() { }

		public BinaryNodeChain (string name) // 0: root, 1: Child, 2:Sibling
			: base(name, 3)  { }

		public BinaryNodeChain (string name, T data)
			: base(name, data, 3) { }

        #region IEnumerable implementation
        public System.Collections.IEnumerator GetEnumerator()
        {
            throw new System.NotImplementedException();
        }
        private List<GenericNodeChainBlock<T>> _ToList() {
            List<GenericNodeChainBlock<T>> liste = new List<GenericNodeChainBlock<T>>();

            _addToList(this, ref liste);
            return liste;
        }
        private void _addToList(BinaryNodeChain<T> root, ref List<GenericNodeChainBlock<T>> liste)
        {
            if (root != null)
            {
                liste.Add(new GenericNodeChainBlock<T>(root.Data));
                _addToList(root.Child, ref liste);
                _addToList(root.Sibling, ref liste);
            }
        }
        IEnumerator<GenericNodeChainBlock<T>> IEnumerable<GenericNodeChainBlock<T>>.GetEnumerator()
        {
            List<GenericNodeChainBlock<T>> list = Root._ToList();
            return list.GetEnumerator();
        }
        #endregion

        #region ICollection implementation
        public void Add(GenericNodeChainBlock<T> item)
        {
            //setNode(new BinaryTreeNode<T>( item.Name, item.Data));
            throw new System.NotImplementedException();
        }

        public void Clear() {
            throw new System.NotImplementedException();
        }

        public bool Contains(GenericNodeChainBlock<T> item) {
            //return getNode(item.Hash) != null; 
            return false;
        }

        public void CopyTo(GenericNodeChainBlock<T>[] array, int arrayIndex) {
            throw new System.NotImplementedException();
        }

        public bool Remove(GenericNodeChainBlock<T> item) {
            return false;
        }

        public int IndexOf(GenericNodeChainBlock<T> item) {
            throw new System.NotImplementedException();
        }

        public void Insert(int index, GenericNodeChainBlock<T> item) {
            throw new System.NotImplementedException();
        }

        public void RemoveAt(int index) {
            throw new System.NotImplementedException();
        }
        IEnumerator<T> IEnumerable<T>.GetEnumerator()
        {
            return this.ToList().GetEnumerator();
        }

        #endregion  
        
    }


  /*  [Serializable]
    public class BlockNode : BinaryNodeChain<Block> { 


        public BlockNode (Block value) : base(name, value) { } 

        public override bool IsGreaterThan(BinaryTreeNode b) {
            if(m_nodes [1] == null)
                return false;

            Block _a = ((Block)m_nodes [1].Data);
            Block _b = ((Block)b.Data);

            return (_a != null && _b != null) ? _a > _b : false;
        }
    }*/
}