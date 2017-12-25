using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CompNet.Tests
{
    [TestClass]
    public class NetTests
    {
        private Net net;

        private IMachine[] computers;

        private bool[,] matrix;

        // Note: Linux probability - 0.4, Mac - 0.2 , Windows - 0.8
        [TestInitialize]
        public void TestInit()
        {
            matrix = new bool[,] { { true, false, true },
                                       { false, true, true },
                                       { true, true, true } };
            computers = new IMachine[3];
            var factory = new CompFactory();
            computers[0] = factory.CreateComp(OS.Linux, true);
            computers[1] = factory.CreateComp(OS.Mac, false);
            computers[2] = factory.CreateComp(OS.Windows, false);
            var rand = new FakeRandom();
            net = new Net(computers, matrix, rand);
        }

        [TestMethod]
        public void NewTurnTest1()
        {
            net.NewTurn();
            var comps = net.InfectedComputers;
            Assert.AreEqual(true, comps[0]);
            Assert.AreEqual(false, comps[1]);
            Assert.AreEqual(true, comps[2]);
            net.NewTurn();
            Assert.AreEqual(false, comps[1]);
        }

        [TestMethod]
        public void NewTurnTest2()
        {
            matrix = new bool[,] { { true, true, false },
                                   { false, true, false },
                                   { false, false, true } };
            var rand = new FakeRandom(new double[1] { 0.2 });
            var net = new Net(computers, matrix, rand);
            net.NewTurn();
            var comps = net.InfectedComputers;
            Assert.AreEqual(true, comps[0]);
            Assert.AreEqual(true, comps[1]);
            Assert.AreEqual(false, comps[2]);
            net.NewTurn();
            Assert.AreEqual(false, comps[2]);
        }

        [TestMethod]
        public void IsEndOfProcessTest1()
        {
            var rand = new FakeRandom(new double[1] { 0.2 });
            var net = new Net(computers, matrix, rand);
            net.NewTurn();
            Assert.IsFalse(net.IsEndOfProcess());
            net.NewTurn();
            Assert.IsTrue(net.IsEndOfProcess());
        }

        [TestMethod]
        public void IsEndOfProcessTest2()
        {
            var rand = new FakeRandom(new double[1] { 0.2 });
            var comp = new Computer("InvicibleOS", 0, false);
            computers = new Computer[1] { comp };
            matrix = new bool[,] { { true } };
            var net = new Net(computers, matrix, rand);
            Assert.IsTrue(net.IsEndOfProcess());
        }
    }
}