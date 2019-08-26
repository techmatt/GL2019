
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
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }

        GameManager manager = null;

        private void button1_Click(object sender, EventArgs e)
        {
            
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if(manager == null)
            {
                manager = new GameManager(pictureBox1);
            }
            manager.stepAndRender();
        }
    }
}
