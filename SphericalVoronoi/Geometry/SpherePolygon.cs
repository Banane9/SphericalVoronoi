using System;
using System.Collections.Generic;
using System.Linq;

namespace SphericalVoronoi.Geometry
{
    public class SpherePolygon
    {
        private Corner startCorner;

        public double Area
        {
            get
            {
                // http://mathworld.wolfram.com/SphericalPolygon.html

                var corners = Corners.ToArray();
                var interiorAngleSum = corners.Select(corner => corner.Angle).Sum();

                return (interiorAngleSum - ((corners.Length - 2) * Math.PI));
            }
        }

        public IEnumerable<Corner> Corners
        {
            get
            {
                var currentCorner = startCorner;

                do
                {
                    yield return currentCorner;
                    currentCorner = currentCorner.Next;
                }
                while (currentCorner.Next != startCorner);

                yield return currentCorner;
            }
        }

        public SpherePolygon(params SphereCoordinate[] points)
        {
            if (points.Length < 2)
                throw new ArgumentOutOfRangeException("points", "There has to be at least two points to make a polygon on a sphere.");

            startCorner = new Corner(points[0]);
            var currentCorner = startCorner;
            for (var i = 1; i % points.Length != 0; ++i)
            {
                var nextCorner = new Corner(points[i]);
                currentCorner.Next = nextCorner;
                nextCorner.Previous = currentCorner;

                currentCorner = nextCorner;
            }

            currentCorner.Next = startCorner;
            startCorner.Previous = currentCorner;
        }

        public class Corner
        {
            public double Angle
            {
                get
                {
                    var tangentToPrevious = ToPrevious.GetTangentAt(Point, Previous.Point);
                    var tangentToNext = ToNext.GetTangentAt(Point, Next.Point);

                    return Math.Abs(Math.Acos(tangentToPrevious.DotProduct(tangentToNext)));
                }
            }

            public Corner Next { get; set; }

            public SphereCoordinate Point { get; private set; }

            public Corner Previous { get; set; }

            public GreatCircleSegment ToNext
            {
                get { return new GreatCircleSegment(Point, Next.Point); }
            }

            public GreatCircleSegment ToPrevious
            {
                get { return new GreatCircleSegment(Point, Previous.Point); }
            }

            public Corner(SphereCoordinate point)
            {
                Point = point;
            }
        }
    }
}