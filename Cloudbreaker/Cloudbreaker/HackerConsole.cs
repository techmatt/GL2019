using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace Cloudbreaker
{

    public class File
    {
        public string filename;
        public bool decrypted;
    }
    public class Host
    {
        public string name;
        public string accessLevel;
        public string room;
        public List<File> files;
    }

    public class HackerConsole : IPanel
    {
        public Point origin = new Point(10, 10);
        public Point dimension = new Point(600, 535);
        public List<string> history = new List<string>();
        public string activeCommand;

        public string accessLevel;
        public string serverName;

        public const int ySpacing = 20;

        public HackerConsole()
        {
            accessLevel = "root";
            serverName = "deckerbox";
            //history.Add("ABC");
            //history.Add("defg");
            //history.Add("HiJKL");
        }

        public void Render(Display d)
        {
            d.g.FillRectangle(d.util.consoleBackgroundBrush, new Rectangle(origin.X, origin.Y, dimension.X, dimension.Y));
            for(int element = 0; element < history.Count; element++)
            {
                int yStart = dimension.Y - (history.Count - element + 1) * ySpacing;
                if (yStart < -ySpacing) continue;
                d.g.DrawString(history[element], d.util.consoleFont, d.util.consoleFontBrush, new Point(origin.X + 4, origin.Y + yStart));
            }

            int blockUnicode = 9608;
            char blockChar = (char)blockUnicode;
            d.g.DrawString(accessLevel + "@" + serverName + " $ " + activeCommand + blockChar.ToString(), d.util.consoleFont, d.util.consoleFontBrush, new Point(origin.X + 4, origin.Y + dimension.Y - ySpacing));
        }

        public void processCurrentCommand()
        {
            history.Add(accessLevel + "@" + serverName + " $ " + activeCommand);

            bool valid = false;

            if(!valid)
            {
                history.Add("invalid command: " + activeCommand);
                foreach(string s in usage())
                {
                    history.Add(s);
                }
            }

            activeCommand = "";
        }

        public void keyPress(char c)
        {
            if(c == (char)8)
            {
                if (activeCommand.Length > 0)
                    activeCommand = activeCommand.Remove(activeCommand.Length - 1);
                return;
            }
            bool valid = false;
            if (c >= 'A' && c <= 'Z') valid = true;
            if (c >= 'a' && c <= 'z') valid = true;
            if (c >= '0' && c <= '9') valid = true;
            if (c == '.' || c == ' ' || c == '?') valid = true;
            if (!valid)
                return;

            activeCommand += c;
        }

        List<string> status()
        {
            List<string> result = new List<string>();
            result.Add("*** valid commands:");
            result.Add("  hosts: shows list of known hostnames and access levels");
            result.Add("  connect <hostname>: connects to given host");
            result.Add("  status: status of current host and unlocked local commands");
            result.Add("  scan: search current host for potentially relevant files");
            result.Add("  decrypt <filename>: begins processing given file");
            result.Add("  probe: script for interfacing with logic probe");
            return result;
        }

        List<string> usage()
        {
            List<string> result = new List<string>();
            result.Add("*** valid commands:");
            result.Add("  hosts: shows list of known hostnames and access levels");
            result.Add("  connect <hostname>: connects to given host");
            result.Add("  status: status of current host");
            result.Add("  scan: search current host for potentially relevant files");
            result.Add("  decrypt <filename>: begins processing given file");
            result.Add("  probe: script for interfacing with logic probe");
            return result;
        }
    }
}
