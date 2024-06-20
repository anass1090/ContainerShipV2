using ClassLibraryContainerShip.Exceptions;
using ClassLibraryContainerShip;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTestContainerShip
{
    [TestClass]
    public class UnitTestShip
    {

        [TestMethod]
        public void Ship_Load_IsTooHeavy()
        {
            Ship ship = new Ship(1, 1);
            ContainerPlacer containerPlacer = new ContainerPlacer(ship);
            List<Container> containers = new List<Container>
            {
                new Container(30, 1),
                new Container(30, 1),
                new Container(30, 1),
                new Container(30, 1),
                new Container(30, 1),
                new Container(30, 1)
            };

            foreach (Container container in containers)
            {
                containerPlacer.Ship.Containers.Add(container);
            }

            foreach (Container container in ship.UnfitContainers)
            {
                Assert.IsTrue(container.UnfitReason == Container.UnfitReasons.ExceedsMaxWeight);
            }
        }

        [TestMethod]
        [ExpectedException(typeof(ContainerException), "Containers are too light")]
        public void Ship_Load_IsTooLight()
        {
            Ship ship = new Ship(3, 1);
            ContainerPlacer containerPlacer = new ContainerPlacer(ship);
            List<Container> containers = new List<Container>
            {
                new Container(4, 1)
            };
            
            containerPlacer.Ship.Containers.AddRange(containers);
            containerPlacer.Run();
        }

        [TestMethod]
        public void Ship_Load_TooManyCoolables()
        {
            Ship ship = new Ship(1, 1);
            ContainerPlacer containerPlacer = new ContainerPlacer(ship);
            List<Container> containers = new List<Container>
            {
                new Container(30, 3),
                new Container(30, 3),
                new Container(30, 3),
                new Container(30, 3),
                new Container(30, 3),
                new Container(30, 3)
            };

            foreach (Container container in containers)
            {
                containerPlacer.Ship.Containers.Add(container);
            }

            foreach (Container container in ship.UnfitContainers)
            {
                Assert.IsTrue(container.UnfitReason == Container.UnfitReasons.TooManyCoolables);
            }
        }

        [TestMethod]
        public void Ship_Distribute_IsTrue()
        {
            Ship ship = new Ship(2, 2);
            ContainerPlacer containerPlacer = new ContainerPlacer(ship);
            List<Container> containers = new List<Container>
            {
                new Container(30, 1),
                new Container(30, 1),
            };

            containerPlacer.Ship.Containers.AddRange(containers);
            

            Assert.IsTrue(containerPlacer.DistributeContainers());
        }
    }

}
