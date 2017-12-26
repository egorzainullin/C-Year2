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
