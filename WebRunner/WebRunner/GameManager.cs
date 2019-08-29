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
        public GameManager(PictureBox _targetBox)
        {
            screen = new GameScreen(_targetBox, data);
        }

        public GameData data = new GameData();
        public GameState state;
        GameScreen screen;
        public EditorManager editor = null;
        public VisionManager vision = new VisionManager();

        public void reset(string missionName)
        {
            state = new GameState(Constants.missionDir + missionName + ".txt");
        }

        void step()
        {
            if(editor == null)
                state.updateViewport(20.0);
        }

        public void stepAndRender(int renderWidth, int renderHeight)
        {
            Bitmap webcamBitmap = vision.processWebcamImage(out state.markers, data);
            //image.Save("test.png");
            step();
            screen.render(webcamBitmap, data, state, editor, renderWidth, renderHeight);
        }
    }
}
