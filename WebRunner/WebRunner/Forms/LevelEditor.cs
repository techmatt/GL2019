using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WebRunner
{
    public partial class LevelEditor : Form
    {
        public LevelEditor()
        {
            InitializeComponent();
        }

        EditorManager editor = new EditorManager();

        private void LevelEditor_Load(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void radioButtonCamera_CheckedChanged(object sender, EventArgs e)
        {
            editor.activeTool = EditorTool.Camera;
        }

        private void radioButtonFirewall_CheckedChanged(object sender, EventArgs e)
        {
            editor.activeTool = EditorTool.Firewall;
        }

        private void radioButtonWall_CheckedChanged(object sender, EventArgs e)
        {
            editor.activeTool = EditorTool.Wall;
        }

        private void radioButtonShielding_CheckedChanged(object sender, EventArgs e)
        {
            editor.activeTool = EditorTool.Shielding;
        }
    }
}
