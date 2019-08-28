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
            reset();
        }

        GameData data = new GameData();
        GameState state;
        GameScreen screen;
        public VisionManager vision = new VisionManager();

        public void reset()
        {
            state = new GameState(Constants.missionDir + "testMission.txt");
        }

        void step()
        {
            state.updateViewport(state.viewport.pMin.x + 1.0);
        }

        public void stepAndRender(int renderWidth, int renderHeight)
        {
            Bitmap webcamBitmap = vision.processWebcamImage(out state.markers, data);
            //image.Save("test.png");
            step();
            screen.render(webcamBitmap, data, state, renderWidth, renderHeight);
        }
    }
}
