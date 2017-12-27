using MyPaint.Model.FactoryComponent;
using System;
using System.Drawing;

namespace MyPaint.Model
{
    public class LineFactory
    {
        private static int idCounter = -1;

        public static ILine CreateLine(Point point1, Point point2)
        {
            ++idCounter;
            return new Line(point1, point2, idCounter);
        }

        public static ILine CreateLineWithGivenId(int id, Point point1, Point point2)
        {
            idCounter = Math.Max(idCounter + 1, id + 1);
            return new Line(point1, point2, id);
        }
    }
}
