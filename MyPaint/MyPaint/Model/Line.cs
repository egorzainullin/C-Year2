using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Threading.Tasks;

namespace MyPaint.Model.FactoryComponent
{
    /// <summary>
    /// This class represents a line on scene
    /// </summary>
    internal class Line : ILine
    {
        /// <summary>
        /// Gets line's first edge 
        /// </summary>
        public Point FirstEdge { get; private set; }

        /// <summary>
        /// Sets a new value to 1st edge
        /// </summary>
        /// <param name="point">New value</param>
        public void SetFirstEdge(Point point) => FirstEdge = point;

        /// <summary>
        /// Get's line second edge
        /// </summary>
        public Point SecondEdge { get; private set; }
        
        /// <summary>
        /// Sets new value to 2nd edge
        /// </summary>
        /// <param name="point">New value</param>
        public void SetSecondEdge(Point point) => SecondEdge = point;

        /// <summary>
        /// Calculates square distance between points
        /// </summary>
        /// <param name="point1">1st point</param>
        /// <param name="point2">2nd point</param>
        /// <returns></returns>
        private static int SquareDistance(Point point1, Point point2)
        {
            int x = point1.X - point2.X;
            int y = point1.Y - point2.Y;
            return x * x + y * y;
        }

        /// <summary>
        /// Calculates scalar product
        /// </summary>
        /// <param name="vector1">1st vector</param>
        /// <param name="vector2">2nd vector</param>
        /// <returns></returns>
        private static int GetScalarProduct(Point vector1, Point vector2) => vector1.X * vector2.X + vector1.Y * vector2.Y;

        /// <summary>
        /// Finds distance from point to this line
        /// </summary>
        /// <param name="point">Point calculate from</param>
        /// <returns>Calculated distance</returns>
        private double DistanceToLine(Point point)
        {
            int x = this.SecondEdge.X - this.FirstEdge.X;
            int y = this.SecondEdge.Y - this.FirstEdge.Y;
            var vectorOfLine = new Point(x, y);
            if (vectorOfLine.X == 0 && vectorOfLine.Y == 0)
            {
                return Math.Sqrt(SquareDistance(point, this.SecondEdge));
            }
            var vector = new Point(point.X - this.FirstEdge.X, point.Y - this.FirstEdge.Y);
            int squareProduct = GetScalarProduct(vectorOfLine, vectorOfLine) * GetScalarProduct(vector, vector);
            int squareScolarProduct = GetScalarProduct(vectorOfLine, vector) * GetScalarProduct(vectorOfLine, vector);
            // (|a||b|)^2 - (a * b) ^ 2 
            int SquareInSquare = squareProduct - squareScolarProduct;
            return Math.Sqrt(SquareInSquare / GetScalarProduct(vectorOfLine, vectorOfLine));
        }

        /// <summary>
        /// Checks is point nearby this line
        /// </summary>
        /// <param name="point">Point to check</param>
        /// <returns>1, 2 if near 1st or 2nd edge, 3 - if near line, but not edges, 0 - otherwise</returns>
        public int PointNearBy(Point point)
        {
            var firstEdge = this.FirstEdge;
            var secondEdge = this.SecondEdge;
            if (SquareDistance(firstEdge, point) < 9)
            {
                return 1;
            }
            if (SquareDistance(secondEdge, point) < 9)
            {
                return 2;
            }
            if (DistanceToLine(point) < 3)
            {
                return 3;
            }
            return 0;
        }

        /// <summary>
        /// Gets line's id
        /// </summary>
        public int Id { get; }

        /// <summary>
        /// Initializes a new instance of <see cref="Line"/> class
        /// </summary>
        /// <param name="point1">1st edge</param>
        /// <param name="point2">2nd edge</param>
        public Line(Point point1, Point point2)
        {
            this.FirstEdge = point1;
            this.SecondEdge = point2;
        }

        /// <summary>
        /// Initializes a new instance of <see cref="Line"/> class
        /// </summary>
        /// <param name="point1">1st edge</param>
        /// <param name="point2">2st edge</param>
        /// <param name="id">id</param>
        public Line(Point point1, Point point2, int id)
        {
            this.FirstEdge = point1;
            this.SecondEdge = point2;
            this.Id = id;
        }
    }
}
