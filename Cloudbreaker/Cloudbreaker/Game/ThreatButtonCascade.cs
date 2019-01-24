using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cloudbreaker.Game
{
    public class ButtonCascadeEntry
    {
        public enum State
        {
            Active,  //white
            Success, //green
            Failure, //red
            Untested //yellow
        }

        public State state;
        public int columnIndex;
        public double startTime;
        public double endTime;

        public Trigger trigger;
    }

    public class ThreatButtonCascadeInfo
    {
        public ThreatButtonCascadeInfo(LevelInfo level)
        {
            columnCount = Util.randExclusive(level.minColumnCount, level.maxColumnCount + 1);

        }
        public double totalTime;
        public double currentTime;
        public int columnCount;
        public List<ButtonCascadeEntry> entries;
    }
}
