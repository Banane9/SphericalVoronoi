using SphericalVoronoi.Geometry;

using System;
using System.Collections.Generic;
using System.Linq;

namespace SphericalVoronoi
{
    public class SphericalVoronoi
    {
        private readonly List<SphereCoordinate> points = new List<SphereCoordinate>();

        public SphericalVoronoi(params SphereCoordinate[] points)
        {
            this.points.AddRange(points);
        }
    }
}