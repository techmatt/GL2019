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
        public GameScreen(PictureBox _targetBox, int _width, int _height)
        {
            targetBox = _targetBox;
            width = _width;
            height = _height;

            bmp = new Bitmap(width, height);
            g = Graphics.FromImage(bmp);

            g.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAlias;
        }
        
        public int width, height;

        PictureBox targetBox;
        Bitmap bmp;
        public Graphics g;
        
        public void update(Bitmap webcamImage)
        {
            //gPBox.DrawImage(bmp, new Point(0, 0));
            g.Clear(Color.Black);
            g.DrawImage(webcamImage, new Rectangle(0, 0, width, height));
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
