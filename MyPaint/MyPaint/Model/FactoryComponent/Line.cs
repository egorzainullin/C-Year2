using System;
using System.Drawing;

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
        /// <returns>Calculated distance</returns>
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
        /// <returns>Calculated product</returns>
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
        /// Checks if point contains in necessary field 
        /// </summary>
        /// <returns>Is contained</returns>
        private bool IsInside(Point point)
        {
            int x = this.FirstEdge.X - this.SecondEdge.X;
            int y = this.FirstEdge.Y - this.SecondEdge.Y;
            var vector = new Point(x, y);
            var vector1 = new Point(point.X - this.FirstEdge.X, point.Y - this.FirstEdge.Y);
            var vector2 = new Point(point.X - this.SecondEdge.X, point.Y - this.SecondEdge.Y);
            return (Math.Sign(GetScalarProduct(vector, vector1)) * Math.Sign(GetScalarProduct(vector, vector2))) <= 0;
        }

        /// <summary>
        /// Checks is point nearby this line
        /// </summary>
        /// <param name="point">Point to check</param>
        /// <returns>State of point relatively to line</returns>
        public NearLineEnum PointNearBy(Point point)
        {
            var firstEdge = this.FirstEdge;
            var secondEdge = this.SecondEdge;
            var distanceNearBy = 3;
            if (SquareDistance(firstEdge, point) < distanceNearBy * distanceNearBy)
            {
                return NearLineEnum.NearFirstEdge;
            }
            if (SquareDistance(secondEdge, point) < distanceNearBy * distanceNearBy)
            {
                return NearLineEnum.NearSecondEdge;
            }
            if ((DistanceToLine(point) < distanceNearBy) && IsInside(point))
            {   
                return NearLineEnum.NearLineButNotNearEdges;
            }
            return NearLineEnum.NotNear;
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
