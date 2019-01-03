using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Cloudbreaker
{
    public partial class HackerWindow : Form
    {
        Game game;
        KeysConverter kc = new KeysConverter();

        public HackerWindow(Game _game)
        {
            InitializeComponent();

            game = _game;
        }

        private void HackerWindow_KeyDown(object sender, KeyEventArgs e)
        {
            string text = kc.ConvertToString(e.KeyData);
            for(int i = 0; i < 26; i++)
            {
                char c = (char)('A' + i);
                if(text == c.ToString())
                    game.console.keyPress((char)('a' + i));
                if (text == "Shift+" + c.ToString())
                    game.console.keyPress((char)('A' + i));
            }
            for (int i = 0; i < 10; i++)
            {
                char c = (char)('0' + i);
                if (text == c.ToString() || text == "NumPad" + c.ToString())
                    game.console.keyPress((char)('0' + i));
            }
            if (text == "Back" || text == "Delete")
                game.console.keyPress((char)8);
            if (text == "Enter")
                game.console.processCurrentCommand();
            if(text == "OemPeriod")
                game.console.keyPress('.');
            if (text == "Shift+OemQuestion")
                game.console.keyPress('?');
            if (text == "Space")
                game.console.keyPress(' ');
            Console.WriteLine(text);
        }

        private void HackerWindow_Load(object sender, EventArgs e)
        {

        }
    }
}
