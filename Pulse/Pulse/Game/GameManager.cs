using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace Pulse
{
    class GameManager
    {
        public GameManager(PictureBox _pictureBoxDecoder, PictureBox _pictureBoxPulse)
        {
            state = new GameState(this);
            screenDecoder = new GameScreen(_pictureBoxDecoder, database);
            screenPulse = new GameScreen(_pictureBoxPulse, database);

            if(File.Exists(Constants.glyphIDsFilename))
            {
                string[] lines = File.ReadAllLines(Constants.glyphIDsFilename);
                for(int i = 0; i < lines.Length; i++)
                {
                    glyphIDToIndex[lines[i]] = i;
                }
            }
            sound.playSpeech("welcome to sector 1");
        }

        public GameDatabase database = new GameDatabase();
        public GameState state;
        GameScreen screenDecoder, screenPulse;
        public JoystickManager joystick = new JoystickManager();
        public SoundManager sound = new SoundManager();
        public Dictionary<string, int> glyphIDToIndex = new Dictionary<string, int>();

        public void step(string scannerID, string glyphID)
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
            if(glyphID != null)
            {
                if(!glyphIDToIndex.ContainsKey(glyphID))
                {
                    Console.WriteLine("glyphID not found: " + glyphID);
                }
                else
                {
                    int scannedGlyphIndex = glyphIDToIndex[glyphID];
                    string WAVFilename = Constants.scannerIDToWAV["default"];
                    if(!Constants.scannerIDToWAV.ContainsKey(scannerID))
                    {
                        Console.WriteLine("scannerID not found: " + scannerID);
                    }
                    else
                    {
                        WAVFilename = Constants.scannerIDToWAV[scannerID];
                    }
                    sound.playWAVFile(WAVFilename);
                }
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
