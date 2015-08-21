using SphericalVoronoi.Geometry;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SphericalVoronoi
{
    /// <summary>
    /// Represents a Polygon on the Sphere.
    /// </summary>
    public class VoronoiCell
    {
        private readonly GreatCircle FirstPart;

        /// <summary>
        /// A reference to one of the corners.
        /// </summary>
        private Corner startCorner;

        /// <summary>
        /// Gets the area of the polygon.
        /// </summary>
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

        /// <summary>
        /// Gets the point used as center for Voronoi calculations.
        /// </summary>
        public SphereCoordinate Center { get; private set; }

        /// <summary>
        /// Gets all the corners.
        /// </summary>
        public IEnumerable<Corner> Corners
        {
            get
            {
                if (startCorner == null)
                    yield break;

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

        /// <summary>
        /// Creates a new instance of the <see cref="VoronoiCell"/> class with the given center and sides.
        /// </summary>
        /// <param name="center">The point used as center for Voronoi calculations. Assumed to be inside.</param>
        /// <param name="firstPart">The <see cref="GreatCircle"/> that makes the first part of the Polygon.</param>
        public VoronoiCell(SphereCoordinate center, GreatCircle firstPart)
        {
            Center = center;
            FirstPart = firstPart;
        }

        /// <summary>
        /// Checks whether the given <see cref="SphereCoordinate"/> is within this <see cref="VoronoiCell"/>.
        /// </summary>
        /// <param name="sphereCoordinate">The <see cref="SphereCoordinate"/> to check.</param>
        /// <returns>Whether the given <see cref="SphereCoordinate"/> is within this <see cref="VoronoiCell"/>.</returns>
        public bool IsInside(SphereCoordinate sphereCoordinate)
        {
            var centerToPoint = new GreatCircleSegment(Center, sphereCoordinate);

            // Check if any sides intersect with the arc from Center to point.
            SphereCoordinate _;
            if (startCorner == null)
                return FirstPart.Intersects(centerToPoint, out _);
            else
                foreach (var corner in Corners)
                    if (corner.ToNext.Intersects(centerToPoint, out _))
                        return false;

            return true;
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

            public GreatCircleSegment ToNext { get; set; }

            public GreatCircleSegment ToPrevious { get; set; }

            public Corner(SphereCoordinate point)
            {
                Point = point;
            }
        }
    }
}