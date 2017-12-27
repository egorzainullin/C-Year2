using MyPaint.Model.FactoryComponent;
using System;
using System.Drawing;

namespace MyPaint.Model
{
    /// <summary>
    /// Factory that creates lines with given parameters
    /// </summary>
    public class LineFactory
    {
        /// <summary>
        /// Internal counter necessary for keeping id unique
        /// </summary>
        private static int idCounter = -1;

        /// <summary>
        /// Create a new line with given parameters
        /// </summary>
        /// <param name="point1">1st edge</param>
        /// <param name="point2">2nd edge</param>
        /// <returns>CreatedLine</returns>
        public static ILine CreateLine(Point point1, Point point2)
        {
            ++idCounter;
            return new Line(point1, point2, idCounter);
        }
        /// <summary>
        /// Create a new line with given parameters
        /// </summary>
        /// <param name="id">Line's ID</param>
        /// <param name="point1">1st edge</param>
        /// <param name="point2">2nd edge</param>
        /// <returns>Created line</returns>
        public static ILine CreateLineWithGivenId(int id, Point point1, Point point2)
        {
            idCounter = Math.Max(idCounter + 1, id + 1);
            return new Line(point1, point2, id);
        }
    }
}
