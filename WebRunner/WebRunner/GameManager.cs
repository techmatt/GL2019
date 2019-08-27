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
        ImageDatabase imageDatabase = new ImageDatabase();
        public VisionManager vision = new VisionManager();

        public void reset()
        {
            state = new GameState();
        }

        void step()
        {
            
        }

        public void stepAndRender(int renderWidth, int renderHeight)
        {
            Bitmap webcamBitmap = vision.processWebcamImage(out state.markers, data);
            //image.Save("test.png");
            step();
            screen.render(webcamBitmap, state, renderWidth, renderHeight);
        }
    }
}
