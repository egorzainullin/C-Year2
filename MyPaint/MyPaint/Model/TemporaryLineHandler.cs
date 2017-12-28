using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyPaint.Model
{
    /// <summary>
    /// This class is responsible for temporary lines created while adding new one and moving
    /// </summary>
    public class TemporaryLineHandler
    {
        /// <summary>
        /// Event happens when line was changed
        /// </summary>
        public event EventHandler LineUpdated;

        /// <summary>
        /// Throw LineUpdated event
        /// </summary>
        private void ThrowUpdateEvent() => LineUpdated?.Invoke(this, EventArgs.Empty);

        /// <summary>
        /// If of the line which should be hidden because of moving
        /// </summary>
        public int IdToHide { get; private set; } = -1;

        /// <summary>
        /// Temp line to present on scene
        /// </summary>
        public ILine TemporaryLine { get; }

        /// <summary>
        /// Initializes a new instance of <see cref="TemporaryLineHandler"/> class
        /// </summary>
        public TemporaryLineHandler() => TemporaryLine = LineFactory.CreateLine(new Point(0, 0), new Point(0, 0));

        /// <summary>
        /// Sets id of line which shouldn't be presented on scene
        /// </summary>
        /// <param name="id"></param>
        public void SetIdToHide(int id)
        {
            IdToHide = id;
            ThrowUpdateEvent();
        }

        /// <summary>
        /// Is temp line hidden now
        /// </summary>
        public bool IsHidden { get; private set; }

        /// <summary>
        /// Sets temp line visible
        /// </summary>
        public void SetVisible()
        {
            IsHidden = false;
            ThrowUpdateEvent();
        }

        /// <summary>
        /// Sets temp line hidden
        /// </summary>
        public void SetHidden()
        {
            IsHidden = true;
            ThrowUpdateEvent();
        }

        /// <summary>
        /// Sets 1st edge of temp line
        /// </summary>
        /// <param name="point">New value</param>
        public void SetFirstEdge(Point point)
        {
            TemporaryLine.SetFirstEdge(point);
        }

        /// <summary>
        /// Sets 2nd edge of temp line
        /// </summary>
        /// <param name="point">New value</param>
        public void SetSecondEdge(Point point)
        {
            TemporaryLine.SetSecondEdge(point);
            ThrowUpdateEvent();
        }
    }
}
