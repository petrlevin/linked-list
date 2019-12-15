using System;

namespace LinkedList
{
    internal struct Node<T>
    {
        public Node(int previous, int next, T value)
        {
            Next = next;
            Previous = previous;
            Value = value;
            Stamp = 0; 
        }
        public int Next { get; set; }
        public int Previous { get; set; }
        public T Value { get; set; }
        public int Stamp;

    }
}
