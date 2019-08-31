using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WebRunner
{
    class GameManager
    {
        public GameManager(PictureBox _targetBox, EditorManager _editor)
        {
            screen = new GameScreen(_targetBox, database);
            editor = _editor;
            if (editor != null)
            {
                editor.manager = this;
                editor.database = database;
            }
        }

        public GameDatabase database = new GameDatabase();
        public GameState state;
        GameScreen screen;
        public EditorManager editor = null;
        public VisionManager vision = new VisionManager();

        public void startMission(string missionName, string levelName)
        {
            state = new GameState(missionName, levelName, database);
            if(editor != null)
                editor.level = state.levels[0];
        }

        void moveRunner(Marker m, Runner runner)
        {
            Vec2 delta = m.center - runner.center;
            double dist = delta.length();
            double speed = 0.0;
            if (dist < Constants.runMaxDistA) speed = Constants.runSpeed;
            else if (dist < Constants.runMaxDistB) speed = Util.linearMap(dist, Constants.runMaxDistA, Constants.runMaxDistB, Constants.runSpeed, 0.0);

            if (speed > 0.0)
            {
                delta = delta.getNormalized();
                Vec2 newCenter = runner.center + delta * speed;
                GameLevel mainLevel = state.activeLevels[0];
                var closest = Util.closestStructure(mainLevel.structures, newCenter, database.runnerBlockingStructures);
                if (closest.Item2 > Constants.runnerRadius)
                    runner.center = newCenter;
            }
        }

        void step()
        {
            double deltaX = 0.0;
            state.updateViewport(deltaX);

            foreach(GameLevel level in state.activeLevels)
            {
                level.updateStructures(database, state);
            }

            foreach (Marker m in state.markers)
            {
                if (m.entry.type == ToolType.RunA && state.activeRunnerA != null)
                {
                    moveRunner(m, state.activeRunnerA);
                }
                if (m.entry.type == ToolType.RunB && state.activeRunnerB != null)
                {
                    moveRunner(m, state.activeRunnerB);
                }
            }
        }

        public void stepAndRender(int renderWidth, int renderHeight)
        {
            Bitmap webcamBitmap = vision.processWebcamImage(out state.markers, database);
            //image.Save("test.png");
            step();
            screen.render(webcamBitmap, state, editor, renderWidth, renderHeight);
        }
    }
}
