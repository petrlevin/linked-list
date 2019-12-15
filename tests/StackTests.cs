using NUnit.Framework;


namespace LinkedList.Tests
{
    public class StackTests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void Test()
        {
            var s = new Stack();
            s.Push(50);
            s.Push(1);
            int value;
            var poped = s.TryPop(out value);
            Assert.AreEqual(true, poped);
            Assert.AreEqual(1, value);
            poped = s.TryPop(out value);
            Assert.AreEqual(true, poped);
            Assert.AreEqual(50, value);
            poped = s.TryPop(out value);
            Assert.AreEqual(false, poped);
        }


        [Test]
        public void ALotOf()
        {
            var s = new Stack();
            for (int i = 0; i < 10000; i++)
            {
                s.Push(i);
            }
            int value;
            for (int i = 0; i < 10000; i++)
            {

                var poped = s.TryPop(out value);
                Assert.AreEqual(true, poped);
                Assert.AreEqual(10000 - i - 1, value);
            }
            var p = s.TryPop(out value);
            Assert.AreEqual(false, p);
        }
    }
}