using System;
using System.Drawing;
using MyPaint.Model;

namespace MyPaint.Controllers
{
    /// <summary>
    /// This class helps to auto register actions on scene in undo stack 
    /// </summary>
    public class SceneCommandRegister
    {
        /// <summary>
        /// Scene to watch
        /// </summary>
        private Scene scene;

        /// <summary>
        /// Stack to add actions
        /// </summary>
        private IUndoStack stack = UndoStackFactory.GetUndoStack();

        /// <summary>
        /// Initializes a new instance of <see cref="SceneCommandRegister"/> 
        /// </summary>
        /// <param name="scene">Scene to register commands</param>
        public SceneCommandRegister(Scene scene) => this.scene = scene;

        /// <summary>
        /// This class implements ICommand interface
        /// </summary>
        private class Command : ICommand
        {
            /// <summary>
            /// Action to do
            /// </summary>
            private Action action;

            /// <summary>
            /// Action to undo
            /// </summary>
            private Action undoAction;

            /// <summary>
            /// Execute action
            /// </summary>
            public void Execute() => action?.Invoke(); 

            /// <summary>
            /// Undo action
            /// </summary>
            public void Undo() => undoAction?.Invoke();

            /// <summary>
            /// Initializes a new instance of <see cref="Command"/> 
            /// </summary>
            /// <param name="action">Action to do</param>
            /// <param name="undoAction">Action to undo</param>
            public Command(Action action, Action undoAction)
            {
                this.action += action;
                this.undoAction += undoAction;
            }
        }

        /// <summary>
        /// Register that line was added
        /// </summary>
        /// <param name="id">Id of added line</param>
        /// <param name="point1">1st edge</param>
        /// <param name="point2">2nd edge</param>
        public void RegisterAddingNewLine(int id, Point point1, Point point2)
        {
            Action action = () => { scene.AddNewLineWithGivenId(id, point1, point2); };
            Action undoAction = () => { scene.RemoveLine(id); };
            var command = new Command(action, undoAction);
            stack.AddCommand(command);
        }
        
        /// <summary>
        /// Register that some line was deleted
        /// </summary>
        /// <param name="givenId">Id of deleted line</param>
        /// <param name="point1">1st edge</param>
        /// <param name="point2">2nd edge</param>
        public void RegisterDeletingLine(int givenId, Point point1, Point point2)
        {
            Action action = () => { scene.RemoveLine(givenId); };
            Action undoAction = () => { scene.AddNewLineWithGivenId(givenId, point1, point2); };
            var command = new Command(action, undoAction);
            stack.AddCommand(command);
        }
        
        /// <summary>
        /// Register that line was moved
        /// </summary>
        /// <param name="id">Id of moved line</param>
        /// <param name="oldPoint1">1st edge of the old position</param>
        /// <param name="oldPoint2">2nd edge of the old position</param>
        /// <param name="newPoint1">1st edge of the new position</param>
        /// <param name="newPoint2">2nd edge of the new position</param>
        public void RegisterLineMoving(int id, Point oldPoint1, Point oldPoint2, Point newPoint1, Point newPoint2)
        {
            Action action = () => scene.MoveLine(id, newPoint1, newPoint2);
            Action undoAction = () => scene.MoveLine(id, oldPoint1, oldPoint2);
            var command = new Command(action, undoAction);
            stack.AddCommand(command);
        }
    }
}
