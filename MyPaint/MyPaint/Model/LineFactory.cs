using MyPaint.Model.FactoryComponent;
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
    }
}
