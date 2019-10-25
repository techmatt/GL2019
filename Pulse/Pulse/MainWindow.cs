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

        public Form decoderWindow = null;
        public Form pulseWindow = null;
        GameState state;

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
            state = new GameState();
            decoderWindow = new DecoderWindow();
            pulseWindow = new PulseWindow();
            decoderWindow.Show();
            pulseWindow.Show();
            timerRender.Enabled = true;
        }
    }
}
