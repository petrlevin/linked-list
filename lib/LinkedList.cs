using System;
using System.Collections.Generic;
using System.Linq;

namespace LinkedList
{
    public partial class LinkedList<T>
    {
        const string INVALID_NODE_MESSAGE = "Node is not valid. Has been removed";
        private int _capacity;
        private Node<T>[] _nodes;
        private int _head = -1;
        private int _tail = -1;
        private int _length;
        private int _place;
        private Forward _forward;
        private Reverse _reverse;
        private ReallocateStrategy _reallocate;
        private ListNode<T> NodeAt(int index)
        {
            return new ListNode<T>(this, index, _nodes[index].Stamp);
        }
        private static Node<T>[] CreateNodes(int capacity)
        {
            var node = new Node<T>(-1, -1, default(T));
            return Enumerable.Repeat(node, capacity).ToArray();
        }
        private static int GetCapacity(ICollection<T> source)
        {
            var capacity = 256;
            while (source.Count > capacity)
            {
                capacity = capacity * 2;
            }
            return capacity;
        }
        private void CheckStamp(int index, int stamp)
        {
            if (!HasSameStamp(index, stamp))
            {
                throw new InvalidOperationException(INVALID_NODE_MESSAGE);
            }
        }
        private void Remove(int index)
        {
            var node = _nodes[index];
            if (node.Previous >= 0)
            {
                _nodes[node.Previous].Next = node.Next;
            }
            if (node.Next >= 0)
            {
                _nodes[node.Next].Previous = node.Previous;
            }
            if (index == _head)
            {
                _head = node.Next;
            }
            if (index == _tail)
            {
                _tail = node.Previous;
            }
            _place = index;
            _nodes[_place].Stamp++;
        }
        private int AddNode(Node<T> node)
        {
            var result = _place;
            if (_place == _capacity)
            {
                ReAllocate();
            }
            var placeNode = _nodes[_place];

            _nodes[_place] = node;

            if ((placeNode.Next != -1) && (_nodes[placeNode.Next].Previous == _place))
            {
                _place = placeNode.Next;
            }
            else
            {
                _place = ++_length;
            }
            return result;
        }
        private void ReAllocate()
        {
            var capacity = _reallocate.GetNewCapacity(_capacity);
            var nodes = new Node<T>[capacity];
            _nodes.CopyTo(nodes, 0);
            Array.Copy(CreateNodes(capacity - _capacity), 0, nodes, _capacity, capacity - _capacity);
            _nodes = nodes;
            _capacity = capacity;
        }

        public List<T> ToList()
        {
            var result = new List<T>();
            var i = _head;
            while (i >= 0)
            {
                var node = _nodes[i];
                result.Add(node.Value);
                i = node.Next;
            }
            return result;
        }
        public LinkedList(int capacity = 256, ReallocateStrategy reallocate = null)
        {
            _capacity = capacity;
            _nodes = CreateNodes(capacity);
            _forward = new Forward(this);
            _reverse = new Reverse(this);
            _reallocate = reallocate ?? new ReallocateStrategy.Default();
        }
        public LinkedList(ICollection<T> source, ReallocateStrategy reallocate = null) : this(GetCapacity(source), reallocate)
        {
            foreach (var item in source)
            {
                AddLast(item);
            }
        }
        public void AddFirst(T value)
        {
            _forward.Add(value);
            return;
        }
        public void AddLast(T value)
        {
            _reverse.Add(value);
            return;
        }
        public ListNode<T> Tail()
        {
            return NodeAt(_tail);
        }
        public ListNode<T> Head()
        {
            return NodeAt(_head);
        }
        public ListNode<T> AddBefore(ListNode<T> target, T value)
        {
            return _reverse.Add(target, value);
        }
        public ListNode<T> AddAfter(ListNode<T> target, T value)
        {
            return _forward.Add(target, value);
        }
        public void Remove(ListNode<T> target)
        {
            var index = target._index;
            Remove(index);
        }

        internal bool HasSameStamp(int index, int stamp)
        {
            return _nodes[index].Stamp == stamp;
        }
        internal T GetValue(int index, int stamp)
        {
            CheckStamp(index, stamp);
            return _nodes[index].Value;
        }
        internal void SetValue(int index, int stamp, T value)
        {
            CheckStamp(index, stamp);
            _nodes[index].Value = value;
        }
        internal ListNode<T> GetNext(int index, int stamp)
        {
            CheckStamp(index, stamp);
            var next = _nodes[index].Next;
            if (next < 0)
                throw new ArgumentOutOfRangeException("node is tail");
            return NodeAt(next);
        }
        internal ListNode<T> GetPrevious(int index, int stamp)
        {
            CheckStamp(index, stamp);
            var previous = _nodes[index].Previous;
            if (previous < 0)
                throw new ArgumentOutOfRangeException("node is head");
            return NodeAt(previous);
        }
    }


}
