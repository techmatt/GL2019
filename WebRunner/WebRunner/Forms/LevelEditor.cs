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

        private void timerRendering_Tick(object sender, EventArgs e)
        {
            if (manager == null)
            {
                manager = new GameManager(pictureBoxMain);
                manager.reset("editor");
                editor = new EditorManager(manager);
                manager.editor = editor;
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
                editor.leftMouseDown(new Vec2(e.X, e.Y));
            //label1.Text = editor.closestStructureDist(editor.level.structures, editor.hoverPos).ToString();
        }

        private void pictureBoxMain_MouseDown(object sender, MouseEventArgs e)
        {
            if (editor == null) return;
            if (e.Button == MouseButtons.Left)
                editor.leftMouseDown(new Vec2(e.X, e.Y));
            
        }
    }
}
