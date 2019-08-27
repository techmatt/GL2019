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
        public GameScreen(PictureBox _targetBox, int _renderWidth, int _renderHeight, GameData _data)
        {
            targetBox = _targetBox;
            renderWidth = _renderWidth;
            renderHeight = _renderHeight;
            data = _data;

            bmpScreen = new Bitmap(renderWidth, renderHeight);
            gScreen = Graphics.FromImage(bmpScreen);

            gScreen.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAlias;
        }
        
        public int renderWidth, renderHeight;

        PictureBox targetBox;
        Bitmap bmpScreen;
        GameData data;
        public Graphics gScreen;
        
        public void drawCircle(Vec2 center, int radius, Brush brush)
        {
            gScreen.FillEllipse(brush, (int)center.x - radius, (int)center.y - radius, radius * 2, radius * 2);
        }

        public void drawRotatedImage(Vec2 center, Vec2 orientation, Bitmap bmp)
        {
            float hw = bmp.Width / 2.0f;
            float hh = bmp.Height / 2.0f;
            gScreen.TranslateTransform(hw, hh);
            gScreen.RotateTransform(orientation.angle());
            gScreen.TranslateTransform(-hw, -hh);
            gScreen.DrawImage(bmp, 0, 0);
            gScreen.ResetTransform();
        }

        public void render(Bitmap webcamImage, GameState state)
        {
            gScreen.Clear(Color.Black);
            gScreen.DrawImage(webcamImage, new Rectangle(0, 0, renderWidth, renderHeight));

            foreach(MarkerInfo m in state.markers)
            {
                drawCircle(m.center, 15, m.toolData.brush);
                drawRotatedImage(m.center, m.orientation, data.bmpShield);
                //m.toolData.color
                //g.DrawEllipse(m.center
            }
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
