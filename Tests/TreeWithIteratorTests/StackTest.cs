using Microsoft.VisualStudio.TestTools.UnitTesting;
using StackList;
using System;

namespace StackList.Tests
{
    [TestClass]
    public class StackTests
    {
        private SimpleStack<int> stack;

        [TestInitialize]
        public void InitializeTest()
        {
            stack = new SimpleStack<int>();
        }

        [TestMethod]
        public void PushTest()
        {
            stack.Push(2);
            Assert.AreEqual(2, stack.Peek());
            stack.Push(2);
            Assert.AreEqual(2, stack.Count);
        }

        [TestMethod]
        public void PopTest()
        {
            stack.Push(3);
            Assert.AreEqual(3, stack.Pop());
        }

        [TestMethod]
        [ExpectedException(typeof(NullReferenceException))]
        public void PopErrorTest()
        {
            stack.Pop();
        }

        [TestMethod]
        public void PeekTest()
        {
            stack.Push(3);
            Assert.AreEqual(3, stack.Peek());
            Assert.AreEqual(1, stack.Count);
        }

        [TestMethod]
        [ExpectedException(typeof(NullReferenceException))]
        public void PeekErrorTest()
        {
            stack.Peek();
        }

        [TestMethod]
        public void IsEmptyTest()
        {
            Assert.IsTrue(stack.IsEmpty());
        }

        [TestMethod]
        public void ClearTest()
        {
            stack.Push(1);
            stack.Push(2);
            stack.Push(3);
            stack.Clear();
            Assert.IsTrue(stack.IsEmpty());
            Assert.AreEqual(0, stack.Count);
        }
    }
}