using Emgu.CV;
using Emgu.CV.Aruco;
using Emgu.CV.Util;

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

namespace WebRunner
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }

        VideoCapture capture = new VideoCapture(0); //create a camera capture
        Dictionary arucoDictionary = new Dictionary(Dictionary.PredefinedDictionaryName.Dict4X4_50);
        Mat frameCopy = new Mat();
        DetectorParameters detectorParameters = DetectorParameters.GetDefault();

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void runDetection(Mat frame)
        {
            frame.CopyTo(frameCopy);
            using (VectorOfInt ids = new VectorOfInt())
            using (VectorOfVectorOfPointF corners = new VectorOfVectorOfPointF())
            using (VectorOfVectorOfPointF rejected = new VectorOfVectorOfPointF())
            {
                ArucoInvoke.DetectMarkers(frameCopy, arucoDictionary, corners, ids, detectorParameters, rejected);
                Debug.WriteLine("id count: " + ids.Size.ToString());
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            Mat frame = capture.QueryFrame();
            runDetection(frame);
            Bitmap image = frame.Bitmap; //take a picture
            //image.Save("test.png");
            pictureBox1.Image = image;
        }
    }
}
