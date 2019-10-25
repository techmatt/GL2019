using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;

namespace Pulse
{
    class GameScreen
    {
        PictureBox targetBox;
        Bitmap bmpScreen;
        Bitmap bmpViewport;
        GameDatabase database;
        public Graphics gScreen, gViewport;

        public GameScreen(PictureBox _targetBox, GameDatabase _database)
        {
            targetBox = _targetBox;
            database = _database;
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

        public void drawCircle(Vec2 center, int radius, Brush fillBrush, Pen edgePen)
        {
            if(fillBrush != null)
                gViewport.FillEllipse(fillBrush, (int)center.x - radius, (int)center.y - radius, radius * 2, radius * 2);
            if(edgePen != null)
                gViewport.DrawEllipse(edgePen, (int)center.x - radius, (int)center.y - radius, radius * 2, radius * 2);
        }

        public void drawLine(Vec2 p0, Vec2 p1, Pen pen)
        {
            gViewport.DrawLine(pen, (int)p0.x, (int)p0.y, (int)p1.x, (int)p1.y);
        }

        public void drawArc(Vec2 center, int radius, Pen pen, double startAngle, double endAngle)
        {
            gViewport.DrawArc(pen, (int)center.x - radius, (int)center.y - radius, radius * 2, radius * 2, (float)startAngle, (float)endAngle);
        }

        public void drawRectangle(Vec2 center, int radius, Color color)
        {
            Brush brush = new SolidBrush(color);
            gViewport.FillRectangle(brush, (int)center.x - radius, (int)center.y - radius, radius * 2, radius * 2);
        }

        public void drawImage(Bitmap bmp, Vec2 center)
        {
            gViewport.DrawImage(bmp, (int)(center.x - bmp.Width / 2), (int)(center.y - bmp.Height / 2));
        }

        public void drawImage(Bitmap bmp, Vec2 start, Vec2 dim)
        {
            gViewport.DrawImage(bmp, new Rectangle((int)start.x, (int)start.y, (int)dim.x, (int)dim.y));
        }

        public void drawRotatedImage(Bitmap bmp, Vec2 center, Vec2 orientation)
        {
            double hw = bmp.Width * 0.5;
            double hh = bmp.Height * 0.5;
            gViewport.TranslateTransform((float)center.x, (float)center.y);
            gViewport.RotateTransform((float)orientation.angle());
            gViewport.TranslateTransform((float)-hw, (float)-hh);
            
            gViewport.DrawImage(bmp, 0, 0);
            gViewport.ResetTransform();
        }

        public void renderDecoder(GameState state, int renderWidth, int renderHeight)
        {
            gViewport.Clear(Color.Black);
            gViewport.DrawImage(database.images.decoderBkg.bmp, 0, 0);

            //static public Vec2 decoderGridSize = new Vec2(3, 6);
            //static public Vec2 decoderGridStart = new Vec2(50, 50);
            //static public Vec2 decoderGridSpacing = new Vec2(300, 100);
            //static public Vec2 decoderTextureOffset = new Vec2(100, 0);
            int glyphIndex = 0;
            for (int gridY = 0; gridY < Constants.decoderGridSize.y; gridY++)
                for (int gridX = 0; gridX < Constants.decoderGridSize.x; gridX++)
                {
                    if (glyphIndex >= Constants.totalGlyphCount)
                        continue;

                    GlyphState g = state.level.alphabet.glyphs[glyphIndex];
                    Bitmap bmpA = database.images.glyphImages[glyphIndex].bmp;
                    Bitmap bmpB = g.texture.bmp;

                    //Constants.decoderGridStart + 
                    Vec2 gridStartPos = Constants.decoderGridStart + new Vec2(gridX * Constants.decoderGridSpacing.x, gridY * Constants.decoderGridSpacing.y);
                    drawImage(bmpA, gridStartPos, new Vec2(Constants.glyphDim, Constants.glyphDim));
                    drawImage(bmpB, gridStartPos + Constants.decoderTextureOffset, Constants.textureSize);

                    glyphIndex++;
                }

            resizeScreen(renderWidth, renderHeight);
            gScreen.DrawImage(bmpViewport, new Rectangle(0, 0, renderWidth, renderHeight));
            targetBox.Image = bmpScreen;
        }

        public void renderPulse(GameState state, int renderWidth, int renderHeight)
        {
            gViewport.Clear(Color.Black);
            gViewport.DrawImage(database.images.pulseBkg.bmp, 0, 0);

            resizeScreen(renderWidth, renderHeight);
            gScreen.DrawImage(bmpViewport, new Rectangle(0, 0, renderWidth, renderHeight));
            targetBox.Image = bmpScreen;
        }
    }
}
