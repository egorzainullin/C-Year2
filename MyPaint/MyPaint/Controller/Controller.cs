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

        public Controller(Scene scene, TemporaryLineHandler temporaryLineHandler)
        {
            this.scene = scene;
            this.tempLineHandler = temporaryLineHandler;
        }

        public void HandleClick(bool isDeleteMode, Point location)
        {
            if (!isDeleteMode)
            {
                switch (numberOfClick)
                {
                    case 0:
                        ++numberOfClick;
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
