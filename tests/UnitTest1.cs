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
            var ll = new LinkedList<string>(); 
            ll.AddFirst("hello");
            ll.AddLast("world");
            var world =ll.HeadNode();
            ll.AddLast("upsd");
            var upsd = ll.TailNode(); 
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
            Assert.Pass();
            ll.RemoveAfter(world,1);
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
            ll.RemoveAfter(world);
            l = ll.ToList();
                Assert.AreEqual("a",l[0]);
            Assert.AreEqual("b",l[1]);
            Assert.AreEqual("c",l[2]);
            Assert.AreEqual("d",l[3]); 
        
            Assert.AreEqual("yes",l[4]);
            Assert.AreEqual("hello",l[5]);
            Assert.AreEqual("world",l[6]);
            Assert.AreEqual(7,l.Count); 
                                    
            Assert.Pass();
           
        }


  
    }
}