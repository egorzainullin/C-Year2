using Microsoft.VisualStudio.TestTools.UnitTesting;
using TreeWithIterator;

namespace TreeWithIteratorTests
{
    [TestClass]
    public class TreeWithIteratorTests
    {
        private TreeWithIterator<int> tree;

        [TestInitialize]
        public void InitializeTest()
        {
            tree = new TreeWithIterator<int>();
            tree.Add(2);
            tree.Add(1);
            tree.Add(3);
        }

        [TestMethod]
        public void AddTest()
        {
            tree.Add(4);
            Assert.AreEqual(4, tree.Count);
            tree.Add(3);
            Assert.AreEqual(4, tree.Count);
        }

        [TestMethod]
        public void CountTest()
        {
            Assert.AreEqual(3, tree.Count);
            tree.Clear();
            Assert.AreEqual(0, tree.Count);
        }

        [TestMethod]
        public void RemoveTest()
        {
            tree.Remove(2);
            Assert.AreEqual(2, tree.Count);
        }

        [TestMethod]
        public void EnumeratorTest()
        {

        }

    }
}
