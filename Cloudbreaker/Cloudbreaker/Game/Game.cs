using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace Cloudbreaker.Game
{
    public class Game
    {
        Game(LevelFile _level)
        {
            level = _level;
            makeInitialThreats(threat.fighterBar);
            makeInitialThreats(threat.rogueBar);
        }

        void makeInitialThreats(ThreatBar bar)
        {
            double startTime = Util.uniform(0.25, 0.75) * Constants.ThreatBarTotalTime;
            while(startTime < Constants.ThreatBarTotalTime)
            {
                
            }
            //double startTime = Util.uniform(level.info.minSlackTime, level.info.maxSlackTime);
        }

        Threat makeRandomThreat(PlayerName player)
        {
            ThreatType type = level.sampleRandomThreatType(player);
            if (type == ThreatType.ButtonCascade)
            {
                var info = new ThreatButtonCascadeInfo(level);
                Threat newThreat = new Threat(info);
            }
            return null;
        }

        public LevelInfo level;
        public HackerConsole console = new HackerConsole();
        public ThreatManager threat = new ThreatManager();
    }
}
