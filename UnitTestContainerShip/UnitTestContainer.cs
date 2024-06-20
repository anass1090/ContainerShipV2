using ClassLibraryContainerShip;
using ClassLibraryContainerShip.Exceptions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTestContainerShip
{
    [TestClass]
    public class UnitTestContainer
    {
        [TestMethod]
        public void Container_Constructor_ThrowsException_WhenWeightIsBelowMinimum()
        {
            ContainerException exception = Assert.ThrowsException<ContainerException>(() => new Container(2, 3));

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
            ContainerException exception = Assert.ThrowsException<ContainerException>(() => new Container(34, 2));

            Assert.AreEqual("Weight maximun is 30 tons", exception.Message);
        }

    }
}
