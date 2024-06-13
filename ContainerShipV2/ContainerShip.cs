using System;
using System.Collections.Generic;
using System.Windows.Forms;
using ClassLibraryContainerShip;

namespace ContainerShipV2
{
    public partial class ContainerShip : Form
    {
        private readonly List<Container> FormContainers = new List<Container>();

        public ContainerShip()
        {
            InitializeComponent();
        }

        private void buttonRun_Click(object sender, EventArgs e)
        {
            ContainerPlacer containerPlacer = new ContainerPlacer(new Ship((int)numLength.Value, (int)numWidth.Value));

            for (int i = 0; i < 3; i++) FormContainers.Add(new Container(30, 4));
            for (int i = 0; i < 7; i++) FormContainers.Add(new Container(30, 3));
            for (int i = 0; i < 25; i++) FormContainers.Add(new Container(30, 1));

            foreach (Container container in FormContainers)
            {
                containerPlacer.Ship.Containers.Add(container);
            }

            containerPlacer.Run();

            Console.WriteLine("Amount of unfit containers: " + containerPlacer.Ship.UnfitContainers.Count);

            foreach (Container container in containerPlacer.Ship.UnfitContainers)
            {
                Console.WriteLine(container.UnfitReason.ToString());
            }
        }
    }
}
