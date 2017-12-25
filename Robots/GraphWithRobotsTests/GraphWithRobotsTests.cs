using Microsoft.VisualStudio.TestTools.UnitTesting;
using Robots;

namespace GraphWithRobotsTests
{
    [TestClass]
    public class GraphWithRobotsTests
    {

        private GraphWithRobots graph1;

        private GraphWithRobots graph2;

        private bool[,] matrix1;

        private bool[,] matrix2;

        [TestInitialize]
        public void InitTest()
        {
            matrix1 = new bool[,] { { true, false, true },
                                    { false, true, true },
                                    { true, true, true } };
            graph1 = new GraphWithRobots(matrix1, new int[] { 0, 2 });
            matrix2 = new bool[,] { { true, false, true, false},
                                    { false, true, true, true},
                                    { true, true, true, false},
                                    { false, true, false, true} };
            graph2 = new GraphWithRobots(matrix2, new int[] { 0, 1, 2, 3});
        }

        [TestMethod]
        public void NodesCanBeReachedFromThisTest1()
        {
            var list0 = graph1.NodesCanBeReachedFromThis(0);
            var list2 = graph1.NodesCanBeReachedFromThis(2);
            Assert.AreEqual(2, list0.Count);
            Assert.AreEqual(1, list2.Count);
        }

        [TestMethod]
        public void NodesCanBeReachedFromThisTest2()
        {
            var list0 = graph2.NodesCanBeReachedFromThis(0);
            var list2 = graph2.NodesCanBeReachedFromThis(2);
            Assert.AreEqual(2, list0.Count);
            Assert.AreEqual(2, list2.Count);
            Assert.IsTrue(list2.Contains(3));
        }

        [TestMethod]
        public void IsAllDamagedTest1()
        {
            Assert.IsFalse(graph1.IsAllDamaged());
            Assert.IsTrue(graph2.IsAllDamaged());
        }
    }
}
