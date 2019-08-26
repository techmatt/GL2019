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

            bmp = new Bitmap(renderWidth, renderHeight);
            g = Graphics.FromImage(bmp);

            g.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAlias;
        }
        
        public int renderWidth, renderHeight;

        PictureBox targetBox;
        Bitmap bmp;
        GameData data;
        public Graphics g;
        
        public void drawCircle(Vec2 center, int radius, Brush brush)
        {
            g.FillEllipse(brush, (int)center.x, (int)center.y, radius, radius);
        }

        public void render(Bitmap webcamImage, GameState state)
        {
            g.Clear(Color.Black);
            g.DrawImage(webcamImage, new Rectangle(0, 0, renderWidth, renderHeight));

            foreach(MarkerInfo m in state.markers)
            {

                //g.DrawEllipse(m.center
            }
            targetBox.Image = bmp;

            /*Bitmap bmpLocal = new Bitmap(targetBox.Image);
            using (Graphics gLocal = Graphics.FromImage(bmpLocal))
            {
                gLocal.DrawImage(bmp, new Point(0, 0));
            }
            targetBox.Image = bmpLocal;*/
        }
    }
}
