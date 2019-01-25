using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cloudbreaker.Game
{
    public class ProgressBarEvent
    {

    }

    public class Threat
    {
        public Threat(ThreatMusicInfo _music)
        {
            type = ThreatType.Music;
            music = _music;
        }
        public double totalTime()
        {
            return endTime - startTime;
        }
        public ThreatType type;

        public double startTime, endTime;
        public double progressBarTotal, progressBarCurrent = 0.0;
        public List<ProgressBarEvent> events;

        public ThreatMusicInfo music;
    }

    public class ThreatBar
    {
        public ThreatBar(PlayerName _player)
        {
            player = _player;
        }
        public PlayerName player;
        public List<Threat> threats = new List<Threat>();
    }

    public class ThreatManager
    {
        public ThreatManager()
        {

        }
        //public ThreatBar hackerBar = new ThreatBar(PlayerName.Hacker);
        public ThreatBar fighterBar = new ThreatBar(PlayerName.Fighter);
        public ThreatBar rogueBar = new ThreatBar(PlayerName.Rogue);
    }
}
