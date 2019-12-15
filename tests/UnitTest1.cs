using System;
using NUnit.Framework;

namespace LinkedList.Tests
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void Test1()
        {
            var ll = new LinkedList<string>(2); 
            ll.AddFirst("hello");
            ll.AddLast("world");
            var world =ll.Tail();
            ll.AddLast("upsd");
            var upsd = ll.Tail(); 
            ll.AddFirst("yes");
            var l = ll.ToList();
            Assert.AreEqual("yes",l[0]);
            Assert.AreEqual("hello",l[1]);
            Assert.AreEqual("world",l[2]);
            Assert.AreEqual("upsd",l[3]);
            ll.AddBefore(upsd,"brothers and sisters");
            l = ll.ToList();
            Assert.AreEqual("yes",l[0]);
            Assert.AreEqual("hello",l[1]);
            Assert.AreEqual("world",l[2]);
            Assert.AreEqual("brothers and sisters",l[3]);
            Assert.AreEqual("upsd",l[4]);            
            
            ll.Remove(world.Next());
             l = ll.ToList();
            Assert.AreEqual("yes",l[0]);
            Assert.AreEqual("hello",l[1]);
            Assert.AreEqual("world",l[2]);
            Assert.AreEqual("upsd",l[3]); 
            ll.AddLast("a");
            ll.AddLast("b");
            ll.AddLast("c");
            ll.AddLast("d");
            ll.AddFirst("a");
            ll.AddFirst("b");
            ll.AddFirst("c");
            ll.AddFirst("d");
            l = ll.ToList();
            ll.Remove(world.Next());
            ll.AddLast("hhhhhhhh");
            l = ll.ToList();
                Assert.AreEqual("d",l[0]);
            Assert.AreEqual("c",l[1]);
            Assert.AreEqual("b",l[2]);
            Assert.AreEqual("a",l[3]); 
        
            Assert.AreEqual("yes",l[4]);
            Assert.AreEqual("hello",l[5]);
            Assert.AreEqual("world",l[6]);
            Assert.AreEqual(12,l.Count);

            var n = world.Next();
            ll.Remove(n);
            Assert.AreEqual(false,n.IsInList());
            var t = n.GetValue();
            Assert.Throws(typeof(InvalidOperationException),()=>n.GetValue());

                                    

           
        }


  
    }
}