using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Cloudbreaker
{
    class LevelHostFileData
    {
        public LevelHostFileData(string _type, double _decryptCost)
        {
            type = _type;
            decryptCost = _decryptCost;
        }
        public double decryptCost;
        public string type;
    }

    class LevelHostData
    {
        public LevelHostData(int _xPos, int _yPos, string _type, Dictionary<string, LevelHostFileData> _files)
        {
            xPos = _xPos;
            yPos = _yPos;
            type = _type;
            files = _files;
        }
        public int xPos;
        public int yPos;
        public string type;
        public Dictionary<string, LevelHostFileData> files;
    }
    class LevelFile
    {
        public Dictionary<string, LevelHostData> hosts;

        public LevelFile()
        {
            var host0files = new Dictionary<string, LevelHostFileData>();
            host0files.Add("host0file0", new LevelHostFileData("text.txt", 4.0));
            host0files.Add("host0file1", new LevelHostFileData("dat.dat", 10.0));
            LevelHostData host0 = new LevelHostData(1, 4, "host0type", host0files);

            hosts = new Dictionary<string, LevelHostData>();
            hosts.Add("hostname0", host0);
            hosts.Add("hostname1", host0);
        }

        public void serializeToFile(string filename)
        {
            string output = JsonConvert.SerializeObject(this, Formatting.Indented);
            System.IO.File.WriteAllText(filename, output);
        }
    }
}
