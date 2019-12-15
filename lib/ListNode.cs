using System;
using System.Collections.Generic;

namespace LinkedList
{
    public struct ListNode<T> : IEquatable<ListNode<T>>
    {
        internal int _index;
        internal int _stamp;
        internal LinkedList<T> _list;
        internal ListNode(LinkedList<T> list, int index, int stamp)
        {
            _index = index;
            _list = list;
            _stamp = stamp;
        }
        public T GetValue()
        {
            return _list.GetValue(_index, _stamp);
        }
        public void SetValue(T value)
        {
            _list.SetValue(_index, _stamp, value);
        }
        public static bool operator ==(ListNode<T> firts, ListNode<T> second)
        {
            return firts.Equals(second);
        }
        public static bool operator !=(ListNode<T> firts, ListNode<T> second)
        {
            return !firts.Equals(second); ;
        }
        public ListNode<T> Next() => _list.GetNext(_index, _stamp);
        public ListNode<T> Previous() => _list.GetPrevious(_index, _stamp);
        public bool IsInList()
        {
            return _list.HasSameStamp(_index, _stamp);
        }
        public override bool Equals(object obj)
        {
            return (obj is ListNode<T>) && Equals((ListNode<T>)obj);
        }
        public bool Equals(ListNode<T> node)
        {
            return (_list == node._list) && (_index == node._index);
        }
        public override int GetHashCode()
        {
            var hashCode = -1127307738;
            hashCode = hashCode * -1521134295 + _index.GetHashCode();
            hashCode = hashCode * -1521134295 + EqualityComparer<LinkedList<T>>.Default.GetHashCode(_list);
            hashCode = hashCode * -1521134295 + EqualityComparer<T>.Default.GetHashCode(GetValue());
            return hashCode;
        }
    }
}
