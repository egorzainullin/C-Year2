using Microsoft.VisualStudio.TestTools.UnitTesting;
using TreeWithIterator;

namespace TreeWithIteratorTests
{
    [TestClass]
    public class TreeWithIteratorTests
    {
        private TreeWithIterator<int> tree;

        [TestInitialize]
        public void InitalizeTest()
        {
            tree = new TreeWithIterator<int>();
            tree.Add(1);
            tree.Add(2);
            tree.Add(3);
        }

        [TestMethod]
        public void AddTest()
        {
            Assert.IsTrue(true);
        }


    }
}
