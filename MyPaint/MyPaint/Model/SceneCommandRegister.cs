using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyPaint.Model
{
    public class SceneCommandRegister
    {
        private Scene scene;

        private IUndoStack stack = UndoStackFactory.CreateUndoStack();

        public SceneCommandRegister(Scene scene)
        {
            this.scene = scene;
        }

        private class Command : ICommand
        {
            private Action action;

            private Action undoAction;

            public void Execute() => action?.Invoke(); 

            public void Undo() => undoAction?.Invoke();

            public Command(Action action, Action undoAction)
            {
                this.action += action;
                this.undoAction += undoAction;
            }
        }

        public void RegisterAddingNewLine(int id, Point point1, Point point2)
        {
            Action action = () => { scene.AddNewLineWithGivenId(id, point1, point2); };
            Action undoAction = () => { scene.RemoveLine(id); };
            var command = new Command(action, undoAction);
            stack.AddCommand(command);
        }

        public void RegisterDeletingLine(int givenId, Point point1, Point point2)
        {
            Action action = () => { scene.RemoveLine(givenId); };
            Action undoAction = () => { scene.AddNewLineWithGivenId(givenId, point1, point2); };
            var command = new Command(action, undoAction);
            stack.AddCommand(command);
        }

        public void RegisterLineMoving(int id, Point oldPoint1, Point oldPoint2, Point newPoint1, Point newPoint2)
        {
            Action action = () => scene.MoveLine(id, newPoint1, newPoint2);
            Action undoAction = () => scene.MoveLine(id, oldPoint1, oldPoint2);
            var command = new Command(action, undoAction);
            stack.AddCommand(command);
        }
    }
}
