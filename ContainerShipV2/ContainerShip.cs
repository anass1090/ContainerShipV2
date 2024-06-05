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
            Ship ship = new Ship(5, 4);


            FormContainers.Add(new Container(30, 1));
            FormContainers.Add(new Container(30, 1));
            FormContainers.Add(new Container(30, 1));
            FormContainers.Add(new Container(30, 1));
            FormContainers.Add(new Container(30, 1)); FormContainers.Add(new Container(30, 1));
            FormContainers.Add(new Container(30, 1));
            FormContainers.Add(new Container(30, 1));
            FormContainers.Add(new Container(30, 1));
            FormContainers.Add(new Container(30, 1)); FormContainers.Add(new Container(30, 1));
            FormContainers.Add(new Container(30, 1));
            FormContainers.Add(new Container(30, 1));
            FormContainers.Add(new Container(30, 1));
            FormContainers.Add(new Container(30, 1)); FormContainers.Add(new Container(30, 1));
            FormContainers.Add(new Container(30, 1));
            FormContainers.Add(new Container(30, 1));
            FormContainers.Add(new Container(30, 1));
            FormContainers.Add(new Container(30, 1));

            FormContainers.Add(new Container(4, 1));
            FormContainers.Add(new Container(4, 1));
            FormContainers.Add(new Container(4, 1));
            FormContainers.Add(new Container(4, 1));
            FormContainers.Add(new Container(4, 1));
            FormContainers.Add(new Container(4, 1));
            FormContainers.Add(new Container(4, 1));
            FormContainers.Add(new Container(4, 1));
            FormContainers.Add(new Container(4, 1));
            FormContainers.Add(new Container(4, 1));

            FormContainers.Add(new Container(30, 3));
            FormContainers.Add(new Container(30, 3));
            FormContainers.Add(new Container(30, 3));
            FormContainers.Add(new Container(30, 3));
            FormContainers.Add(new Container(30, 3));

            FormContainers.Add(new Container(30, 2));
            FormContainers.Add(new Container(30, 2));
            FormContainers.Add(new Container(30, 2));






            foreach (Container container in FormContainers)
            {
                ship.AddContainer(container);
            }

            ship.Run();
        }
    }
}
