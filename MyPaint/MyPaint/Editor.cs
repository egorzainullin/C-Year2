using MyPaint.Model;
using MyPaint.Controllers;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace MyPaint
{
    public partial class Editor : Form
    {
        private Scene scene;

        private Controller controller;

        private TemporaryLineHandler tempLineHandler;

        private IUndoStack undoStack = UndoStackFactory.CreateUndoStack();

        private bool IsButtonNeedsBlocking => !tempLineHandler.IsHidden;

        private int IdToHide => tempLineHandler.IdToHide;

        private IEnumerable<ILine> Lines => scene.Lines;

        public Editor()
        {
            InitializeComponent();
            this.scene = new Scene();
            this.scene.SceneUpdated += OnSceneUpdated;
            this.tempLineHandler = new TemporaryLineHandler();
            this.tempLineHandler.LineUpdated += OnTemporaryLineUpdated;
            this.controller = new Controller(scene, tempLineHandler);
        }

        private void BlockButtons()
        {
            deleteButton.Enabled = false;
            undoButton.Enabled = false;
            redoButton.Enabled = false;
        }

        private void EnableButtons()
        {
            deleteButton.Enabled = true;
            undoButton.Enabled = true;
            redoButton.Enabled = true;
        }

        private void OnSceneUpdated(object sender, EventArgs args) => Redraw();

        private void OnTemporaryLineUpdated(object sender, EventArgs args) => Redraw();

        private void Redraw() => this.picture.Invalidate();

        private void OnPicturePaint(object sender, PaintEventArgs e)
        {
            var graphics = e.Graphics;
            var pen = new Pen(Pens.Black.Color)
            {
                Width = 3
            };
            foreach (var line in Lines)
            {
                if (line.Id != IdToHide)
                {
                    if (line.FirstEdge == line.SecondEdge)
                    {
                        graphics.DrawEllipse(pen, new Rectangle(line.FirstEdge, new Size(1,1)));
                    }
                    else
                    {
                        graphics.DrawLine(pen, line.FirstEdge, line.SecondEdge);
                    }
                }
            }
            if (!tempLineHandler.IsHidden)
            {
                var point1 = tempLineHandler.TemporaryLine.FirstEdge;
                var point2 = tempLineHandler.TemporaryLine.SecondEdge;
                graphics.DrawLine(pen, point1, point2);
            }
        }

        private void OnPictureMouseDown(object sender, MouseEventArgs e)
        {
            controller.HandleClick(e.Location);
            BlockButtons();
        }

        private void OnPictureMouseMove(object sender, MouseEventArgs e)
        {
            controller.HandleMove(e.Location);
            if (IsButtonNeedsBlocking)
            {
                BlockButtons();
            }
        }

        private void OnPictureMouseUp(object sender, MouseEventArgs e)
        {
            controller.HandleMouseUp(e.Location);
            EnableButtons();
        }

        private void OnDeleteButtonClick(object sender, EventArgs e) => controller.SetDeleteMode();

        private void OnUndoButtonClick(object sender, EventArgs e) => undoStack.Undo();

        private void OnRedoButtonClick(object sender, EventArgs e) => undoStack.Redo();
    }
}
