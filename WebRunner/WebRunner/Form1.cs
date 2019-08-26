
using Emgu.CV;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace WebRunner
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        VideoCapture capture = new VideoCapture(1); //create a camera capture

        private void button1_Click(object sender, EventArgs e)
        {
            
            
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            Bitmap image = capture.QueryFrame().Bitmap; //take a picture
            //image.Save("test.png");
            pictureBox1.Image = image;
        }
    }
}
