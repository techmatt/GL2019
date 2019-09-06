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

        PictureBox targetBox;
        Bitmap bmpScreen;
        Bitmap bmpViewport;
        GameDatabase database;
        public Graphics gScreen, gViewport;
        
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

        public void drawImage(ImageEntry image, int imgInstanceHash, Vec2 center)
        {
            Bitmap bmp = image.getBmp(imgInstanceHash);
            gViewport.DrawImage(bmp, (int)(center.x - bmp.Width / 2), (int)(center.y - bmp.Height / 2));
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

        public void render(Bitmap webcamImage, GameState state, EditorManager editor, int renderWidth, int renderHeight)
        {
            gViewport.Clear(Color.Black);

            gViewport.DrawImage(webcamImage, new Rectangle(0, 0, (int)Constants.viewportSize.x, (int)Constants.viewportSize.y));
            foreach (GameLevel level in state.visibleLevels)
            {
                ImageEntry backgroundImg = database.images.getBackground(level.backgroundName, false);
                Vec2 bkgStart = level.worldRect.pMin - state.viewport.pMin;
                gViewport.DrawImage(backgroundImg.getBmp(0), (int)bkgStart.x, (int)bkgStart.y);
                level.render(this, database, state);
            }

            /*if(state.activeRunnerA != null)
            {
                drawImage(database.images.runners, 0, state.activeRunnerA.center);
            }
            if (state.activeRunnerB != null)
            {
                drawImage(database.images.runners, 1, state.activeRunnerB.center);
            }*/

            Vec2 viewportOrigin = state.viewport.pMin;
            foreach (Structure structure in state.curFrameTemporaryStructures)
            {
                drawImage(database.images.structures[structure.type], structure.curImgInstanceHash, structure.center - viewportOrigin);
            }

            foreach (Marker m in state.markers)
            {
                drawRotatedImage(m.center, m.orientation, database.images.orientationViewer.getBmp(0));
                //drawCircle(m.center, 15, m.toolData.brush, null);
                drawImage(m.entry.image, 0, m.center);
            }

            if(editor != null)
            {
                if (editor.activeTool == EditorTool.Structure)
                {
                    drawImage(database.images.structures[editor.activeStructureType], 0, editor.hoverPos);
                    StructureEntry entry = database.getStructureEntry(editor.activeStructureType);
                    Color hoverColor = Color.FromArgb(128, 160, 240, 160);
                    if (!editor.hoverPosValidForPlacement)
                        hoverColor = Color.FromArgb(128, 240, 160, 160);
                    drawRectangle(editor.hoverPos, (int)entry.radius, hoverColor);
                }
                if(editor.activeTool == EditorTool.Select && editor.selectedStructureIndex != -1)
                {
                    Structure seletedStructure = editor.level.structures[editor.selectedStructureIndex];
                    Color selectedColor = Color.FromArgb(128, 150, 150, 250);
                    drawRectangle(seletedStructure.center, (int)seletedStructure.entry.radius, selectedColor);
                }
            }

            resizeScreen(renderWidth, renderHeight);
            gScreen.DrawImage(bmpViewport, new Rectangle(0, 0, renderWidth, renderHeight));
            targetBox.Image = bmpScreen;
        }
    }
}
