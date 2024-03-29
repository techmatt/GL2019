﻿
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
            var screen = Screen.AllScreens[0];
            this.FormBorderStyle = FormBorderStyle.None;
            this.Top = 0;
            this.Left = 0;
            this.Width = screen.Bounds.Width;
            this.Height = screen.Bounds.Height;
            pictureBoxMain.Top = 0;
            pictureBoxMain.Left = 0;
            pictureBoxMain.Width = screen.Bounds.Width;
            pictureBoxMain.Height = screen.Bounds.Height;
            buttonStartGame.Visible = false;
            buttonFullScreen.Visible = false;
            buttonAdvanceLevel.Visible = false;
            buttonComplete.Visible = false;
            buttonBrowse.Visible = false;
            
            label1.Visible = false;
            label2.Visible = false;
            textBoxMissionName.Visible = false;
            textBoxTeam.Visible = false;
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
            manager.startMission(textBoxMissionName.Text, textBoxTeam.Text, null);

            //manager.startMission("defaultMission", "emptyLevel");
            //manager.vision.saveMarkerImages();
            //levelUpdate();
        }

        private void buttonComplete_Click(object sender, EventArgs e)
        {
            manager.state.curLevel.completeLevel();
        }

        private void buttonAdvanceLevel_Click(object sender, EventArgs e)
        {
            GameLevel level = manager.state.curLevel;
            level.runnersCompleted[0] = true;
            level.runnersCompleted[1] = true;
        }

        private void GameWindow_KeyUp(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.N)
            {
                GameLevel level = manager.state.curLevel;
                level.runnersCompleted[0] = true;
                level.runnersCompleted[1] = true;
            }
        }

        private void GameWindow_Load(object sender, EventArgs e)
        {

        }
    }
}
