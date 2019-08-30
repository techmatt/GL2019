using System;
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

        private void radioButtonCamera_CheckedChanged(object sender, EventArgs e)
        {
            if(editor != null)
                editor.setToolStructure(StructureType.Camera);
        }

        private void radioButtonFirewall_CheckedChanged(object sender, EventArgs e)
        {
            if (editor != null)
                editor.setToolStructure(StructureType.Firewall);
        }

        private void radioButtonWall_CheckedChanged(object sender, EventArgs e)
        {
            if (editor != null)
                editor.setToolStructure(StructureType.Wall);
        }

        private void radioButtonShielding_CheckedChanged(object sender, EventArgs e)
        {
            if (editor != null)
                editor.setToolStructure(StructureType.Shielding);
        }

        private void radioButtonSpawnPoint_CheckedChanged(object sender, EventArgs e)
        {
            if (editor != null)
                editor.setToolStructure(StructureType.SpawnPoint);
        }

        private void radioButtonDoor_CheckedChanged(object sender, EventArgs e)
        {
            if (editor != null)
                editor.setToolStructure(StructureType.Door);
        }

        private void radioButtonObjective_CheckedChanged(object sender, EventArgs e)
        {
            if (editor != null)
                editor.setToolStructure(StructureType.Objective);
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
                manager.startMission("defaultMission", "emptyLevel");
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
                        manager.startMission(textBoxMissionName.Text, textBoxLevelName.Text);
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
            labelMaxTime.Text = editor.level.maxCompletionTime.ToString();
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
            editor.level.maxCompletionTime = Convert.ToDouble(labelMaxTime.Text);

            Structure selection = editor.getSelectedStructure();
            if (selection == null) return;
            selection.sweepAngleStart = Convert.ToDouble(labelAngleA.Text);
            selection.sweepAngleSpan = Convert.ToDouble(labelAngleB.Text);
            selection.sweepAngleSpeed = Convert.ToDouble(labelSpeed.Text);
        }

        private void scrollAngleA_Scroll(object sender, ScrollEventArgs e)
        {
            labelAngleA.Text = Math.Min(scrollAngleA.Value, 350.0).ToString();
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
    }
}
