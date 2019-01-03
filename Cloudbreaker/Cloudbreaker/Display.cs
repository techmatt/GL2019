using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace Cloudbreaker
{
    public class GraphicsUtil
    {
        public GraphicsUtil()
        {
            
        }
        //public Font consoleFont = new Font(new FontFamily("Times New Roman"), 32, FontStyle.Regular, GraphicsUnit.Pixel);
        public Font consoleFont = new Font(new FontFamily("CONSOLAS"), 16, FontStyle.Regular, GraphicsUnit.Pixel);
        public SolidBrush consoleBackgroundBrush = new SolidBrush(Color.FromArgb(255, 40, 40, 40));
        public SolidBrush consoleFontBrush = new SolidBrush(Color.FromArgb(255, 24, 190, 24));
    }

    public class Display
    {
        public Display(PictureBox _targetBox, int _width, int _height)
        {
            targetBox = _targetBox;
            width = _width;
            height = _height;

            bmp = new Bitmap(width, height);
            g = Graphics.FromImage(bmp);

            g.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAlias;

            //bmpPBox = new Bitmap(width, height);
            //gPBox = Graphics.FromImage(bmpPBox);
            //targetBox.Image = bmpPBox;
        }
        //public Graphics gPBox;
        //public Bitmap bmpPBox;

        public int width, height;

        PictureBox targetBox;
        Bitmap bmp;
        public Graphics g;
        public GraphicsUtil util = new GraphicsUtil();

        public void update()
        {
            //gPBox.DrawImage(bmp, new Point(0, 0));
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
