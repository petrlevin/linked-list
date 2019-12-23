using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("LinkedList.Tests")]
namespace LinkedList
{
    public partial class LinkedList<T> : ICollection<T>
    {
        const string INVALID_NODE_MESSAGE = "Node is not valid. Has been removed";
        private int _capacity;
        private Node<T>[] _nodes;
        private int _head = -1;
        private int _tail = -1;
        private int _length;
        private Stack _places;
        private Forward _forward;
        private Reverse _reverse;
        private Func<int, int> _reallocate;

        private int _version;

        public int Count => throw new NotImplementedException();

        public bool IsReadOnly => throw new NotImplementedException();

        private int DefaultReallocate(int capacity) => capacity + capacity / 2;

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
            _places.Push(index);
            _nodes[index].Stamp++;
            _version++;
        }



        private int AddNode(Node<T> node)
        {
            int place;
            if (!_places.TryPop(out place))
            {
                place = ++_length;
            }
            if (place == _capacity)
            {
                ReAllocate();
            }
            _nodes[place] = node;
            _version++;
            return place;
        }

        private void ReAllocate()
        {
            var capacity = _reallocate(_capacity);
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

        public LinkedList(int capacity = 256, Func<int, int> reallocate = null)
        {
            _capacity = capacity;
            _nodes = CreateNodes(capacity);
            _forward = new Forward(this);
            _reverse = new Reverse(this);
            _reallocate = reallocate ?? DefaultReallocate;
            _places = new Stack();
        }

        public LinkedList(ICollection<T> source, Func<int, int> reallocate = null) : this(GetCapacity(source), reallocate)
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

        void ICollection<T>.Add(T item)
        {
            AddLast(item);
        }

        void ICollection<T>.Clear()
        {
            _head = -1;
            _tail = -1;
            _length = 0;
            _nodes = CreateNodes(_capacity);
            _places = new Stack();
        }

        bool ICollection<T>.Contains(T item)
        {
            var index = _head;
            while (index >= 0)
            {
                if (_nodes[index].Value.Equals(item))
                {
                    return true;
                }
                index = _nodes[index].Next;
            }
            return false;
        }

        void ICollection<T>.CopyTo(T[] array, int arrayIndex)
        {
            if (arrayIndex < 0 || arrayIndex > array.Length)
            {
                throw new ArgumentOutOfRangeException("arrayIndex", "Index out of range");
            }
            if (array.Length - arrayIndex < _length)
            {
                throw new ArgumentException("Insufficient space");
            }
            var index = _head;
            var i = arrayIndex;
            while (index >= 0)
            {
                array[i++] = _nodes[index].Value;
                index = _nodes[index].Next;
            }
        }

        bool ICollection<T>.Remove(T item)
        {
            var index = _head;
            while (index >= 0)
            {
                if (_nodes[index].Value.Equals(item))
                {
                    Remove(index);
                    return true;
                }
                index = _nodes[index].Next;
            }
            return false;
        }

        public IEnumerator<T> GetEnumerator()
        {
            return new Enumerator(this);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        struct Enumerator : IEnumerator<T>
        {
            private readonly LinkedList<T> _list;
            private int _version;
            private int _index;
            private T _current;

            public T Current => _current;

            object IEnumerator.Current => _current;

            private void CheckVersion()
            {
                if (_version != _list._version)
                {
                    throw new InvalidOperationException("List has been changed");
                }
            }

            public bool MoveNext()
            {
                CheckVersion();
                if (_index != -1)
                {
                    _current = _list._nodes[_index].Value;
                    _index = _list._nodes[_index].Next;
                    return true;
                }
                return false;

            }



            public void Reset()
            {
                CheckVersion();
                _index = _list._head;
                _current = default(T);
            }

            public void Dispose()
            {

            }

            public Enumerator(LinkedList<T> list)
            {
                _list = list;
                _version = list._version;
                _index = list._head;
                _current = default(T);
            }
        }
    }


}
