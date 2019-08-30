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
            editor.manager = this;
            editor.database = database;
        }

        public GameDatabase database = new GameDatabase();
        public GameState state;
        GameScreen screen;
        public EditorManager editor = null;
        public VisionManager vision = new VisionManager();

        public void startMission(string missionName, string levelName)
        {
            state = new GameState(missionName, levelName, database);
            editor.level = state.levels[0];
        }

        void step()
        {
            double deltaX = 0.0;
            state.updateViewport(deltaX);

            foreach(GameLevel level in state.activeLevels)
            {
                level.updateStructures(database, state);
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
