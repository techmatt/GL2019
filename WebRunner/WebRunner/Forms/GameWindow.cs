
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace WebRunner
{
    public partial class GameWindow : Form
    {
        public GameWindow()
        {
            InitializeComponent();
        }

        GameManager manager = null;

        private void button1_Click(object sender, EventArgs e)
        {
            manager.vision.saveMarkerImages();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if(manager == null)
            {
                manager = new GameManager(pictureBoxMain, null);
                manager.startMission("defaultMission", "emptyLevel");
            }
            manager.stepAndRender(pictureBoxMain.Width, pictureBoxMain.Height);
        }

        private void buttonFullScreen_Click(object sender, EventArgs e)
        {
            this.FormBorderStyle = FormBorderStyle.None;
            this.Top = 0;
            this.Left = 0;
            this.Width = Constants.renderWidthFull;
            this.Height = Constants.renderHeightFull;
            pictureBoxMain.Top = 0;
            pictureBoxMain.Left = 0;
            pictureBoxMain.Width = Constants.renderWidthFull;
            pictureBoxMain.Height = Constants.renderHeightFull;
            button1.Visible = false;
            buttonFullScreen.Visible = false;
        }

        private void pictureBoxMain_Click(object sender, EventArgs e)
        {

        }
    }
}
