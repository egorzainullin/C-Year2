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
        /// <summary>
        /// Scene
        /// </summary>
        private Scene scene;

        /// <summary>
        /// Controller
        /// </summary>
        private Controller controller;

        /// <summary>
        /// This class is responsible for temporary lines
        /// </summary>
        private TemporaryLineHandler tempLineHandler;

        /// <summary>
        /// Undo stack
        /// </summary>
        private IUndoStack undoStack = UndoStackFactory.GetUndoStack();

        /// <summary>
        /// Buttons require blocking
        /// </summary>
        private bool IsButtonNeedsBlocking => !tempLineHandler.IsHidden;

        /// <summary>
        /// Which line should be hided
        /// </summary>
        private int IdToHide => tempLineHandler.IdToHide;

        /// <summary>
        /// Gets line to present on scene
        /// </summary>
        private IEnumerable<ILine> Lines => scene.Lines;

        /// <summary>
        /// Initialize Editor 
        /// </summary>
        public Editor()
        {
            InitializeComponent();
            this.scene = new Scene();
            this.scene.SceneUpdated += OnSceneUpdated;
            this.tempLineHandler = new TemporaryLineHandler();
            this.tempLineHandler.LineUpdated += OnTemporaryLineUpdated;
            this.controller = new Controller(scene, tempLineHandler);
        }

        /// <summary>
        /// Block buttons from clicking
        /// </summary>
        private void BlockButtons()
        {
            deleteButton.Enabled = false;
            undoButton.Enabled = false;
            redoButton.Enabled = false;
        }

        /// <summary>
        /// Allow user to click buttons
        /// </summary>
        private void EnableButtons()
        {
            deleteButton.Enabled = true;
            undoButton.Enabled = true;
            redoButton.Enabled = true;
        }
        
        /// <summary>
        /// Redraws scene view when real scene has been changed
        /// </summary>
        /// <param name="sender">Sender</param>
        /// <param name="args">Event args</param>
        private void OnSceneUpdated(object sender, EventArgs args) => Redraw();

        /// <summary>
        /// Redraws scene when temp line has been changed
        /// </summary>
        /// <param name="sender">Sender</param>
        /// <param name="args">Event args</param>
        private void OnTemporaryLineUpdated(object sender, EventArgs args) => Redraw();

        /// <summary>
        /// Redraws scene
        /// </summary>
        private void Redraw() => this.picture.Invalidate();

        /// <summary>
        /// Redraws picture
        /// </summary>
        /// <param name="sender">Sender</param>
        /// <param name="e">Args</param>
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
                        graphics.DrawEllipse(pen, new Rectangle(line.FirstEdge, new Size(1, 1)));
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

        /// <summary>
        /// Handles button's click
        /// </summary>
        /// <param name="sender">Sender</param>
        /// <param name="e">Args</param>
        private void OnPictureMouseDown(object sender, MouseEventArgs e)
        {
            controller.HandleClick(e.Location);
            BlockButtons();
        }

        /// <summary>
        /// Handles mouse movement
        /// </summary>
        /// <param name="sender">Sender</param>
        /// <param name="e">Args</param>
        private void OnPictureMouseMove(object sender, MouseEventArgs e)
        {
            controller.HandleMove(e.Location);
            if (IsButtonNeedsBlocking)
            {
                BlockButtons();
            }
        }

        /// <summary>
        /// Handles releasing button
        /// </summary>
        /// <param name="sender">Sender</param>
        /// <param name="e">Args</param>
        private void OnPictureMouseUp(object sender, MouseEventArgs e)
        {
            controller.HandleMouseUp(e.Location);
            EnableButtons();
        }

        /// <summary>
        /// Handles click on delete button
        /// </summary>
        /// <param name="sender">Sender</param>
        /// <param name="e">Args</param>
        private void OnDeleteButtonClick(object sender, EventArgs e) => controller.SetDeleteMode();

        /// <summary>
        /// Handles click on undo button
        /// </summary>
        /// <param name="sender">Sender</param>
        /// <param name="e">Args</param>
        private void OnUndoButtonClick(object sender, EventArgs e) => undoStack.Undo();

        /// <summary>
        /// Handles redo button click
        /// </summary>
        /// <param name="sender">Sender</param>
        /// <param name="e">Args</param>
        private void OnRedoButtonClick(object sender, EventArgs e) => undoStack.Redo();
    }
}
