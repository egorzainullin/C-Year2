using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CompNet.Tests
{
    [TestClass]
    public class ComputerTests
    {
        [TestMethod]
        public void SetInfectedTest()
        {
            var comp = new Computer("Linux", 0, false);
            Assert.IsFalse(comp.IsInfected);
            comp.SetInfected();
            Assert.IsTrue(comp.IsInfected);
        }

        [TestMethod]
        public void ComputerTest()
        {
            var comp = new Computer("Linux", 0.2, false);
            Assert.AreEqual("Linux", comp.TypeOfOS);
            Assert.AreEqual(0.2, comp.ProbabilityOfInfection);
            Assert.AreEqual(false, comp.IsInfected);
        }
    }
}