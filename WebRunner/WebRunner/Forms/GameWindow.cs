
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

        private void timer1_Tick(object sender, EventArgs e)
        {
            if(manager == null)
            {
                return;
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
            buttonStartGame.Visible = false;
            buttonFullScreen.Visible = false;
        }

        private void buttonBrowse_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.InitialDirectory = Constants.dataDir + "missions";
                openFileDialog.Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*";
                openFileDialog.FilterIndex = 2;
                openFileDialog.RestoreDirectory = false;

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    string filename = openFileDialog.FileName;
                    string[] parts = filename.Split('\\');
                    textBoxMissionName.Text = parts[parts.Length - 2];
                }
            }
        }

        private void buttonStartGame_Click(object sender, EventArgs e)
        {
            manager = new GameManager(pictureBoxMain, null);
            manager.startMission(textBoxMissionName.Text, null);

            //manager.startMission("defaultMission", "emptyLevel");
            //manager.vision.saveMarkerImages();
            //levelUpdate();
        }

        private void buttonComplete_Click(object sender, EventArgs e)
        {
            GameLevel level = manager.state.curLevel;
            foreach(Structure s in level.structures)
            {
                if(s.type == StructureType.Objective)
                {
                    s.achieved = true;
                }
                if(s.type == StructureType.Camera || s.type == StructureType.LaserTurret)
                {
                    s.disableTimeLeft = 10000.0;
                    s.curHealth = 0.0;
                }
            }
            level.objectivesAchieved = level.objectivesTotal;
        }
    }
}
