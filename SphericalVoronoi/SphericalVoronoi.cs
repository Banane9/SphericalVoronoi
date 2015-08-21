using SphericalVoronoi.Geometry;

using System;
using System.Collections.Generic;
using System.Linq;

namespace SphericalVoronoi
{
    /// <summary>
    /// Represents a Voronoi Diagram on a unit sphere.
    /// </summary>
    public class SphericalVoronoi
    {
        private readonly List<VoronoiPoint> points = new List<VoronoiPoint>();

        public SphericalVoronoi(params SphereCoordinate[] points)
        {
            this.points.AddRange(points.Select(point => new VoronoiPoint(point)));
        }

        public void Calculate()
        {
            var uncalculated = points.Where(point => !point.Calculated);
        }

        private class VoronoiPoint
        {
            public bool Calculated { get; set; }

            public SphereCoordinate Point { get; private set; }

            public VoronoiCell Polygon { get; set; }

            public VoronoiPoint(SphereCoordinate point, bool calculated = false, VoronoiCell polygon = null)
            {
                Point = point;
                Calculated = calculated;
                Polygon = polygon;
            }
        }
    }
}