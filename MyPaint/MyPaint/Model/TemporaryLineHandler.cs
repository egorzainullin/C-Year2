using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyPaint.Model
{
    public class TemporaryLineHandler
    {
        public event EventHandler LineUpdated;

        private void ThrowUpdateEvent() => LineUpdated?.Invoke(this, EventArgs.Empty);

        public ILine TemporaryLine { get; private set; }

        public TemporaryLineHandler()
        {
            TemporaryLine = LineFactory.CreateLine(new Point(0, 0), new Point(0, 0));
        }

        public bool IsHidden { get; private set; }

        public void SetVisible()
        {
            IsHidden = false;
            ThrowUpdateEvent();
        }

        public void SetHidden()
        {
            IsHidden = true;
            ThrowUpdateEvent();
        }

        public void SetFirstEdge(Point point)
        {
            TemporaryLine.SetFirstEdge(point);
        }

        public void SetSecondEdge(Point point)
        {
            TemporaryLine.SetSecondEdge(point);
            ThrowUpdateEvent();
        }
    }
}
