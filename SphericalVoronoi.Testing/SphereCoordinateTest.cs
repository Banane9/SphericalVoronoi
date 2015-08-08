using Microsoft.VisualStudio.TestTools.UnitTesting;
using SphericalVoronoi.CoordinateSystems.Spherical;
using System;

namespace SphericalVoronoi.Testing
{
    [TestClass]
    public class SphereCoordinateTest
    {
        [TestMethod]
        public void HandlesGreaterTheta()
        {
            var sphereCoord1 = new SphereCoordinate(3 * Math.PI, 0);
            var sphereCoord2 = new SphereCoordinate(4 * Math.PI, 0);

            Assert.AreEqual(new SphereCoordinate(Math.PI, 0), sphereCoord1);
            Assert.AreEqual(new SphereCoordinate(0, Math.PI), sphereCoord2);
        }

        [TestMethod]
        public void HandlesSmallerTheta()
        {
            var sphereCoord1 = new SphereCoordinate(-Math.PI, 0);
            var sphereCoord2 = new SphereCoordinate(-2 * Math.PI, 0);

            Assert.AreEqual(new SphereCoordinate(Math.PI, 0), sphereCoord1);
            Assert.AreEqual(new SphereCoordinate(0, Math.PI), sphereCoord2);
        }
    }
}