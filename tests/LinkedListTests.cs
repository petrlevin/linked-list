using System;
using NUnit.Framework;
using System.Diagnostics;
using System.Collections.Generic;

namespace LinkedList.Tests
{
    public class LinkedListTests
    {
        [SetUp]
        public void Setup()
        {
        }


        

        [Test]
        public void Test()
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
     //       var t = n.GetValue();
            Assert.Throws(typeof(InvalidOperationException),()=>n.GetValue());    
        }

        [Test]
         public void Reallocation(){
             var ll = new LinkedList<Char>(4);
             ll.AddLast('a');
             ll.AddLast('b');
             ll.AddLast('c');
             ll.AddLast('d');
             ll.AddLast('e');
             ll.AddLast('f');
             var l = ll.ToList();
             var s = String.Concat(l);
             Assert.AreEqual("abcdef",s);
         }

       [Test]
         public void ReallocationAndRemove(){
             var ll = new LinkedList<Char>(4);
             ll.AddLast('a');
             ll.AddLast('b');
             var n = ll.Tail();
             ll.AddLast('c');
             ll.AddLast('d');
             ll.AddLast('e');
             ll.AddLast('f');
             ll.Remove(n);
             var l = ll.ToList();
             var s = String.Concat(l);
             Assert.AreEqual("acdef",s);
         }


        [Test]
        public void NodesEquals(){
            var s = "ABCDEFGK";
             var ll= new LinkedList<Char>();
             for (int i = 0; i < 256+16; i++)
             {
                 ll.AddLast(s[i%8]);
             }
             var t = ll.Tail();
             var n = ll.Head();
             for (int i = 1; i < 256+16; i++)
             {
                 n = n.Next();
             }
             Assert.AreEqual(n,t);

        }

       [Test]
        public void NodesNotEquals(){
            var s = "ABCDEFGK";
             var ll= new LinkedList<Char>();
             for (int i = 0; i < 256+16; i++)
             {
                 ll.AddLast(s[i%8]);
             }
             var t = ll.Tail();
             var n = ll.Head();
             for (int i = 1; i < 8; i++)
             {
                 n = n.Next();
             }
             Assert.AreEqual(n.GetValue(),t.GetValue());
             Assert.AreNotEqual(n,t);

        }

        [Test]
        public void SetValues(){
             var s = "ABCDEFGK";
             var ll= new LinkedList<Char>();
             for (int i = 0; i < 256+16; i++)
             {
                 ll.AddLast(s[i%8]);
             }           
             var n = ll.Head();
             for (int i = 1; i < 256+16; i++)
             {
                   if (i%4==0){
                     n.SetValue('x');
                 }
                 n=n.Next();
             }            
             Debug.WriteLine(String.Concat(ll.ToList()));
             n = ll.Head();
             var chrs = new char[8];
             chrs[0] =n.GetValue();
             for (int i = 1; i < 256+16; i++)
             {              
                  if (i%8==0){
                       Assert.AreEqual(String.Concat(chrs),"ABCxEFGx"); 
                  }
                  n=n.Next();
                  chrs[i%8] = n.GetValue();
                  
             }            
  
        } 

        [Test]
        public void Enumerator(){
            var s = "ABCDEFGK";
             var ll= new LinkedList<Char>();
             for (int i = 0; i < 256+16; i++)
             {
                 ll.AddLast(s[i%8]);
             }
             var j = 0;
             foreach (var item in ll)
             {
                 Assert.AreEqual(s[j],item);
                
                 j= (++j)%8;
             }                                  
        }

        [Test]
        public void Contains(){
            var s = "ABCDEFGK";
             var ll= new LinkedList<Char>();
             for (int i = 0; i < 256+16; i++)
             {
                 ll.AddLast(s[i%8]);
             }
             Assert.AreEqual(true,(((ICollection<Char>)ll).Contains('A')));
             Assert.AreEqual(false,(((ICollection<Char>)ll).Contains('X')));
  
                              
        }      

        [Test]
        public void CopyTo(){
            var s = "ABCDEFGK";
             var ll= new LinkedList<Char>();
             for (int i = 0; i < 256+16; i++)
             {
                 ll.AddLast(s[i%8]);
             }
             var arr = new Char[256+16+4];
             ((ICollection<Char>)ll).CopyTo(arr,4);
             
             for (int i = 4; i < arr.Length; i++)
             {
                 Assert.AreEqual(s[(i-4)%8],arr[i]);
             }
                              
        }       

        [Test]
         public void HugeList(){
             var s = "xABCxDEF";
             var ll= new LinkedList<Char>();
             for (int i = 0; i < 256*256*256; i++)
             {
                 ll.AddLast(s[i%8]);
             }
             var n = ll.Tail();
             for (int i = 1; i < 256*256*256; i++)
             {
                 n=n.Previous();
                 if (i%4==0){
                     ll.Remove(n.Next());
                 }
             }
             ll.Remove(n);
             n = ll.Head();
             var j=0;
             var chrs = new char[6];
             while (n!=ll.Tail()){
                 chrs[j] = n.GetValue();
                 n = n.Next();
                 j++;
                 if (j==6){
                     j=0;
                     //Debug.WriteLine(String.Concat(chrs));
                     Assert.AreEqual(String.Concat(chrs),"ABCDEF");
                 }

             }

             

         } 



  
    }
}