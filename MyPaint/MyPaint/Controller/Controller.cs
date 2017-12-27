using System;
using System.Drawing;
using MyPaint.Model;

namespace MyPaint.Controllers
{
    /// <summary>
    /// This class handles button, mouse clicks, movement and so on
    /// </summary>
    public class Controller
    {
        /// <summary>
        /// Scene of editor
        /// </summary>
        private Scene scene;

        /// <summary>
        /// Number of clicks
        /// </summary>
        private int numberOfClick = 0;

        /// <summary>
        /// Is user trying to move line
        /// </summary>
        private bool movingLineMode = false;

        /// <summary>
        /// Class that adds temp line on scene
        /// </summary>
        private TemporaryLineHandler tempLineHandler;

        /// <summary>
        /// Registers scene command for undo stack
        /// </summary>
        private SceneCommandRegister register;

        /// <summary>
        /// Is user trying to delete something on scene
        /// </summary>
        private bool isDeleteMode = false;

        /// <summary>
        /// Initializes a new instance of <see cref="Controller"/> class
        /// </summary>
        /// <param name="scene">Scene of editor</param>
        /// <param name="temporaryLineHandler">Class that adds temp lines</param>
        public Controller(Scene scene, TemporaryLineHandler temporaryLineHandler)
        {
            this.scene = scene;
            this.tempLineHandler = temporaryLineHandler;
            this.register = new SceneCommandRegister(scene);
        }

        /// <summary>
        /// Adding new line on scene
        /// </summary>
        /// <param name="location">Current location of mouse</param>
        private void AddingNewLine(Point location)
        {
            switch (numberOfClick)
            {
                case 0:
                    numberOfClick = 1;
                    tempLineHandler.SetFirstEdge(location);
                    tempLineHandler.SetSecondEdge(location);
                    tempLineHandler.SetVisible();
                    break;
                case 1:
                    numberOfClick = 0;
                    tempLineHandler.SetHidden();
                    var id = scene.AddNewLine(tempLineHandler.TemporaryLine.FirstEdge, location);
                    register.RegisterAddingNewLine(id, tempLineHandler.TemporaryLine.FirstEdge, location);
                    break;
            }
        }

        /// <summary>
        /// Handles click on scene
        /// </summary>
        /// <param name="location">Location of click</param>
        public void HandleClick(Point location)
        {
            var stack = UndoStackFactory.CreateUndoStack();
            stack.ResetRedo();
            if (!isDeleteMode)
            {
                if (numberOfClick == 0)
                {
                    foreach (var line in scene.Lines)
                    {
                        if (line.PointNearBy(location) == 1)
                        {
                            numberOfClick = 1;
                            movingLineMode = true;
                            tempLineHandler.SetIdToHide(line.Id);
                            tempLineHandler.SetFirstEdge(line.SecondEdge);
                            tempLineHandler.SetSecondEdge(location);
                            tempLineHandler.SetVisible();
                            return;
                        }
                        if (line.PointNearBy(location) == 2)
                        {
                            numberOfClick = 1;
                            movingLineMode = true;
                            tempLineHandler.SetIdToHide(line.Id);
                            tempLineHandler.SetFirstEdge(line.FirstEdge);
                            tempLineHandler.SetSecondEdge(location);
                            tempLineHandler.SetVisible();
                            return;
                        }
                    }
                }
                AddingNewLine(location);
            }
            else
            {
                LineDeleting(location);
            }
        }

        /// <summary>
        /// Sets delete mode
        /// </summary>
        public void SetDeleteMode() => isDeleteMode = true;

        /// <summary>
        /// Deletes line nearby this location
        /// </summary>
        /// <param name="location">Mouse's location</param>
        private void LineDeleting(Point location)
        {
            isDeleteMode = false;
            int idToDelete = -1;
            Point point1 = new Point();
            Point point2 = new Point();
            foreach (var line in scene.Lines)
            {
                if (line.PointNearBy(location) != 0)
                {
                    idToDelete = line.Id;
                    point1 = line.FirstEdge;
                    point2 = line.SecondEdge;
                    break;
                }
            }
            if (idToDelete > 0)
            {
                scene.RemoveLine(idToDelete);
                register.RegisterDeletingLine(idToDelete, point1, point2);
            }
        }

        /// <summary>
        /// What to do when mouse button was released
        /// </summary>
        /// <param name="location">Current location of mouse</param>
        public void HandleMouseUp(Point location)
        {
            if (movingLineMode)
            {
                numberOfClick = 0;
                movingLineMode = false;
                var point1 = tempLineHandler.TemporaryLine.FirstEdge;
                var point2 = location;
                var line = scene.GetLineById(tempLineHandler.IdToHide);
                var oldPoint1 = line.FirstEdge;
                var oldPoint2 = line.SecondEdge;
                scene.MoveLine(tempLineHandler.IdToHide, point1, point2);
                register.RegisterLineMoving(tempLineHandler.IdToHide, oldPoint1, oldPoint2, point1, point2);
                tempLineHandler.SetIdToHide(-1);
                tempLineHandler.SetHidden();
                return;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="location"></param>
        public void HandleMove(Point location)
        {
            if (numberOfClick == 1)
            {
                tempLineHandler.TemporaryLine.SetSecondEdge(location);
                tempLineHandler.SetVisible();
            }
        }
    }
}
