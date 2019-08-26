using Emgu.CV;
using Emgu.CV.Aruco;
using Emgu.CV.Util;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Drawing;

namespace WebRunner
{
    class VisionManager
    {
        VideoCapture capture = new VideoCapture(0); //create a camera capture
        Dictionary arucoDictionary = new Dictionary(Dictionary.PredefinedDictionaryName.Dict4X4_50);
        DetectorParameters detectorParameters = DetectorParameters.GetDefault();

        public void saveImages()
        {
            Directory.CreateDirectory("markerImages");
            for (int id = 0; id < 50; id++)
            {
                Mat imgOut = new Mat();
                ArucoInvoke.DrawMarker(arucoDictionary, id, 512, imgOut);
                Bitmap image = imgOut.Bitmap;
                image.Save("markerImages/" + id.ToString() + ".png");
            }
        }

        private List<MarkerInfo> runDetection(Mat frame)
        {
            var result = new List<MarkerInfo>();

            //frame.CopyTo(frameCopy);
            using (VectorOfInt ids = new VectorOfInt())
            using (VectorOfVectorOfPointF corners = new VectorOfVectorOfPointF())
            using (VectorOfVectorOfPointF rejected = new VectorOfVectorOfPointF())
            {
                ArucoInvoke.DetectMarkers(frame, arucoDictionary, corners, ids, detectorParameters, rejected);
                for(int markerIdx = 0; markerIdx < ids.Size; markerIdx++)
                {
                    int id = ids[markerIdx];
                    var cornerList = corners[id];
                    var corner0 = new Vec2(cornerList[0].X, cornerList[0].Y);
                    var corner1 = new Vec2(cornerList[1].X, cornerList[1].Y);
                    var corner2 = new Vec2(cornerList[2].X, cornerList[2].Y);
                    var corner3 = new Vec2(cornerList[3].X, cornerList[3].Y);
                    result.Add(new MarkerInfo(id, corner0, corner1, corner2, corner3));
                }
            }
            return result;
        }

        public Bitmap processWebcamImage(out List<MarkerInfo> markers)
        {
            Mat frame = capture.QueryFrame();
            markers = runDetection(frame);
            return frame.Bitmap;
        }
    }
}
