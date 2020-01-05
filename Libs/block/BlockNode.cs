using System;
using System.Numerics;
using System.Text;
using System.Collections.Generic;
using node;

namespace nodechain { //// 0: root, 1: Child, 2:Sibling

    public class GenericNodeChainBlock {
        private static ulong m_index = 0;

        public string Name { get; set; } //

        public ulong Index { get; internal set; } //
        public long TimeStamp { get; internal set; } 
        public String Hash { get; internal set; } //
        public String prevHash { get; internal set; } //
        public Object Data { get; internal set; } //

        public GenericNodeChainBlock(ListNode<Object> root)
            : this(root.Name, root.Data)
        {
        }
        public GenericNodeChainBlock(string name, Object data)
        {
            Name = name;
            Data = data;
            Index = m_index++;
            //TODO!!
        }


        public static implicit operator GenericNodeChainBlock(BinaryNodeChain node)
        {
            return new GenericNodeChainBlock(node.Name, node.Data);
        }
        public void calc_hash() {

        }
    }
    [Serializable]
	public class BinaryNodeChain : Node<GenericNodeChainBlock, BinaryNodeChain>, IEnumerable<Object>, IList<GenericNodeChainBlock>
	{
        public int Count {
            get { return this.ToList().Count; }
        }
        public GenericNodeChainBlock this[int index] {
            get { return _ToList()[index]; }
            set { throw new System.NotImplementedException(); }
        }
        public bool IsReadOnly {
            get { return false; }
        }

        public int Depth
        {
            get
            {
                int depth = 0;
                BinaryNodeChain node = this;
                while(node.Parent != null)
                {
                    node = node.Parent;
                    depth++;
                }
                return depth;
            }
        }

		public override BinaryNodeChain Root
        {
            get
            {
                if (m_nodes [0] == null)
                    return this;
                else
                    return m_nodes [0].Root;
            }
            protected set
            {
            }
		}
        public BinaryNodeChain Parent 
        {
            get { return m_nodes[0]; }
            set { m_nodes[0] = value; }
        }
		public BinaryNodeChain Sibling 
		{
			get { return m_nodes[2]; }
		}
		public BinaryNodeChain Child
		{
			get { return m_nodes[1]; }
		}

        public BinaryNodeChain() { }

		public BinaryNodeChain (string name)
			: base(name, 3)  { }

		public BinaryNodeChain (string name, GenericNodeChainBlock data)
			: base(name, data, 3) { }

		public override BinaryNodeChain getNode(string hash) {
			if (m_data.Hash == hash)
				return this;

			if (m_nodes[1] != null)
				return m_nodes[1].getNode (hash);
			if (m_nodes[2] != null)
				return m_nodes[2].getNode (hash);

			return null;
		}
		public override BinaryNodeChain setNode(BinaryNodeChain node) {
            if (m_nodes[1] == null)  {
                node.Parent = this;
                node.Data.prevHash = Data.Hash;
                node.Data.Hash = Utils.ComputeSha256Hash(node);
                m_nodes[1] = node;

            }
            else
                m_nodes[1].setNode (node);

			return this;
		}
        public virtual BinaryNodeChain setChild(BinaryNodeChain node) {
            if (m_nodes[2] == null)  {
                node.Parent = this;
                node.Data.prevHash = Data.Hash;
                node.Data.Hash = Utils.ComputeSha256Hash(node);
                m_nodes[2] = node;
            }
            else
                m_nodes[2].setNode (node);

            return this;
        }

		public override BinaryNodeChain removeNode(BinaryNodeChain node, ref bool removed) {
			return this;
		}
		public override BinaryNodeChain removeNode(string name, ref bool removed)
		{
			return this;
		}
		protected override void Dispose (bool disposing)
		{
			base.Dispose (disposing);

			if (disposing) 
			{
				if (m_nodes[1] != null) 
				{
					m_nodes[1].Dispose (disposing);
				}
				if (m_nodes[2] != null) 
				{
					m_nodes[2].Dispose (disposing);
				}
			}
		}

        public override void Travers(TraversOrder order, funcTravers travers, BinaryNodeChain root)
        {

            if (travers == null)
                throw new ArgumentNullException("travers");

          
            switch (order)
            {
                case TraversOrder.Inorder:
                    if (root != null)
                    {
                        Travers(order, travers, root.m_nodes [1]);
                        travers(root);
                        Travers(order, travers, root.m_nodes [2]);
                    }
                    break;
                case TraversOrder.Postorder:
                    if (root != null)
                    {
                        Travers(order, travers, root.m_nodes [1]);
                        Travers(order, travers, root.m_nodes [2]);
                        travers(root);
                    }
                    break;
                case TraversOrder.Preorder:
                    if (root != null)
                    {
                        travers(root);
                        Travers(order, travers, root.m_nodes [1]);
                        Travers(order, travers, root.m_nodes [2]);
                    }
                    break;
            }
        }
        public override void Travers(TraversOrder order, BinaryNodeChain Root)
        {
            switch (order)
            {
                case TraversOrder.Inorder:
                    if (Root != null)
                    {
                        Travers(order, Root.m_nodes[1]);
                        Console.Write(Root.Data + " ");
                        Travers(order, Root.m_nodes[2]);
                    }
                    break;
                case TraversOrder.Postorder:
                    if (Root != null)
                    {
                        Travers(order, Root.m_nodes[1]);
                        Travers(order, Root.m_nodes[2]);
                        Console.Write(Root.Data + " ");
                    }
                    break;
                case TraversOrder.Preorder:
                    if (Root != null)
                    {
                        Console.Write(Root.Data + " ");
                        Travers(order, Root.m_nodes[1]);
                        Travers(order, Root.m_nodes[2]);
                    }
                    break;
            }
        }
        public virtual bool IsGreaterThan(BinaryNodeChain b)
        {
            if(m_nodes [1] == null)
                return false;

            Object _a = ((Object)m_nodes [1].Data);
            Object _b = ((Object)b.Data);

            if (_a.IsNumber() && _b.IsNumber())
            {

                return (Convert.ToDecimal(_a)) > (Convert.ToDecimal(_b));

            }
            return false;
        }
        public override List<GenericNodeChainBlock> ToList()
        {
            List<GenericNodeChainBlock> liste = new List<GenericNodeChainBlock>();

            addToList(this, ref liste);
            return liste;
        }

        protected void addToList(BinaryNodeChain root, ref List<GenericNodeChainBlock> liste)
        {
            if (root != null)
            {
                liste.Add(root.Data);
                addToList(root.Child, ref liste);
                addToList(root.Sibling, ref liste);
            }
        }
        public static bool operator > (BinaryNodeChain a, BinaryNodeChain b)
        {
            return a.IsGreaterThan(b as BinaryNodeChain);
        }
        public static bool operator < (BinaryNodeChain a, BinaryNodeChain b)
        {
            return !a.IsGreaterThan(b as BinaryNodeChain);
        }

        public static implicit operator BinaryNodeChain(ListNode<GenericNodeChainBlock> node)
        {
            ListNode<GenericNodeChainBlock> _root = node;
            BinaryNodeChain _new = new BinaryNodeChain();

            do
            {
                _new.setNode(new BinaryNodeChain(node.Name, node.Data));
                _root = _root.Next;
            } while (_root != null);

            return _new;
        }
        #region IEnumerable implementation
        public System.Collections.IEnumerator GetEnumerator()
        {
            throw new System.NotImplementedException();
        }
        #endregion

        #region IEnumerable implementation
        private List<GenericNodeChainBlock> _ToList()
        {
            List<GenericNodeChainBlock> liste = new List<GenericNodeChainBlock>();

            _addToList(this, ref liste);
            return liste;
        }

        private void _addToList(BinaryNodeChain root, ref List<GenericNodeChainBlock> liste)
        {
            if (root != null)
            {
                liste.Add(new GenericNodeChainBlock(root.Name, root.Data));
                _addToList(root.Child, ref liste);
                _addToList(root.Sibling, ref liste);
            }
        }
        IEnumerator<GenericNodeChainBlock> IEnumerable<GenericNodeChainBlock>.GetEnumerator()
        {
            List<GenericNodeChainBlock> list = Root._ToList();
            return list.GetEnumerator();
        }
        #endregion

        #region ICollection implementation
        public void Add(GenericNodeChainBlock item)
        {
            setNode(new BinaryNodeChain( item.Name, item));
        }

        public void Clear()
        {
            throw new System.NotImplementedException();
        }

        public bool Contains(GenericNodeChainBlock item)
        {
            return getNode(item.Name) != null;
        }

        public void CopyTo(GenericNodeChainBlock[] array, int arrayIndex)
        {
            throw new System.NotImplementedException();
        }

        public bool Remove(GenericNodeChainBlock item)
        {
            bool removed = false;
            removeNode(item.Name, ref removed);
            return removed;
        }


        #endregion

        #region IList implementation
        public int IndexOf(GenericNodeChainBlock item)
        {
            throw new System.NotImplementedException();
        }

        public void Insert(int index, GenericNodeChainBlock item)
        {
            throw new System.NotImplementedException();
        }

        public void RemoveAt(int index)
        {
            throw new System.NotImplementedException();
        }


        #endregion  


        IEnumerator<Object> IEnumerable<Object>.GetEnumerator()
        {
            return this.ToList().GetEnumerator();
        }
       
	}
}