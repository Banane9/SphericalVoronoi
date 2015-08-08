using Microsoft.VisualStudio.TestTools.UnitTesting;
using SphericalVoronoi.CoordinateSystems.Spherical;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SphericalVoronoi.Testing
{
    [TestClass]
    public class GreatCircleSegmentIntersectionTest
    {
        [TestMethod]
        public void HandlesArcGoingThrough0Azimuthal()
        {
            var start1 = new SphereCoordinate(Math.PI / 2, 1.75 * Math.PI);
            var end1 = new SphereCoordinate(Math.PI / 2, Math.PI / 4);

            var start2 = new SphereCoordinate(Math.PI / 4, 0);
            var end2 = new SphereCoordinate(0.75 * Math.PI, 0);

            var arc1 = new GreatCircleSegment(start1, end1);
            var arc2 = new GreatCircleSegment(start2, end2);

            SphereCoordinate intersection;
            Assert.IsTrue(arc1.Intersects(arc2, out intersection));
            Assert.AreEqual(intersection, new SphereCoordinate(Math.PI / 2, 0));
        }
    }
}