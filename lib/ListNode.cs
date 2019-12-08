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

        public bool IsLast() => _list.IsLast(_index);
        public ListNode<T> Next() => _list.GetNext(_index);

        public bool IsFirst() => _list.IsFirst(_index);
        public ListNode<T> Previous() => _list.GetPrevious(_index);



    }
}
