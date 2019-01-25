using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Cloudbreaker.Game
{
    public class LevelThreatInfo
    {
        public LevelThreatInfo(ThreatType _type, double _frequency)
        {
            type = _type;
            frequency = _frequency;
        }
        public ThreatType type;
        public double frequency;
    }

    public class LevelInfo
    {
        public LevelInfo()
        {
            threatInfo.Add(new LevelThreatInfo(ThreatType.Music, 1.0));
        }

        public ThreatType sampleRandomThreatType(PlayerName player)
        {
            double s = Util.uniform(0.0, 1.0);
            int threatIndex = 0;
            while(s > threatInfo[threatIndex].frequency && threatIndex != threatInfo.Count - 1)
            {
                threatIndex++;
            }
            return threatInfo[threatIndex].type;
        }

        public double minThreatTime = 5.0, maxThreatTime = 10.0;
        public double minSlackTime = 1.0, maxSlackTime = 5.0;

        //
        // Song constants
        //
        public int minColumnCount = 3, maxColumnCount = 5;
        public double minNoteDuration = 1.0, maxNoteDuration = 2.0;
        public int minNoteAddAttempts = 1, maxNoteAddAttempts = 10;

        public List<LevelThreatInfo> threatInfo = new List<LevelThreatInfo>();
    }

    public class LevelFile
    {
        public LevelFile()
        {

        }

        public LevelInfo info = new LevelInfo();

        public void serializeToFile(string filename)
        {
            string output = JsonConvert.SerializeObject(this, Formatting.Indented);
            System.IO.File.WriteAllText(filename, output);
        }

        static public LevelFile deserializeFromFile(string filename)
        {
            string text = System.IO.File.ReadAllText(filename);
            LevelFile result = JsonConvert.DeserializeObject<LevelFile>(text);
            return result;
        }
    }
}
