namespace LinkedList
{
    public partial class LinkedList<T>
    {
        internal abstract  class Modifier
        {
            protected LinkedList<T> List;
            protected Node<T>[] Nodes { get { return List._nodes; } }
            protected abstract Node<T> NewNode(int previous, int next, T value);
            protected abstract int Next(Node<T> node);
            protected abstract int Previous(Node<T> node);
            protected abstract void SetNext(int targetr, int next);
            protected abstract void SetPrevious(int target, int previous);
            protected abstract void SetTail(int index);
            protected abstract void SetHead(int index);
            protected abstract int GetTail();
            protected abstract int GetHead();

            protected Modifier(LinkedList<T> list)
            {
                List = list;
            }
           
            public ListNode<T> Add(ListNode<T> target, T value)
            {
                var index = target._index;
                var node = Nodes[index];
                var next = Next(node);
                var place = List.AddNode(NewNode(index, next, value));
                SetPrevious(next, place);
                if (GetTail() == index)
                    SetTail(place);
                return List.NodeAt(place);
            }
            
            public void Add(T value)
            {
                var place = List.AddNode(NewNode(-1, GetHead(), value));
                if (GetHead() >= 0)
                    SetPrevious(GetHead(), place);
                SetHead(place);
                if (GetTail() < 0)
                    SetTail(place);
            }
        }
    }
}
