using System.Drawing;

namespace MyPaint.Model
{
    public interface ILine
    {
        Point FirstEdge { get; }

        Point SecondEdge { get; }

        void SetFirstEdge(Point point);

        void SetSecondEdge(Point point);

        int PointNearBy(Point point);

        int Id { get; }
    }
}