using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Drawing;

namespace MyPaint.Model.Tests
{
    [TestClass]
    public class TemporaryLineHandlerTests
    {
        private TemporaryLineHandler tempLineHandler;

        private Point point1;

        private Point point2;

        [TestInitialize]
        public void InitTest()
        {
            tempLineHandler = new TemporaryLineHandler();
            point1 = new Point(10, 10);
            point2 = new Point(20, 20);
        }

        [TestMethod]
        public void SetIdToHideTest()
        {
            tempLineHandler.SetIdToHide(2);
            Assert.AreEqual(2, tempLineHandler.IdToHide);
        }

        [TestMethod]
        public void SetVisibleTest()
        {
            tempLineHandler.SetVisible();
            Assert.IsFalse(tempLineHandler.IsHidden);
        }

        [TestMethod]
        public void SetHiddenTest()
        {
            tempLineHandler.SetHidden();
            Assert.IsTrue(tempLineHandler.IsHidden);
        }

        [TestMethod]
        public void SetFirstEdgeTest()
        {
            tempLineHandler.SetFirstEdge(point1);
            Assert.AreEqual(point1, tempLineHandler.TemporaryLine.FirstEdge);
        }

        [TestMethod]
        public void SetSecondEdgeTest()
        {
            tempLineHandler.SetSecondEdge(point2);
            Assert.AreEqual(point2, tempLineHandler.TemporaryLine.SecondEdge);
        }
    }
}