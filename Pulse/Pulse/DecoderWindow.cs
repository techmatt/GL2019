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
    public partial class DecoderWindow : Form
    {
        public DecoderWindow()
        {
            InitializeComponent();
        }

        private void DecoderWindow_Load(object sender, EventArgs e)
        {

        }

        public PictureBox getPictureBox()
        {
            return pictureBoxDecoder;
        }
    }
}
