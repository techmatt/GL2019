﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace WebRunner
{
    public partial class LevelEditor : Form
    {
        public LevelEditor()
        {
            InitializeComponent();
        }

        GameManager manager = null;
        EditorManager editor = null;

        private void LevelEditor_Load(object sender, EventArgs e)
        {

        }

        private void setEditorStructure(StructureType type)
        {
            if (editor != null)
                editor.setToolStructure(type);
        }

        private void radioButtonCamera_CheckedChanged(object sender, EventArgs e)
        {
            setEditorStructure(StructureType.Camera);
        }

        private void radioButtonFirewall_CheckedChanged(object sender, EventArgs e)
        {
            //setEditorStructure(StructureType.Firewall);
        }

        private void radioButtonWall_CheckedChanged(object sender, EventArgs e)
        {
            setEditorStructure(StructureType.Wall);
        }

        private void radioButtonShielding_CheckedChanged(object sender, EventArgs e)
        {
            setEditorStructure(StructureType.GlassWall);
        }

        private void radioButtonSpawnA_CheckedChanged(object sender, EventArgs e)
        {
            setEditorStructure(StructureType.SpawnPointA);
        }

        private void radioButtonSpawnB_CheckedChanged(object sender, EventArgs e)
        {
            setEditorStructure(StructureType.SpawnPointB);
        }

        private void radioButtonDoor_CheckedChanged(object sender, EventArgs e)
        {
            setEditorStructure(StructureType.Door);
        }

        private void radioButtonObjective_CheckedChanged(object sender, EventArgs e)
        {
            setEditorStructure(StructureType.Objective);
        }

        private void radioButtonShoes_CheckedChanged(object sender, EventArgs e)
        {
            setEditorStructure(StructureType.Shoes);
        }

        private void radioButtonLaserGun_CheckedChanged(object sender, EventArgs e)
        {
            setEditorStructure(StructureType.LaserGun);
        }

        private void radioButtonMirror_CheckedChanged(object sender, EventArgs e)
        {
            setEditorStructure(StructureType.StationaryMirror);
        }

        private void radioButtonLaserTurret_CheckedChanged(object sender, EventArgs e)
        {
            setEditorStructure(StructureType.LaserTurret);
        }

        private void radioButtonBulletTurret_CheckedChanged(object sender, EventArgs e)
        {
            //setEditorStructure(StructureType.BulletTurret);
        }

        private void radioButtonDistractionPickup_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void radioButtonMedpack_CheckedChanged(object sender, EventArgs e)
        {
            setEditorStructure(StructureType.MedpackPickup);
        }

        private void radioButtonBotnet_CheckedChanged(object sender, EventArgs e)
        {
            //setEditorStructure(StructureType.BotnetPickup);
        }

        private void radioButtonBomb_CheckedChanged(object sender, EventArgs e)
        {
            //setEditorStructure(StructureType.BombPickup);
        }

        private void radioButtonCloakingFieldPickup_CheckedChanged(object sender, EventArgs e)
        {
            setEditorStructure(StructureType.CloakingFieldPickup);
        }

        private void radioButtonMirrorPickup_CheckedChanged(object sender, EventArgs e)
        {
            setEditorStructure(StructureType.MirrorPickup);
        }
        private void radioButtonBotnet_CheckedChanged_1(object sender, EventArgs e)
        {
            setEditorStructure(StructureType.DysonPickup);
        }
        private void radioButtonKusanagi_CheckedChanged(object sender, EventArgs e)
        {
            setEditorStructure(StructureType.KusanagiPickup);
        }

        private void radioButtonSelect_CheckedChanged(object sender, EventArgs e)
        {
            editor.activeTool = EditorTool.Select;
        }

        private void timerRendering_Tick(object sender, EventArgs e)
        {
            if (manager == null)
            {
                editor = new EditorManager();
                manager = new GameManager(pictureBoxMain, editor);
                manager.startMission("defaultMission", "noTeamName", "emptyLevel");
                levelUpdate();
            }
            manager.stepAndRender(pictureBoxMain.Width, pictureBoxMain.Height);
        }

        private void pictureBoxMain_Click(object sender, EventArgs e)
        {

        }

        private void pictureBoxMain_MouseMove(object sender, MouseEventArgs e)
        {
            if (editor == null) return;
            editor.mouseMove(new Vec2(e.X, e.Y));
            if (e.Button == MouseButtons.Left)
            {
                editor.leftMouseDown(new Vec2(e.X, e.Y));
                selectionUpdate();
            }
            if (e.Button == MouseButtons.Right)
            {
                radioButtonSelect.Checked = true;
                editor.rightMouseDown(new Vec2(e.X, e.Y));
                selectionUpdate();
            }
            //label1.Text = editor.closestStructureDist(editor.level.structures, editor.hoverPos).ToString();
        }

        private void pictureBoxMain_MouseDown(object sender, MouseEventArgs e)
        {
            if (editor == null) return;
            if (e.Button == MouseButtons.Left)
            {
                editor.leftMouseDown(new Vec2(e.X, e.Y));
                selectionUpdate();
            }
            if (e.Button == MouseButtons.Right)
            {
                radioButtonSelect.Checked = true;
                editor.rightMouseDown(new Vec2(e.X, e.Y));
                selectionUpdate();
            }
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            string dir = Constants.missionBaseDir + textBoxMissionName.Text + '/';
            Directory.CreateDirectory(dir);
            string filename = dir + textBoxLevelName.Text + ".txt";
            editor.level.saveToFile(filename);
        }

        private void buttonLoad_Click(object sender, EventArgs e)
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
                    textBoxLevelName.Text   = parts[parts.Length - 1].Replace(".txt", "");
                    if(File.Exists(filename))
                    {
                        manager.startMission(textBoxMissionName.Text, "NoTeamName", textBoxLevelName.Text);
                        levelUpdate();
                        radioButtonSelect.Checked = true;
                    }
                }
            }
        }

        private void levelUpdate()
        {
            labelGuardSpawnRate.Text = editor.level.guardSpawnRate.ToString();
            labelIceSpawnRate.Text = editor.level.ICESpawnRate.ToString();
            labelMaxTime.Text = editor.level.storedMaxCompletionTime.ToString();
        }

        private void selectionUpdate()
        {
            Structure selection = editor.getSelectedStructure();
            if (selection == null) return;
            labelAngleA.Text = selection.sweepAngleStart.ToString();
            labelAngleB.Text = selection.sweepAngleSpan.ToString();
            labelSpeed.Text = selection.sweepAngleSpeed.ToString();
        }

        private void scrollUpdate()
        {
            editor.level.guardSpawnRate = Convert.ToDouble(labelGuardSpawnRate.Text);
            editor.level.ICESpawnRate = Convert.ToDouble(labelIceSpawnRate.Text);
            editor.level.storedMaxCompletionTime = Convert.ToInt32(labelMaxTime.Text);

            Structure selection = editor.getSelectedStructure();
            if (selection == null) return;
            selection.sweepAngleStart = Convert.ToDouble(labelAngleA.Text);
            selection.sweepAngleSpan = Convert.ToDouble(labelAngleB.Text);
            selection.sweepAngleSpeed = Convert.ToDouble(labelSpeed.Text);
        }

        private void scrollAngleA_Scroll(object sender, ScrollEventArgs e)
        {
            labelAngleA.Text = Math.Min(scrollAngleA.Value, 360.0).ToString();
            scrollUpdate();
        }

        private void scrollAngleB_Scroll(object sender, ScrollEventArgs e)
        {
            labelAngleB.Text = scrollAngleB.Value.ToString();
            scrollUpdate();
        }

        private void scrollSpeed_Scroll(object sender, ScrollEventArgs e)
        {
            labelSpeed.Text = (scrollSpeed.Value / 100.0 * 4.0 + 0.1).ToString();
            scrollUpdate();
        }

        private void scrollICESpawnRate_Scroll(object sender, ScrollEventArgs e)
        {
            labelIceSpawnRate.Text = (scrollICESpawnRate.Value / 100.0 * 20.0 + 2.0).ToString();
            scrollUpdate();
        }

        private void scrollMaxTime_Scroll(object sender, ScrollEventArgs e)
        {
            labelMaxTime.Text = (scrollMaxTime.Value / 100.0 * 120.0 + 20.0).ToString();
            scrollUpdate();
        }

        private void scrollGuardSpawnRate_Scroll(object sender, ScrollEventArgs e)
        {
            labelGuardSpawnRate.Text = (scrollGuardSpawnRate.Value / 100.0 * 20.0 + 2.0).ToString();
            scrollUpdate();
        }

        private void buttonReload_Click(object sender, EventArgs e)
        {
            manager.startMission(textBoxMissionName.Text, "NoTeamName", textBoxLevelName.Text);
            levelUpdate();
            radioButtonSelect.Checked = true;
        }
    }
}
