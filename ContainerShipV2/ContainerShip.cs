using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ContainerShipV2
{
    public partial class ContainerShip : Form
    {

        List<Container> FormContainers = new List<Container>();

        public ContainerShip()
        {
            InitializeComponent();
        }

        private void buttonRun_Click(object sender, EventArgs e)
        {
            Ship ship = new Ship(4, 4);

            FormContainers.Add(new Container(11, 1));
            FormContainers.Add(new Container(11, 1));
            FormContainers.Add(new Container(11, 1));
            FormContainers.Add(new Container(11, 2));

            foreach (var container in FormContainers)
            {
                ship.AddContainer(container);
            }

            ship.Run();
        }
    }
}
