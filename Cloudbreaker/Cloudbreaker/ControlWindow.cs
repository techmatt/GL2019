using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Cloudbreaker
{
    public partial class ControlWindow : Form
    {
        HackerWindow hackerWindow;
        SectorWindow sectorWindow;

        public ControlWindow()
        {
            InitializeComponent();
        }

        private void buttonLaunchRun_Click(object sender, EventArgs e)
        {
            closeAll();
            hackerWindow = new HackerWindow();
            sectorWindow = new SectorWindow();

            hackerWindow.Show();
            sectorWindow.Show();
        }

        private void closeAll()
        {
            if (hackerWindow != null)
                hackerWindow.Close();
            if (sectorWindow != null)
                sectorWindow.Close();
        }
    }
}
