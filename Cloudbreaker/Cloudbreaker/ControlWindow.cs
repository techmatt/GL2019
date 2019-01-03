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
        Random rnd = new Random();

        HackerWindow hackerWindow;
        //SectorWindow sectorWindow;

        Display hackerDisplay;
        //Display sectorDisplay;

        int hackerWindowWidth = 1920 / 2;
        int hackerWindowHeight = 1200 / 2;

        Game game;

        public ControlWindow()
        {
            InitializeComponent();
        }

        private void buttonLaunchRun_Click(object sender, EventArgs e)
        {
            closeAll();

            game = new Game();

            hackerWindow = new HackerWindow(game);
            //sectorWindow = new SectorWindow();

            hackerWindow.Show();
            //sectorWindow.Show();

            hackerWindow.Width = hackerWindowWidth;
            hackerWindow.Height = hackerWindowHeight;

            hackerWindow.pictureBoxMain.Top = 0;
            hackerWindow.pictureBoxMain.Left = 0;
            hackerWindow.pictureBoxMain.Width = hackerWindowWidth;
            hackerWindow.pictureBoxMain.Height = hackerWindowHeight;

            hackerDisplay = new Display(hackerWindow.pictureBoxMain, hackerWindowWidth, hackerWindowHeight);
        }

        private void closeAll()
        {
            if (hackerWindow != null)
                hackerWindow.Close();
            //if (sectorWindow != null)
            //    sectorWindow.Close();
        }

        private void frameTimer_Tick(object sender, EventArgs e)
        {
            if (hackerDisplay != null)
            {
                //hackerDisplay.g.Clear(Color.FromArgb(rnd.Next(0, 256), rnd.Next(0, 256), rnd.Next(0, 256)));
                hackerDisplay.g.Clear(Color.FromArgb(0, 0, 0));

                game.console.Render(hackerDisplay);

                hackerDisplay.update();

                
            }
        }
    }
}
