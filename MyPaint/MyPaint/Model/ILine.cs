using System.Drawing;

namespace MyPaint.Model
{
    /// <summary>
    /// This is abstraction of line presented on scene
    /// </summary>
    public interface ILine
    {
        /// <summary>
        /// Gets the first edge of this line
        /// </summary>
        Point FirstEdge { get; }

        /// <summary>
        /// Gets the second edge of this line
        /// </summary>
        Point SecondEdge { get; }

        /// <summary>
        /// Sets new value to 1st edge
        /// </summary>
        /// <param name="point">New value to set</param>
        void SetFirstEdge(Point point);

        /// <summary>
        /// Sets new value to 2nd edge
        /// </summary>
        /// <param name="point">New value to set</param>
        void SetSecondEdge(Point point);

        /// <summary>
        /// Checks if point is near this line
        /// </summary>
        /// <param name="point">Point to check</param>
        /// <returns>1,2 if near the edges, 3, if it's near the line, but not edges, 0 otherwise</returns>
        int PointNearBy(Point point);

        /// <summary>
        /// Gets id of this line
        /// </summary>
        int Id { get; }
    }
}