using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cloudbreaker.Game
{
    public class MusicalNote
    {
        public enum State
        {
            Active,  //white
            Success, //green
            Failure, //red
            Untested //yellow
        }

        public double middleTime()
        {
            return (startTime + endTime) * 0.5;
        }
        public double duration()
        {
            return endTime - startTime;
        }

        public int columnIndex;
        public double startTime;
        public double endTime;

        public Trigger trigger;

        public State state;
    }

    public class ThreatMusicInfo
    {
        public ThreatMusicInfo(LevelInfo level, PlayerName primaryPlayer)
        {
            columnCount = Util.randExclusive(level.minColumnCount, level.maxColumnCount + 1);

            var columnIndices = new int[columnCount];
            for (int i = 0; i < columnCount; i++)
                columnIndices[i] = i;

            int[] shuffledColumns = columnIndices.OrderBy(x => Util.rnd.Next()).ToArray();
            addRandomButton(level, shuffledColumns[0], PlayerName.Fighter);
            addRandomButton(level, shuffledColumns[1], PlayerName.Hacker);
            addRandomButton(level, shuffledColumns[2], PlayerName.Rogue);
            for(int i = 3; i < columnCount; i++)
                addRandomButton(level, shuffledColumns[i], primaryPlayer);

            int noteAddAttempts = Util.randExclusive(level.minNoteAddAttempts, level.maxNoteAddAttempts + 1);
            for (int i = 0; i < noteAddAttempts; i++)
                addRandomButton(level, Util.randExclusive(0, columnCount), primaryPlayer);
        }

        public void addRandomButton(LevelInfo level, int column, PlayerName player)
        {
            MusicalNote e = new MusicalNote();
            e.columnIndex = column;
            double noteLength = Util.uniform(level.minNoteDuration, level.maxNoteDuration);
            e.startTime = Util.uniform(0.0, totalSongTime - noteLength);
            e.endTime = noteLength;

            if (hasCollision(e))
                return;

            e.trigger = Trigger.makeRandom(player);
            entries.Add(e);
        }

        public bool hasCollision(MusicalNote m1)
        {
            const double bufferTime = 0.25;
            foreach(MusicalNote m2 in entries)
            {
                if (m1.columnIndex != m2.columnIndex) continue;
                double dist = Math.Abs(m1.middleTime() - m2.middleTime());
                if (dist < (m1.duration() + m2.duration()) * 0.5 + bufferTime)
                    return true;
            }
            return false;
        }

        public double totalSongTime;
        public double currentSongTime;
        public int columnCount;
        public List<MusicalNote> entries;
    }
}
