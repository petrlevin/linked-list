using System;
using System.Collections.Generic;
using System.Linq;

namespace LinkedList
{


    public partial class LinkedList<T>
    {
        private int _capacity;
        private Node<T>[] _nodes;
        private int _head = -1;
        private int _tail = -1;
        private int _length;
        private int _place;
        private Forward _forward;
        private Reverse _reverse;

        private ListNode<T> NodeAt(int index)
        {
            return new ListNode<T>(this, index);
        }

        private Node<T>[] CreateNodes(int capacity)
        {
            var node = new Node<T>(-1, -1, default(T));
            return Enumerable.Repeat(node, capacity).ToArray();
        }

        public LinkedList(int capacity = 256)
        {
            _capacity = capacity;
            _nodes = CreateNodes(capacity);
            _forward = new Forward(this);
            _reverse = new Reverse(this);
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

        public ListNode<T> TailNode()
        {
            return NodeAt(_tail);
        }

        public ListNode<T> HeadNode()
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

        public void RemoveAfter(ListNode<T> target, int count = 0)
        {
            var index = target._index;
            RemoveAfter(index, count);
        }

        internal void RemoveAfter(int index, int count)
        {
 

            var node = _nodes[index];
            var next = node.Next;
            _nodes[next].Previous = -1;

            if (count == 0)
            {
                _nodes[index].Next = -1;
                _tail = index;
                return;
            }
            else
            {
                for (var j = 0; j < count; j++)
                {
                    node = _nodes[next];
                    if (node.Next == -1)
                    {
                        _nodes[index].Next = -1;
                        _tail = index;
                        return;
                    }
                    next = node.Next;
                }
                _nodes[next].Previous = index;
                _nodes[index].Next = next;
            }
        }

        internal void RemoveBefore(int index, int count)
        {
            var node = _nodes[index];
            var next = node.Previous;
            _nodes[next].Previous = -1;

            if (count == 0)
            {
                _nodes[index].Next = -1;
                _tail = index;
                return;
            }
            else
            {
                for (var j = 0; j < count; j++)
                {
                    node = _nodes[next];
                    if (node.Next == -1)
                    {
                        _nodes[index].Next = -1;
                        _tail = index;
                        return;
                    }
                    next = node.Next;
                }
                _nodes[next].Previous = index;
                _nodes[index].Next = next;
            }
        }
        public void CopyTo(T[] destination)
        {
            throw new NotImplementedException();
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

        internal T GetValue(int index)
        {
            return _nodes[index].Value;
        }

        internal void SetValue(int index, T value)
        {
            _nodes[index].Value = value;
        }

        internal ListNode<T> GetNext(int index)
        {
            var next = _nodes[index].Next;
            if (next < 0)
                throw new ArgumentOutOfRangeException("node is tail");
            return NodeAt(next);
        }
        internal ListNode<T> GetPrevious(int index)
        {
            var previous = _nodes[index].Previous;
            if (previous < 0)
                throw new ArgumentOutOfRangeException("node is head");
            return NodeAt(previous);

        }
        internal bool IsTail(int index)
        {

            return index == _tail;
        }

        internal bool IsHead(int index)
        {
            return index == _head;
        }

        internal int AddNode(Node<T> node)
        {
            var result = _place;
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

    }



}
