using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

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
        GameState state;
        GameManager manager;

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

            pictureBoxDecoder.Top = 0;
            pictureBoxDecoder.Left = 0;
            pictureBoxDecoder.Width = Constants.decoderWindowWidth;
            pictureBoxDecoder.Height = Constants.decoderWindowHeight;

            pictureBoxPulse.Top = 0;
            pictureBoxPulse.Left = 0;
            pictureBoxPulse.Width = Constants.pulseWindowWidth;
            pictureBoxPulse.Height = Constants.pulseWindowHeight;

            manager = new GameManager(pictureBoxDecoder, pictureBoxPulse);

            timerRender.Enabled = true;
        }

        private void timerRender_Tick(object sender, EventArgs e)
        {
            manager.step();
            manager.render();
        }
    }
}
