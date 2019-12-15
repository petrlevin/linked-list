using System;
using BenchmarkDotNet.Attributes;
using LinkedList;


namespace LinkedList.Bench
{
    public class Initialization
    {
        [Benchmark(Description = "LinkedList")]
        public void LinkedList()
        {
            var l = 256 * 16 * 256;
            var ll = new LinkedList<Char>(l);
            for (int i = 0; i < l; i++)
            {
                ll.AddLast('a');
            }
        }

        [Benchmark(Description = "System.Collections.Generic.LinkedList")]
        public void StandartLinkedList()
        {
            var l = 256 * 16 * 256;
            var ll = new System.Collections.Generic.LinkedList<Char>();
            for (int i = 0; i < l; i++)
            {
                ll.AddLast('a');
            }
        }
    }
}