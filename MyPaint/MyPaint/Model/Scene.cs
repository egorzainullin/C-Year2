using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyPaint.Model
{
    /// <summary>
    /// This class keeps data about lines on scene and allows to add, move, delete elements on scene
    /// </summary>
    public class Scene
    {
        /// <summary>
        /// This event happens when something is changing on scene
        /// </summary>
        public event EventHandler<EventArgs> SceneUpdated;

        /// <summary>
        /// This method throws SceneUpdated event
        /// </summary>
        private void ThrowUpdateEvent() => SceneUpdated?.Invoke(this, EventArgs.Empty);

        /// <summary>
        /// Gets lines on scene
        /// </summary>
        public IEnumerable<ILine> Lines => linesDictionary.Values;

        /// <summary>
        /// Dictionary to keep lines and get them by their id
        /// </summary>
        private Dictionary<int, ILine> linesDictionary = new Dictionary<int, ILine>();

        /// <summary>
        /// Gets line by its id, if it does not exist returns null
        /// </summary>
        /// <param name="id">Id to find</param>
        /// <returns>Line with this id</returns>
        public ILine GetLineById(int id) => linesDictionary.ContainsKey(id) ? linesDictionary[id] : null;

        public int AddNewLine(Point point1, Point point2)
        {
            var line = LineFactory.CreateLine(point1, point2);
            linesDictionary.Add(line.Id, line);
            int id = line.Id;
            ThrowUpdateEvent();
            return id;
        }

        /// <summary>
        /// Creates line with given edges an id
        /// </summary>
        /// <param name="givenId">Id</param>
        /// <param name="point1">First edge</param>
        /// <param name="point2">Second edge</param>
        /// <exception cref="InvalidOperationException">Throws when id already exists</exception>
        public void AddNewLineWithGivenId(int givenId, Point point1, Point point2)
        {
            if (linesDictionary.ContainsKey(givenId))
            {
                throw new InvalidOperationException("This id already exists");
            }
            var line = LineFactory.CreateLineWithGivenId(givenId, point1, point2);
            linesDictionary.Add(givenId, line);
            ThrowUpdateEvent();
        }

        /// <summary>
        /// Move line into given position
        /// </summary>
        /// <param name="id">Id of line to move</param>
        /// <param name="point1">First new coordinates</param>
        /// <param name="point2">Second new coordinates</param>
        /// <exception cref="InvalidOperationException">This id does not exist</exception>
        public void MoveLine(int id, Point point1, Point point2)
        {
            if (!linesDictionary.ContainsKey(id))
            {
                throw new InvalidOperationException("this id does not exist");
            }
            var line = linesDictionary[id];
            line.SetFirstEdge(point1);
            line.SetSecondEdge(point2);
            ThrowUpdateEvent();
        }

        /// <summary>
        /// Removes line by its id
        /// </summary>
        /// <param name="id">Id of line to remove</param>
        public void RemoveLine(int id)
        {
            if (linesDictionary.ContainsKey(id))
            {
                linesDictionary.Remove(id);
                ThrowUpdateEvent();
            }
        }
    }
}
