using Microsoft.VisualStudio.TestTools.UnitTesting;
using MyPaint.Model;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyPaint.Model.Tests
{
    [TestClass]
    public class LineFactoryTests
    {
        private Point point1;

        private Point point2;

        [TestInitialize]
        public void TestInit()
        {
            point1 = new Point(10, 10);
            point2 = new Point(20, 20);
        }
            
        [TestMethod]
        public void CreateLineTest()
        {
            var line = LineFactory.CreateLine(point1, point2);
            Assert.AreEqual(point1, line.FirstEdge);
            Assert.AreEqual(point2, line.SecondEdge);
        }

        [TestMethod]
        public void CreateLineWithGivenIdTest()
        {
            int id = 2;
            var line = LineFactory.CreateLineWithGivenId(id, point1, point2);
            Assert.AreEqual(point1, line.FirstEdge);
            Assert.AreEqual(point2, line.SecondEdge);
            Assert.AreEqual(2, line.Id);
        }
    }
}