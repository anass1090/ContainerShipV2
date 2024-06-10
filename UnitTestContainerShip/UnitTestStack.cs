using ContainerShipV2;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTestContainerShip
{
    [TestClass]
    public class UnitTestStack
    {
        [TestMethod]
        public void TryAddingContainer_ShouldReturnFalse_WhenStackIsReserved()
        {
            // Arrange
            var stack = new Stack(0, false, false)
            {
                Reserved = true
            };
            var container = new Container(10, 1);

            // Act
            var result = stack.TryAddingContainer(container);

            // Assert
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void TryAddingContainer_ShouldAddValuableContainer_WhenStackIsEmpty()
        {
            // Arrange
            var stack = new Stack(0, false, false);
            var container = new Container(10, 2);

            // Act
            var result = stack.TryAddingContainer(container);

            // Assert
            Assert.IsTrue(result);
            Assert.AreEqual(1, stack.containers.Count);
            Assert.AreEqual(container, stack.containers[0]);
        }

        [TestMethod]
        public void TryAddingContainer_ShouldReturnFalse_WhenCoolableContainerNotAtPosition0()
        {
            // Arrange
            var stack = new Stack(1, false, false);
            var container = new Container(10, 3);

            // Act
            var result = stack.TryAddingContainer(container);

            // Assert
            Assert.IsFalse(result);
            Assert.AreEqual(0, stack.containers.Count);
        }
    }
}
