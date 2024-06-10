﻿using ContainerShipV2;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTestContainerShip
{
    [TestClass]
    public class UnitTestContainer
    {
        [TestMethod]
        public void Container_Constructor_ThrowsException_WhenWeightIsBelowMinimum()
        {
            // Act & Assert
            Exception exception = Assert.ThrowsException<Exception>(() => new Container(2, 3));

            Assert.AreEqual("Weight mininum is 4 tons", exception.Message);
        }

        [TestMethod]
        public void Container_Constructor_WhenWeightIsAboveMinimum()
        {
            Container container = new Container(15, 1);

            Assert.IsTrue(container.Weight > 4);
        }

        [TestMethod]
        public void Container_Constructor_WhenWeightIsAboveMaximum()
        {
            Exception exception = Assert.ThrowsException<Exception>(() => new Container(34, 2));

            Assert.AreEqual("Weight maximun is 30 tons", exception.Message);

        }

    }
}
