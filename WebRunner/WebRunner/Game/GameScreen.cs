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

        public void drawLine(Vec2 p0, Vec2 p1, Pen penA, Pen penB)
        {
            gViewport.DrawLine(penA, (int)p0.x, (int)p0.y, (int)p1.x, (int)p1.y);
            gViewport.DrawLine(penB, (int)p0.x, (int)p0.y, (int)p1.x, (int)p1.y);
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

        public void drawRotatedImage(Vec2 center, Vec2 orientation, Bitmap bmp, double scale = 1.0)
        {
            drawRotatedImage(center, (float)orientation.angle(), bmp, scale);
        }

        public void drawRotatedImage(Vec2 center, float angle, Bitmap bmp, double scale = 1.0)
        {
            double hw = bmp.Width * 0.5;
            double hh = bmp.Height * 0.5;
            gViewport.TranslateTransform((float)center.x, (float)center.y);
            if (scale != 1.0)
                gViewport.ScaleTransform((float)scale, (float)scale);
            gViewport.RotateTransform(angle);
            gViewport.TranslateTransform((float)-hw, (float)-hh);

            gViewport.DrawImage(bmp, 0, 0);
            gViewport.ResetTransform();
        }

        public void drawRunnerHealthCircle(GameDatabase database, Runner r)
        {
            if (r == null || r.curHealth >= Constants.runnerMaxHealth)
                return;

            int maxRadius = 30;
            double radius = (1.0 - r.curHealth / Constants.runnerMaxHealth) * maxRadius;
            drawCircle(r.center, maxRadius, null, database.cameraPenThin);
            drawCircle(r.center, (int)radius, database.runnerHealthInterior, null);
        }

        public void renderLaserPath(LaserPath path, Pen penA, Pen penB)
        {
            if (path == null)
                return;
            for (int beamIdx = 0; beamIdx < path.beamPoints.Count - 1; beamIdx++)
            {
                drawLine(path.beamPoints[beamIdx], path.beamPoints[beamIdx + 1], penA, penB);
            }
        }

        public void drawRunnerLaser(GameState state, GameDatabase database, Runner r)
        {
            if (r == null)
                return;

            if(state.curLevel.toolsAcquired[ToolType.Kusanagi])
            {
                Color c0 = Color.FromArgb(Util.randInt(50, 200), Util.randInt(50, 200), Util.randInt(50, 200), Util.randInt(50, 200));
                Color c1 = Color.FromArgb(Util.randInt(50, 200), Util.randInt(50, 200), Util.randInt(50, 200), Util.randInt(50, 200));
                renderLaserPath(r.laserPath, new Pen(c0, (float)Util.uniform(6.0, 12.0)), new Pen(c1, (float)Util.uniform(6.0, 12.0)));
            }
            else if (r.hasLaser)
            {
                drawCircle(r.laserOrigin(), 7, database.laserIndicatorInterior, database.cameraPenThin);
                renderLaserPath(r.laserPath, database.laserGunRayA, database.laserGunRayB);
            }
            
        }

        public void drawIcon(Bitmap bmp, Color color, int xStart)
        {
            var rect = new Rectangle(xStart, 648, bmp.Width, bmp.Height);
            gViewport.FillRectangle(new SolidBrush(color), rect );
            gViewport.DrawImage(bmp, rect.X, rect.Y);
        }

        public void render(Bitmap webcamImage, GameState state, EditorManager editor, int renderWidth, int renderHeight)
        {
            gViewport.Clear(Color.Black);

            gViewport.DrawImage(webcamImage, new Rectangle(0, 0, (int)Constants.viewportSize.x, (int)Constants.viewportSize.y));

            ImageEntry backgroundImg = database.images.getBackground(state.curLevel.tilesetName, false);
            Vec2 bkgStart = state.curLevel.worldRect.pMin - state.viewport.pMin;
            gViewport.DrawImage(backgroundImg.getBmp(0), (int)bkgStart.x, (int)bkgStart.y);
            state.curLevel.render(this, database, state, editor);

            drawRunnerHealthCircle(database, state.activeRunners[0]);
            drawRunnerHealthCircle(database, state.activeRunners[1]);

            drawRunnerLaser(state, database, state.activeRunners[0]);
            drawRunnerLaser(state, database, state.activeRunners[1]);

            Vec2 viewportOrigin = state.viewport.pMin;
            foreach (Structure structure in state.curFrameTemporaryStructures)
            {
                drawImage(database.images.getStructureImage(structure.type, null), structure.curImgInstanceHash, structure.center - viewportOrigin);
            }

            foreach (Marker m in state.markers)
            {
                if (!m.available)
                    continue;

                if(m.entry.type == ToolType.Mirror)
                    drawRotatedImage(m.screenCenter, m.orientation, database.images.mirrorOrientation.getBmp(0));

                if(m.entry.type == ToolType.Medpack)
                {
                    Rectangle rect = new Rectangle((int)(m.screenCenter.x - Constants.medPackRadius),
                                                   (int)(m.screenCenter.y - Constants.medPackRadius), Constants.medPackRadius * 2, Constants.medPackRadius * 2);
                    gViewport.FillEllipse(new SolidBrush(Color.FromArgb(40, 50, 200, 50)), rect);
                }
                if (m.entry.type == ToolType.Dyson)
                {
                    Rectangle rect = new Rectangle((int)(m.screenCenter.x - Constants.dysonRadius),
                                                   (int)(m.screenCenter.y - Constants.dysonRadius), Constants.dysonRadius * 2, Constants.dysonRadius * 2);
                    gViewport.FillEllipse(new SolidBrush(Color.FromArgb(40, 200, 200, 200)), rect);
                }
                if (m.entry.type == ToolType.Kusanagi)
                {
                    Rectangle rect = new Rectangle((int)(m.screenCenter.x - Constants.kusanagiRadius),
                                                   (int)(m.screenCenter.y - Constants.kusanagiRadius), Constants.kusanagiRadius * 2, Constants.kusanagiRadius * 2);
                    gViewport.FillEllipse(new SolidBrush(Color.FromArgb(40, 200, 50, 50)), rect);
                }

                //drawCircle(m.center, 15, m.toolData.brush, null);
                drawImage(m.entry.image, 0, m.screenCenter);
            }

            if(editor != null)
            {
                if (editor.activeTool == EditorTool.Structure)
                {
                    drawImage(database.images.getStructureImage(editor.activeStructureType, editor.level.tilesetName), 0, editor.hoverPos);
                    StructureEntry entry = database.getStructureEntry(editor.activeStructureType);
                    Color hoverColor = Color.FromArgb(128, 160, 240, 160);
                    if (!editor.hoverPosValidForPlacement)
                        hoverColor = Color.FromArgb(128, 240, 160, 160);
                    drawRectangle(editor.hoverPos, (int)entry.radius, hoverColor);
                }
                if(editor.activeTool == EditorTool.Select && editor.selectedStructureIndex != -1 && editor.selectedStructureIndex < editor.level.structures.Count)
                {
                    Structure seletedStructure = editor.level.structures[editor.selectedStructureIndex];
                    Color selectedColor = Color.FromArgb(128, 150, 150, 250);
                    drawRectangle(seletedStructure.center, (int)seletedStructure.entry.radius, selectedColor);
                }
            }

            int totalSecondsElapsed = (int)((DateTime.Now - state.gameStartTime).TotalSeconds);
            string totalMinutesText = (totalSecondsElapsed / 60).ToString().PadLeft(2, '0');
            string totalSecondsText = (totalSecondsElapsed % 60).ToString().PadLeft(2, '0');

            //int levelSecondsElapsed = (int)((DateTime.Now - state.levelStartTime).TotalSeconds);
            //int levelSecondsRemaining = maxSecondsAllowed - levelSecondsElapsed;
            //string levelMinutesText = (levelSecondsRemaining / 60).ToString().PadLeft(2, '0');
            //string levelSecondsText = (levelSecondsRemaining % 60).ToString().PadLeft(2, '0');

            int totalSecondsRemaining = (int)state.maxSecondsAllowed - totalSecondsElapsed;
            string remainingMinutesText = (totalSecondsRemaining / 60).ToString().PadLeft(2, '0');
            string reaminingSecondsText = (totalSecondsRemaining % 60).ToString().PadLeft(2, '0');

            gViewport.DrawString("Run time: " + totalMinutesText + ":" + totalSecondsText, Constants.consoleFont, Constants.consoleFontBrush, new Point(111, 648));
            gViewport.DrawString("Time remaining: " + remainingMinutesText + ":" + reaminingSecondsText, Constants.consoleFont, Constants.consoleFontBrush, new Point(15, 680));

            gViewport.DrawString("Sector " + (state.curLevelIndex + 1).ToString() + " of " + state.allLevels.Count.ToString(), Constants.consoleFont, Constants.consoleFontBrush, new Point(890, 648));

            int objectivesRemaining = state.curLevel.objectivesTotal - state.curLevel.objectivesAchieved;
            if(totalSecondsElapsed / 4 % 2 == 0)
                gViewport.DrawString(objectivesRemaining.ToString() + " objectives remaining", Constants.consoleFont, Constants.consoleFontBrush, new Point(890, 680));
            else
                gViewport.DrawString(state.totalCasualties + " casualties", Constants.consoleFont, Constants.consoleFontBrush, new Point(890, 680));

            const int iconSpacing = 75;
            if (state.curLevel.toolsAcquired[ToolType.Dyson])
            {
                drawIcon(database.images.dysonIcon.bmp[0], Color.FromArgb(150, 200, 50, 50), 370 + iconSpacing * 0);
            }
            if (state.curLevel.toolsAcquired[ToolType.Mirror])
            {
                drawIcon(database.images.mirrorIcon.bmp[0], Color.FromArgb(150, 153, 217, 234), 370 + iconSpacing * 1);
            }
            if (state.curLevel.toolsAcquired[ToolType.Medpack])
            {
                drawIcon(database.images.medpackIcon.bmp[0], Color.FromArgb(150, 50, 200, 50), 370 + iconSpacing * 2);
            }
            if (state.curLevel.toolsAcquired[ToolType.CloakingField])
            {
                drawIcon(database.images.cloakingFieldIcon.bmp[0], Color.FromArgb(150, 200, 200, 50), 370 + iconSpacing * 3);
            }
            if (state.curLevel.toolsAcquired[ToolType.Kusanagi])
            {
                drawIcon(database.images.kusanagiIcon.bmp[0], Color.FromArgb(150, 200, 50, 200), 370 + iconSpacing * 4);
            }

            if ((state.activeRunners[0] != null && state.activeRunners[0].hasLaser) ||
               (state.activeRunners[1] != null && state.activeRunners[1].hasLaser))
            {
                gViewport.DrawImage(database.images.laserGunIcon.bmp[0], 777, 648);
            }

            resizeScreen(renderWidth, renderHeight);
            gScreen.DrawImage(bmpViewport, new Rectangle(0, 0, renderWidth, renderHeight));
            targetBox.Image = bmpScreen;
        }
    }
}
