
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
using System.Windows.Forms;

/*
 * https://github.com/emgucv/emgucv/blob/master/Emgu.CV.Example/Aruco/MainForm.cs
 * https://docs.opencv.org/3.2.0/d9/d6a/group__aruco.html#ga13a2742381c0a48e146d230a8cda2e66
 * http://www.emgu.com/wiki/files/3.2.0/document/html/763bec7d-ea86-f394-1e61-2dbbf59a0d6f.htm
 * https://docs.opencv.org/3.1.0/d5/dae/tutorial_aruco_detection.html
 */
namespace WebRunner
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        VideoCapture capture = new VideoCapture(1); //create a camera capture
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
