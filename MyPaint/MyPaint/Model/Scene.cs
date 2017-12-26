using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyPaint.Model
{
    public class Scene
    {
        public event EventHandler<EventArgs> SceneUpdated;

        private void ThrowUpdateEvent() => SceneUpdated?.Invoke(this, EventArgs.Empty);

        public IEnumerable<ILine> Lines => linesDictionary.Values;

        private Dictionary<int, ILine> linesDictionary = new Dictionary<int, ILine>();

        public void AddNewLine(Point point1, Point point2)
        {
            var line = LineFactory.CreateLine(point1, point2);
            linesDictionary.Add(line.Id, line);
            ThrowUpdateEvent();
        }

        public void MoveLine(int id, Point point1, Point point2)
        {
            if (!linesDictionary.ContainsKey(id))
            {
                throw new InvalidOperationException("this id does not exist");
            }
            linesDictionary[id].SetFirstEdge(point1);
            linesDictionary[id].SetSecondEdge(point2);
            ThrowUpdateEvent();
        }

        public void RemoveLine(int id)
        {
            linesDictionary.Remove(id);
            ThrowUpdateEvent();
        }
    }
}
