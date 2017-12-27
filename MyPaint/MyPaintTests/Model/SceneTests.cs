using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Drawing;
using System.Linq;

namespace MyPaint.Model.Tests
{
    [TestClass]
    public class SceneTests
    {
        private Scene scene;

        private Point point1;

        private Point point2;

        private Point point3;

        private Point point4;

        [TestInitialize]
        public void InitTest()
        {
            scene = new Scene();
            point1 = new Point(0, 0);
            point2 = new Point(10, 10);
            point3 = new Point(20, 20);
            point4 = new Point(30, 30);
        }

        [TestMethod]
        public void GetLineByIdTest()
        {
            scene.AddNewLineWithGivenId(0, point1, point2);
            Assert.IsNotNull(scene.GetLineById(0));
        }

        [TestMethod]
        public void AddNewLineTest()
        {
            scene.AddNewLine(point1, point2);
            Assert.AreEqual(1, scene.Lines.ToList().Count);
        }

        [TestMethod]
        public void AddNewLineWithGivenIdTest()
        {
            scene.AddNewLineWithGivenId(0, point1, point2);
            var line = scene.Lines.ToList()[0];
            Assert.AreEqual(0, line.Id);
            Assert.AreEqual(point1, line.FirstEdge);
        }

        [TestMethod]
        public void MoveLineTest()
        {
            scene.AddNewLine(point1, point2);
            var line = scene.Lines.ToList()[0];
            int id = line.Id;
            scene.MoveLine(id, point3, point4);
            Assert.AreEqual(point3, line.FirstEdge);
            Assert.AreEqual(point4, line.SecondEdge);
        }

        [TestMethod]
        public void RemoveLineTest()
        {
            scene.AddNewLine(point1, point2);
            var line = scene.Lines.ToList()[0];
            int id = line.Id;
            scene.RemoveLine(id);
            var list = scene.Lines.ToList();
            Assert.AreEqual(0, list.Count);
        }
    }
}