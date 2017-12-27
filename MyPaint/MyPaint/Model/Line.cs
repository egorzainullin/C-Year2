using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Threading.Tasks;

namespace MyPaint.Model.FactoryComponent
{
    internal class Line : ILine
    {
        public Point FirstEdge { get; private set; }

        public void SetFirstEdge(Point point) => FirstEdge = point;

        public Point SecondEdge { get; private set; }
            
        public void SetSecondEdge(Point point) => SecondEdge = point;

        private static int SquareDistance(Point point1, Point point2)
        {
            int x = point1.X - point2.X;
            int y = point1.Y - point2.Y;
            return x * x + y * y;
        }

        private static int GetScolarProduct(Point point1, Point point2) => point1.X * point2.X + point1.Y * point2.Y;

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
            int squareProduct = GetScolarProduct(vectorOfLine, vectorOfLine) * GetScolarProduct(vector, vector);
            int squareScolarProduct = GetScolarProduct(vectorOfLine, vector) * GetScolarProduct(vectorOfLine, vector);
            // (|a||b|)^2 - (a * b) ^ 2 
            int SquareInSquare = squareProduct - squareScolarProduct;
            return Math.Sqrt(SquareInSquare / GetScolarProduct(vectorOfLine, vectorOfLine));
        }

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

        public int Id { get; }

        public Line(Point point1, Point point2)
        {
            this.FirstEdge = point1;
            this.SecondEdge = point2;
        }

        public Line(Point point1, Point point2, int id)
        {
            this.FirstEdge = point1;
            this.SecondEdge = point2;
            this.Id = id;
        }
    }
}
