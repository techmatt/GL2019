
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

        GameScreen screen = null;
        VisionManager vision = new VisionManager();

        private void button1_Click(object sender, EventArgs e)
        {
            
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if(screen == null)
            {
                 screen = new GameScreen(pictureBox1, 1280, 720);
            }
            List<MarkerInfo> markers;
            Bitmap webcamBitmap = vision.processWebcamImage(out markers);
            //image.Save("test.png");
            screen.update(webcamBitmap);
        }
    }
}
