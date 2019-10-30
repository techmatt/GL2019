using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO.Ports;
using System.IO;
using System.Text.RegularExpressions;
using System.Diagnostics;

namespace Pulse
{
    public partial class MainWindow : Form
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        public DecoderWindow decoderWindow = null;
        public PulseWindow pulseWindow = null;
        GameManager manager;
        SerialPort port = new SerialPort("COM3", 115200, Parity.None, 8, StopBits.One);

        SoundManager scanSounds = null;
        GameDatabase scanGlyphDatabase = null;
        int scanGlyphIndex = -1;
        List<string> scanGlyphStrings = null;
        Stopwatch stopwatch = new Stopwatch();
        FrameTimer frameTimer = new FrameTimer();
        
        public void killAllWindows()
        {
            if (decoderWindow != null)
                decoderWindow.Close();
            if (pulseWindow != null)
                pulseWindow.Close();
        }

        private void buttonStart_Click(object sender, EventArgs e)
        {
            killAllWindows();
            decoderWindow = new DecoderWindow();
            pulseWindow = new PulseWindow();
            decoderWindow.Show();
            pulseWindow.Show();

            PictureBox pictureBoxDecoder = decoderWindow.getPictureBox();
            PictureBox pictureBoxPulse = pulseWindow.getPictureBox();

            int borderWidth = (decoderWindow.Width - decoderWindow.ClientSize.Width);
            int borderHeight = (decoderWindow.Height - decoderWindow.ClientSize.Height);

            pictureBoxDecoder.Top = 0;
            pictureBoxDecoder.Left = 0;
            pictureBoxDecoder.Width = Constants.decoderWindowWidth;
            pictureBoxDecoder.Height = Constants.decoderWindowHeight;
            decoderWindow.Width = Constants.decoderWindowWidth + borderWidth;
            decoderWindow.Height = Constants.decoderWindowHeight + borderHeight;

            pictureBoxPulse.Top = 0;
            pictureBoxPulse.Left = 0;
            pictureBoxPulse.Width = Constants.pulseWindowWidth;
            pictureBoxPulse.Height = Constants.pulseWindowHeight;
            pulseWindow.Width = Constants.pulseWindowWidth + borderWidth;
            pulseWindow.Height = Constants.pulseWindowHeight + borderHeight;

            manager = new GameManager(pictureBoxDecoder, pictureBoxPulse);

            stopwatch.Restart();
            timerRender.Enabled = true;
        }

        private void timerRender_Tick(object sender, EventArgs e)
        {
            string scannerID = null;
            string glyphID = null;
            if (Constants.useSerialPort)
            {
                if (!port.IsOpen)
                    port.Open();

                if (port.BytesToRead > 0)
                {
                    //byte[] buffer = new byte[port.BytesToRead];
                    //int bytesRead = port.Read(buffer, 0, port.BytesToRead);
                    string text = port.ReadExisting();
                    string[] parts = text.Split(':');
                    if (parts.Length != 2)
                    {
                        Console.WriteLine("unknown serial text: " + text);
                        return;
                    }
                    scannerID = parts[0];
                    glyphID = parts[1];
                    glyphID = Regex.Replace(glyphID, @"\t|\n|\r", "");
                    if (scanGlyphIndex != -1)
                    {
                        scanSounds.playWAVFile("success.wav");
                        scanGlyphStrings.Add(glyphID);
                        scanGlyphIndex++;
                        if (scanGlyphIndex == Constants.totalGlyphCount)
                        {
                            File.WriteAllLines(Constants.glyphIDsFilename, scanGlyphStrings);
                            scanGlyphIndex = -1;
                            scanGlyphStrings = null;
                            labelGlyphText.Text = "Scan Complete";
                        }
                        else
                        {
                            loadCurrentScanGlyph();
                        }
                    }

                    //Console.WriteLine("read " + bytesRead.ToString() + " bytes: " + BitConverter.ToString(buffer));
                    //Console.WriteLine("serial: " + text);
                    // read 69 bytes: [From Bluefruit52]: 50 0 E7 1F 8C 53 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0
                    // read 68 bytes: [From Bluefruit52]: 50 0 59 40 0 A3 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0
                    // read 69 bytes: [From Bluefruit52]: 50 0 19 78 5F A3 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0
                }
            }

            frameTimer.frame();
            labelFPS.Text = "FPS: " + frameTimer.framesPerSecond.ToString();
            if (manager != null)
            {
                double totalTime = stopwatch.Elapsed.TotalSeconds;
                //double deltaT = totalTime - previousFrameTotalSeconds;
                //previousFrameTotalSeconds = stopwatch.Elapsed.TotalSeconds;
                manager.step(scannerID, glyphID, totalTime, frameTimer.secondsPerFrame);
                manager.render();
            }
        }

        private void buttonNextLevel_Click(object sender, EventArgs e)
        {
            if(manager != null)
            {
                manager.state.nextLevel();
            }
        }

        void loadCurrentScanGlyph()
        {
            pictureBoxGlyph.Image = scanGlyphDatabase.images.glyphImages[scanGlyphIndex].bmp;
            labelGlyphText.Text = "Scan Glyph " + scanGlyphIndex.ToString();
        }

        private void buttonRegister_Click(object sender, EventArgs e)
        {
            scanGlyphDatabase = new GameDatabase();
            scanGlyphIndex = 0;
            scanGlyphStrings = new List<string>();
            scanSounds = new SoundManager();
            loadCurrentScanGlyph();

            timerRender.Enabled = true;
        }

        private void fullScreenForm(Form form, PictureBox box, Screen screen)
        {
            form.FormBorderStyle = FormBorderStyle.None;
            form.Top = screen.Bounds.Top;
            form.Left = screen.Bounds.Left;
            form.Width = screen.Bounds.Width;
            form.Height = screen.Bounds.Height;
            box.Top = 0;
            box.Left = 0;
            box.Width = screen.Bounds.Width;
            box.Height = screen.Bounds.Height;
        }

        private void buttonFullscreen_Click(object sender, EventArgs e)
        {
            int flip = checkBoxFlipscreens.Checked ? 0 : 1;
            var screen0 = Screen.AllScreens[(0 + flip) % 2];
            var screen1 = Screen.AllScreens[(1 + flip) % 2];

            fullScreenForm(decoderWindow, decoderWindow.getPictureBox(), screen0);
            fullScreenForm(pulseWindow, pulseWindow.getPictureBox(), screen1);
            //manager.state.decoderStale = true;
        }
    }
}
