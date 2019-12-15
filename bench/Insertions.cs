using System;
using BenchmarkDotNet.Attributes;
using LinkedList;


namespace LinkedList.Bench
{
    [MemoryDiagnoser]
    //  [ShortRunJob]  
    public class Insertion
    {
        [Params(65536, 65536 * 16, 65536 * 16 * 8)]
        public int N;
        char[] Array;
        LinkedList<Char> List;
        System.Collections.Generic.LinkedList<Char> StandartList;

        [IterationSetup]
        public void Setup()
        {
            Array = new char[N];
            List = new LinkedList<Char>(Array);
            StandartList = new System.Collections.Generic.LinkedList<Char>(Array);
        }
        [Benchmark(Description = "LinkedList")]
        public void LinkedList()
        {
            var node = List.Head();
            for (int i = 0; i < Array.Length / 4; i++)
            {
                node = node.Next().Next().Next();
                List.AddAfter(node, 'a');
            }
        }
        [Benchmark(Description = "System.Collections.Generic.LinkedList")]
        public void StandartLinkedList()
        {

            var node = StandartList.First;
            var count = Array.Length / 4;
            for (int i = 0; i < count; i++)
            {
                node = node.Next.Next.Next;
                StandartList.AddAfter(node, 'a');
            }
        }
    }
}