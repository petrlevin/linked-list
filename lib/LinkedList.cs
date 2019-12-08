using System;
using System.Collections.Generic;

namespace LinkedList
{
    public class LinkedList<T>
    {
        private int _capacity;
        private Node<T>[] _nodes;

        private int _head = -1;

        private int _tail = -1;

        private int _length;

        private ListNode<T> NodeAt(int index)
        {
            return new ListNode<T>(this, index);
        }

        public LinkedList(int capacity = 256)
        {
            _capacity = capacity;
            _nodes = new Node<T>[capacity];

        }

        public void AddFirst(T value)
        {
            _nodes[_length] = new Node<T>(-1, _head, value);
            if (_head >= 0)
                _nodes[_head].Previous = _length;
            _head = _length;
            if (_tail < 0)
                _tail = _length;
            _length++;
        }



        public void AddLast(T value)
        {
            _nodes[_length] = new Node<T>(_tail, -1, value);
            if (_tail >= 0)
                _nodes[_tail].Next = _length;
            _tail = _length;
            if (_head < 0)
                _head = _length;
            _length++;
        }

        public ListNode<T> Last(){
            return NodeAt(_tail);
        }

       public ListNode<T> First(){
            return NodeAt(_head);
        }
 
        public ListNode<T> AddBefore(ListNode<T> target, T value)
        {
            var index = target._index;
            var node = _nodes[index];
            var previous = node.Previous;
            _nodes[_length] = new Node<T>(previous, index, value);
            _nodes[previous].Next = _length;
            if (index == _head)
                _head = _length;
            return NodeAt(_length++);
        }

        public ListNode<T> AddAfter(ListNode<T> target, T value)
        {
            var index = target._index;
            var node = _nodes[index];
            var next = node.Next;
            _nodes[_length] = new Node<T>(index, next, value);
            _nodes[next].Previous = _length;
            if (index == _tail)
                _tail = _length;
            return NodeAt(_length++);
        }

        public void RemoveAfter(ListNode<T> target, int count = 0)
        {
            var index = target._index;
            RemoveAfter(index,count);
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
                _nodes[index].Next=next;
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
                _nodes[index].Next=next;
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
            return new ListNode<T>(this, next);
        }
        internal ListNode<T> GetPrevious(int index)
        {
            var previous = _nodes[index].Previous;
            if (previous < 0)
                throw new ArgumentOutOfRangeException("node is head");
            return new ListNode<T>(this, previous);

        }
        internal bool IsLast(int index)
        {
            var next = _nodes[index].Next;
            return next == -1;
        }

        internal bool IsFirst(int index)
        {
            var previous = _nodes[index].Previous;
            return previous == -1;
        }

 internal abstract class Modifier {

       private LinkedList<T> _list;
       public ListNode<T> AddAfter(ListNode<T> target, T value)
        {
            var index = target._index;
            var node = _list._nodes[index];
            var next = node.Next;
            _list._nodes[_list._length] = new Node<T>(index, next, value);
            _list._nodes[next].Previous = _list._length;
            if (index == _list._tail)
                _list._tail = _list._length;
            return _list.NodeAt(_list._length++);
        }

       

        protected abstract int Next(Node<T> node);
        protected abstract int Previous(Node<T> node);

        protected abstract int SetNext(int index,int next);

        protected abstract int SetPrevious(int index,int previous);
        protected abstract int NewNode(int index,int previous, T value);

        protected abstract  void SetTail(int index);

    }

    internal abstract  class Forward:Modifier{

    }


    }


   
}
