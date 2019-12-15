namespace LinkedList
{
    public abstract class ReallocateStrategy
    {
        public abstract int GetNewCapacity(int capacity);
        internal class Default : ReallocateStrategy
        {
            public override int GetNewCapacity(int capacity)
            {
                return capacity + capacity / 2;
            }
        }
    }
}
