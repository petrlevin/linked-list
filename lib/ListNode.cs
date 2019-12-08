namespace LinkedList
{
    public struct ListNode<T>
    {
        internal int _index;

        internal LinkedList<T> _list;

        internal ListNode(LinkedList<T> list, int index)
        {
            _index = index;
            _list = list;
        }

        public T Value
        {
            get
            {
                return _list.GetValue(_index);
            }
            set
            {
                _list.SetValue(_index, value);
            }
        }

        public bool IsTail() => _list.IsTail(_index);
        public ListNode<T> Next() => _list.GetNext(_index);

        public bool IsHead() => _list.IsHead(_index);
        public ListNode<T> Previous() => _list.GetPrevious(_index);
    }
}
