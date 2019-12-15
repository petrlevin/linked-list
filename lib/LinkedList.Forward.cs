namespace LinkedList
{
    public partial class LinkedList<T>
    {
        internal class Forward : Modifier
        {
            public Forward(LinkedList<T> list) : base(list)
            {
            }
            protected override int GetHead()
            {
                return List._head;
            }
            protected override int GetTail()
            {
                return List._tail;
            }
            protected override Node<T> NewNode(int privious, int next, T value)
            {
                return new Node<T>(privious, next, value);
            }
            protected override int Next(Node<T> node)
            {
                return node.Next;
            }
            protected override int Previous(Node<T> node)
            {
                return node.Previous;
            }
            protected override void SetHead(int value)
            {
                List._head = value; ;
            }
            protected override void SetNext(int target, int value)
            {
                Nodes[target].Next = value;
            }
            protected override void SetPrevious(int target, int value)
            {
                Nodes[target].Previous = value;
            }
            protected override void SetTail(int value)
            {
                List._tail = value;
            }
        }
    }



}
