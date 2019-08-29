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
        VideoCapture capture = new VideoCapture(Constants.webcamCaptureIndex); //create a camera capture
        Dictionary arucoDictionary = new Dictionary(Dictionary.PredefinedDictionaryName.Dict4X4_50);
        DetectorParameters detectorParameters = DetectorParameters.GetDefault();

        public VisionManager()
        {
            capture.FlipHorizontal = true;
            //detectorParameters.AdaptiveThreshConstant = 3;
            //detectorParameters.AprilTagMinWhiteBlackDiff
        }

        public void saveMarkerImages()
        {
            Directory.CreateDirectory("markerImages");
            for (int id = 0; id < 50; id++)
            {
                Mat imgOut = new Mat();
                ArucoInvoke.DrawMarker(arucoDictionary, id, 512, imgOut);

                Mat imgOutFlip = new Mat();
                CvInvoke.Flip(imgOut, imgOutFlip, Emgu.CV.CvEnum.FlipType.Horizontal);

                Bitmap image = imgOutFlip.Bitmap;
                image.Save("markerImages/" + id.ToString() + ".png");
            }
        }

        //Mat frameCopy = new Mat();
        private List<Marker> runDetection(Mat frame, GameData data)
        {
            var result = new List<Marker>();

            float xScale = (float)Constants.viewportSize.x / frame.Width;
            float yScale = (float)Constants.viewportSize.y / frame.Height;

            //frame.CopyTo(frameCopy);
            using (VectorOfInt ids = new VectorOfInt())
            using (VectorOfVectorOfPointF corners = new VectorOfVectorOfPointF())
            using (VectorOfVectorOfPointF rejected = new VectorOfVectorOfPointF())
            {
                ArucoInvoke.DetectMarkers(frame, arucoDictionary, corners, ids, detectorParameters, rejected);
                for(int markerIdx = 0; markerIdx < ids.Size; markerIdx++)
                {
                    int id = ids[markerIdx];
                    ToolType type = data.getToolType(id);
                    if (type == ToolType.InvalidID)
                        continue;
                    ToolData toolData = data.getToolData(type);
                    var cornerList = corners[markerIdx];
                    var corner0 = new Vec2(cornerList[0].X * xScale, cornerList[0].Y * yScale);
                    var corner1 = new Vec2(cornerList[1].X * xScale, cornerList[1].Y * yScale);
                    var corner2 = new Vec2(cornerList[2].X * xScale, cornerList[2].Y * yScale);
                    var corner3 = new Vec2(cornerList[3].X * xScale, cornerList[3].Y * yScale);
                    
                    result.Add(new Marker(toolData, corner0, corner1, corner2, corner3));
                }
            }
            return result;
        }

        Mat debugFrame = null;
        public Bitmap processWebcamImage(out List<Marker> markers, GameData data)
        {
            if(debugFrame == null)
            {
                debugFrame = capture.QueryFrame();
            }
            markers = runDetection(debugFrame, data);
            return debugFrame.Bitmap;

            //Mat frame = capture.QueryFrame();
            //markers = runDetection(frame, data);
            //return frame.Bitmap;
        }
    }
}
