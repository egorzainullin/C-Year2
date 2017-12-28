using Microsoft.VisualStudio.TestTools.UnitTesting;
using MyPaint.Model;
using System.Drawing;
using System.Linq;

namespace MyPaint.Controllers.Tests
{
    [TestClass]
    public class SceneCommandRegisterTests
    {
        private Scene scene;

        private SceneCommandRegister register;

        private IUndoStack stack = UndoStackFactory.GetUndoStack();

        private ILine line1;

        private ILine line2;

        [TestInitialize]
        public void TestInit()
        {
            scene = new Scene();
            register = new SceneCommandRegister(scene);
            var point1 = new Point(0, 0);
            var point2 = new Point(10, 10);
            line1 = LineFactory.CreateLine(point1, point2);
            var point3 = new Point(20, 20);
            var point4 = new Point(30, 30);
            line2 = LineFactory.CreateLine(point3, point4);
        }

        [TestMethod]
        public void RegisterAddingNewLineTest()
        {
            var id = scene.AddNewLine(line1.FirstEdge, line1.SecondEdge);
            register.RegisterAddingNewLine(id, line1.FirstEdge, line1.SecondEdge);
            stack.Undo();
            Assert.AreEqual(0, scene.Lines.ToList().Count);
            stack.Redo();
            Assert.AreEqual(1, scene.Lines.ToList().Count);
        }

        [TestMethod]
        public void RegisterDeletingLineTest()
        {
            var id = scene.AddNewLine(line1.FirstEdge, line1.SecondEdge);
            scene.RemoveLine(id);
            register.RegisterDeletingLine(id, line1.FirstEdge, line1.SecondEdge);
            stack.Undo();
            Assert.AreEqual(1, scene.Lines.ToList().Count);
            stack.Redo();
            Assert.AreEqual(0, scene.Lines.ToList().Count);
        }

        [TestMethod]
        public void RegisterLineMovingTest()
        {
            var id = scene.AddNewLine(line1.FirstEdge, line1.SecondEdge);
            scene.MoveLine(id, line2.FirstEdge, line2.SecondEdge);
            register.RegisterLineMoving(id, line1.FirstEdge, line1.SecondEdge, line2.FirstEdge, line2.SecondEdge);
            stack.Undo();
            Assert.AreEqual(line1.FirstEdge, scene.Lines.ToList()[0].FirstEdge);
            stack.Redo();
            Assert.AreEqual(line2.FirstEdge, scene.Lines.ToList()[0].FirstEdge);
        }
    }
}