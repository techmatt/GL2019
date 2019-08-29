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
            bmpViewport = new Bitmap((int)Constants.viewportSize.x, (int)Constants.viewportSize.y);
            gViewport = Graphics.FromImage(bmpViewport);
            gViewport.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAlias;
            gViewport.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            gViewport.CompositingMode = System.Drawing.Drawing2D.CompositingMode.SourceOver;
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

        public void drawImage(ImageEntry image, Vec2 center)
        {
            gViewport.DrawImage(image.bmp, (int)(center.x - image.bmp.Width / 2), (int)(center.y - image.bmp.Height / 2));
        }

        public void drawRotatedImage(Vec2 center, Vec2 orientation, Bitmap bmp)
        {
            double hw = bmp.Width * 0.5;
            double hh = bmp.Height * 0.5;
            gViewport.TranslateTransform((float)center.x, (float)center.y);
            gViewport.RotateTransform((float)orientation.angle());
            gViewport.TranslateTransform((float)-hw, (float)-hh);
            
            gViewport.DrawImage(bmp, 0, 0);
            gViewport.ResetTransform();
        }

        public void render(Bitmap webcamImage, GameData data, GameState state, int renderWidth, int renderHeight)
        {
            gViewport.Clear(Color.Black);

            gViewport.DrawImage(webcamImage, new Rectangle(0, 0, (int)Constants.viewportSize.x, (int)Constants.viewportSize.y));
            foreach (GameLevel level in state.activeLevels)
            {
                ImageEntry backgroundImg = data.images.getBackground(level.backgroundName, false);
                Vec2 bkgStart = level.worldRect.pMin - state.viewport.pMin;
                gViewport.DrawImage(backgroundImg.bmp, (int)bkgStart.x, (int)bkgStart.y);
                level.render(this, data, state);
            }

            foreach(Marker m in state.markers)
            {
                drawRotatedImage(m.center, m.orientation, data.images.shield.bmp);
                drawCircle(m.center, 15, m.toolData.brush);
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
