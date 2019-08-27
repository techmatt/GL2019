using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;

namespace WebRunner
{
    class GameScreen
    {
        public GameScreen(PictureBox _targetBox, GameData _data)
        {
            targetBox = _targetBox;
            data = _data;
            bmpViewport = new Bitmap(Constants.viewportWidth, Constants.viewportHeight);
            gViewport = Graphics.FromImage(bmpViewport);
            gViewport.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAlias;
        }

        void resizeScreen(int renderWidth, int renderHeight)
        {
            if (bmpScreen == null || bmpScreen.Width != renderWidth || bmpScreen.Height != renderHeight)
            {
                bmpScreen = new Bitmap(renderWidth, renderHeight);
                gScreen = Graphics.FromImage(bmpScreen);
            }
        }

        PictureBox targetBox;
        Bitmap bmpScreen;
        Bitmap bmpViewport;
        GameData data;
        public Graphics gScreen, gViewport;
        
        public void drawCircle(Vec2 center, int radius, Brush brush)
        {
            gViewport.FillEllipse(brush, (int)center.x - radius, (int)center.y - radius, radius * 2, radius * 2);
        }

        public void drawRotatedImage(Vec2 center, Vec2 orientation, Bitmap bmp)
        {
            float hw = bmp.Width / 2.0f;
            float hh = bmp.Height / 2.0f;
            gViewport.TranslateTransform(center.x, center.y);
            gViewport.RotateTransform(orientation.angle());
            gViewport.TranslateTransform(-hw, -hh);
            //gScreen.TranslateTransform(hw, hh);

            gViewport.DrawImage(bmp, 0, 0);
            gViewport.ResetTransform();
        }

        public void render(Bitmap webcamImage, GameState state, int renderWidth, int renderHeight)
        {
            gViewport.Clear(Color.Black);
            gViewport.DrawImage(webcamImage, new Rectangle(0, 0, Constants.viewportWidth, Constants.viewportHeight));

            foreach(MarkerInfo m in state.markers)
            {
                drawRotatedImage(m.center, m.orientation, data.bmpShield);
                drawCircle(m.center, 15, m.toolData.brush);
                //m.toolData.color
                //g.DrawEllipse(m.center
            }

            resizeScreen(renderWidth, renderHeight);
            gScreen.DrawImage(bmpViewport, new Rectangle(0, 0, renderWidth, renderHeight));
            targetBox.Image = bmpScreen;

            /*Bitmap bmpLocal = new Bitmap(targetBox.Image);
            using (Graphics gLocal = Graphics.FromImage(bmpLocal))
            {
                gLocal.DrawImage(bmp, new Point(0, 0));
            }
            targetBox.Image = bmpLocal;*/
        }
    }
}
