using System;
using System.Drawing;
using MyPaint.Model;

namespace MyPaint.Controllers
{
    public class Controller
    {
        private Scene scene;

        private int numberOfClick = 0;

        private bool movingLineMode = false;

        private TemporaryLineHandler tempLineHandler;

        private bool isDeleteMode = false;

        public Controller(Scene scene, TemporaryLineHandler temporaryLineHandler)
        {
            this.scene = scene;
            this.tempLineHandler = temporaryLineHandler;
        }

        public void HandleClick(Point location)
        {
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
                        scene.AddNewLine(tempLineHandler.TemporaryLine.FirstEdge, location);
                        break;
                }
            }
            else
            {
                LineDeleting(location);
            }
        }

        public void SetDeleteMode() => isDeleteMode = true;

        private void LineDeleting(Point location)
        {
            isDeleteMode = false;
            int idToDelete = -1;
            foreach (var line in scene.Lines)
            {
                if (line.PointNearBy(location) != 0)
                {
                    idToDelete = line.Id;
                    break;
                }
            }
            if (idToDelete > 0)
            {
                scene.RemoveLine(idToDelete);
            }
        }

        public void HandleMouseUp(Point location)
        {
            if (movingLineMode)
            {
                numberOfClick = 0;
                movingLineMode = false;
                var point1 = tempLineHandler.TemporaryLine.FirstEdge;
                var point2 = location; ;
                scene.MoveLine(tempLineHandler.IdToHide, point1, point2);
                tempLineHandler.SetIdToHide(-1);
                tempLineHandler.SetHidden();
                return;
            }
        }

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
