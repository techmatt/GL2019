using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Pulse
{
    class GameManager
    {
        public GameManager(PictureBox _pictureBoxDecoder, PictureBox _pictureBoxPulse)
        {
            state = new GameState(this);
            screenDecoder = new GameScreen(_pictureBoxDecoder, database);
            screenPulse = new GameScreen(_pictureBoxPulse, database);
            sound.playSpeech("welcome to sector 1");
        }

        public GameDatabase database = new GameDatabase();
        public GameState state;
        GameScreen screenDecoder, screenPulse;
        public JoystickManager joystick = new JoystickManager();
        public SoundManager sound = new SoundManager();

        public void step()
        {
            joystick.poll();
            foreach (RunnerJoystickState j in joystick.joysticks)
            {
                foreach (GamepadButton b in j.buttonsToProcess)
                {
                    Console.WriteLine(b.ToString());
                }
                j.buttonsToProcess.Clear();
            }
        }

        public void render()
        {
            state.level.alphabet.updateTextures();
            screenDecoder.renderDecoder(state, Constants.decoderWindowWidth, Constants.decoderWindowHeight);
            screenPulse.renderPulse(state, Constants.pulseWindowWidth, Constants.pulseWindowHeight);
        }
    }
}
