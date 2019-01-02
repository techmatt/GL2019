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
    class Display
    {
        public Display(PictureBox _targetBox, int _width, int _height)
        {
            targetBox = _targetBox;
            width = _width;
            height = _height;
            bmp = new Bitmap(width, height);
            g = Graphics.FromImage(bmp);
        }
        public Graphics g;
        public int width, height;

        PictureBox targetBox;
        Bitmap bmp;

        public void update()
        {
            Bitmap bmpLocal = new Bitmap(targetBox.Image);
            using (Graphics gLocal = Graphics.FromImage(bmpLocal))
            {
                gLocal.DrawImage(bmp, new Point(0, 0));
            }
            targetBox.Image = bmpLocal;
        }
    }
}
