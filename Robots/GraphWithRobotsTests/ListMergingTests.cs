using Microsoft.VisualStudio.TestTools.UnitTesting;
using Robots;
using System.Collections.Generic;

namespace RobotsTests
{
    [TestClass]
    public class ListMergingTests
    {
        [TestMethod]
        public void MergeTest1()
        {
            var list1 = new List<int> { 1, 2, 3 };
            var list2 = new List<int> { 4, 5, 6 };
            var list = ListMerging.MergeWithoutRepeating(list1, list2);
            for (int i = 1; i < 6; i++)
            {
                Assert.AreEqual(i, list[i - 1]);
            }
        }

        [TestMethod]
        public void MergeTest2()
        {
            var list1 = new List<int> { 1, 2, 3 };
            var list2 = new List<int> { 3, 4, 5 };
            var list = ListMerging.MergeWithoutRepeating(list1, list2);
            Assert.AreEqual(5, list.Count);
            for (int i = 1; i < 5; i++)
            {
                Assert.AreEqual(i, list[i - 1]);
            }
        }
    }
}
