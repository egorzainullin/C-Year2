using Microsoft.VisualStudio.TestTools.UnitTesting;
using CompNet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompNet.Tests
{
    [TestClass]
    public class CompFactoryTests
    {
        [TestMethod]
        public void CreateCompTest()
        {
            var factory = new CompFactory();
            var comp1 = factory.CreateComp(OS.Linux, true);
            Assert.AreEqual("Linux", comp1.TypeOfOS);
            Assert.IsTrue(comp1.IsInfected);
            var comp2 = factory.CreateComp(OS.Mac, false);
            Assert.AreEqual("Mac", comp2.TypeOfOS);
            Assert.IsFalse(comp2.IsInfected);
            var comp3 = factory.CreateComp(OS.Windows, true);
            Assert.AreEqual("Windows", comp3.TypeOfOS);
            Assert.IsTrue(comp3.IsInfected);
        }
    }
}