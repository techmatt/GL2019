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
    public class CGGame
    {
        CGGame(LevelFile _level)
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
                Threat newThreat = makeRandomThreat(bar.player);
                bar.threats.Add(newThreat);
                startTime += newThreat.totalTime;
            }
            //double startTime = Util.uniform(level.info.minSlackTime, level.info.maxSlackTime);
        }

        Threat makeRandomThreat(PlayerName player)
        {
            ThreatType type = level.sampleRandomThreatType(player);
            Threat newThreat = null;
            if (type == ThreatType.Music)
            {
                var info = new ThreatMusicInfo(level, player);
                newThreat = new Threat(info);
            }
            return newThreat;
        }

        public LevelInfo level;
        public HackerConsole console = new HackerConsole();
        public ThreatManager threat = new ThreatManager();
    }
}
