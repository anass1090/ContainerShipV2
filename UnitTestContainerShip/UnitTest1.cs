using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using ContainerShipV2;

namespace UnitTestContainerShip
{
    [TestClass]
    public class UnitTest1
    {
        private readonly Ship ship = new Ship(3, 2);
        [TestMethod]
        public void TestMethod1()
        {

            Assert.IsNotNull(ship);
        }
    }
}
