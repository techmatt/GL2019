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
            screen = new GameScreen(_targetBox, Constants.renderWidth, Constants.renderHeight, data);
            reset();
        }

        GameData data = new GameData();
        GameState state;
        GameScreen screen;
        VisionManager vision = new VisionManager();

        public void reset()
        {
            state = new GameState();
        }

        void step()
        {

        }

        public void stepAndRender()
        {
            Bitmap webcamBitmap = vision.processWebcamImage(out state.markers, data);
            //image.Save("test.png");
            step();
            screen.render(webcamBitmap, state);
        }
    }
}
