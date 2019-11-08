using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Pulse
{
    // slow down the pulse and increase note size over time.

    class LevelGenInfo
    {
        public LevelGenInfo(int levelIndex, int expansion)
        {
            difficulty = Math.Min(levelIndex, 10);

            int[] noteCountByDifficulty = { 2, 2, 3, 3, 3, 4, 4, 5, 5, 6, 6, 7};
            int totalNoteCount = noteCountByDifficulty[difficulty];
            for (int i = 0; i < Constants.beamCount; i++)
                beamNoteCounts.Add(0);
            for(int i = 0; i < totalNoteCount; i++)
                beamNoteCounts[i % 3]++;
            beamNoteCounts = beamNoteCounts.Shuffle();

             colorGridX = expansion + 1;
            if(difficulty <= 5)
                colorGridY = 1;
            else
                colorGridY = 2;

            int[] colorCountByDifficulty = {7, 7, 6, 6, 5, 5, 4, 4, 3, 3, 3, 3, 3, 3, 3, 3, 3};
            int colorCount = colorCountByDifficulty[difficulty];
            
            colors = Constants.allColors.Shuffle();
            colors = colors.GetRange(0, colorCount);

            TextureType[] textureTypeByDifficulty = { TextureType.ColorGrid, TextureType.ColorGrid,
                                                      TextureType.TextureGroup1, TextureType.TextureGroup2,
                                                      TextureType.TextureGroup0, TextureType.ColorGrid, TextureType.TextureGroup1,
                                                      TextureType.TextureGroup2, TextureType.ColorGrid,
                                                      TextureType.TextureGroup1, TextureType.ColorGrid ,
                                                      TextureType.ColorGrid, TextureType.ColorGrid };
            textureType = textureTypeByDifficulty[difficulty];
            //validTextureTypes = new List<TextureType>() { TextureType.ColorGrid };

            minNoteLength = 0.15;
            maxNoteLength = 0.25;
        }
        public int difficulty;
        public List<int> beamNoteCounts = new List<int>();
        public int colorGridX, colorGridY;
        public double minNoteLength;
        public double maxNoteLength;
        public List<Color> colors;
        //public List<TextureType> validTextureTypes = new List<TextureType>();
        public TextureType textureType;
    }

    class GlyphState
    {
        public GlyphState(LevelGenInfo info, int textureIndex)
        {
            texture = new Texture(info, textureIndex);
            residualScanStrength = 0.0;
        }
        public Texture texture;
        public double residualScanStrength;
        public Color scannerColor;
    }

    class AlphabetState
    {
        public AlphabetState(LevelGenInfo info)
        {
            if (info.textureType == TextureType.ColorGrid)
            {
                int maxAttemptCount = 100;
                for (int i = 0; i < Constants.totalGlyphCount; i++)
                {
                    for (int attempt = 0; attempt < maxAttemptCount; attempt++)
                    {
                        GlyphState newGlyph = new GlyphState(info, -1);
                        bool isValid = true;
                        foreach (GlyphState g in glyphs)
                        {
                            if (Texture.texturesEquivalent(g.texture, newGlyph.texture))
                                isValid = false;
                        }
                        if (isValid)
                        {
                            glyphs.Add(newGlyph);
                            break;
                        }
                    }
                }
            }
            else
            {
                var textureIndices = new List<int>();
                for (int i = 0; i < Constants.totalGlyphCount; i++)
                    textureIndices.Add(i);
                textureIndices = textureIndices.Shuffle();

                for (int i = 0; i < Constants.totalGlyphCount; i++)
                {
                    GlyphState newGlyph = new GlyphState(info, textureIndices[i]);
                    glyphs.Add(newGlyph);
                }
            }
        }
        public void updateTextures()
        {
            foreach(GlyphState g in glyphs)
            {
                g.texture.updateBmp();
            }
        }
        public List<GlyphState> glyphs = new List<GlyphState>();
    }

    enum NoteState
    {
        Neutral,
        Success,
        Failed,
        Attempted,
    }
    class Note
    {
        public Note(LevelGenInfo info)
        {
            double noteLength = Util.uniform(info.minNoteLength, info.maxNoteLength);
            start = Util.uniform(0.0, 1.0 - noteLength);
            end = start + noteLength;
            glyphIndex = Util.randInt(0, Constants.totalGlyphCount);
        }
        static public bool notesOverlap(Note n1, Note n2)
        {
            double centerDist = Math.Abs(n2.center() - n1.center());
            double s = (n1.length() + n2.length()) * 0.5;
            return (centerDist < s + 0.001);
        }
        public double center()
        {
            return (start + end) * 0.5;
        }
        public double length()
        {
            return end - start;
        }
        public double start;
        public double end;
        public NoteState state = NoteState.Neutral;
        public int glyphIndex;
    }

    class Beam
    {
        public List<Note> notes = new List<Note>();

        public Beam(LevelGenInfo info, int noteCount, HashSet<int> glyphsUsed)
        {
            int maxNoteAddAttempts = 100;
            for (int i = 0; i < maxNoteAddAttempts && notes.Count < noteCount; i++)
            {
                Note newNote = new Note(info);
                if(canAddNote(newNote, glyphsUsed))
                {
                    notes.Add(newNote);
                    glyphsUsed.Add(newNote.glyphIndex);
                }
            }
        }

        public bool canAddNote(Note newNote, HashSet<int> glyphsUsed)
        {
            if (glyphsUsed.Contains(newNote.glyphIndex))
                return false;
            foreach(Note n in notes)
            {
                if(Note.notesOverlap(n, newNote))
                    return false;
            }
            return true;
        }

        public void recordGlyphScan(GameManager manager, double pulseLocation, int scannedGlyphIndex)
        {
            foreach (Note n in notes)
            {
                if (n.state == NoteState.Success || n.state == NoteState.Failed)
                    continue;

                if (pulseLocation >= n.start - Constants.noteEpsilon && pulseLocation <= n.end + Constants.noteEpsilon)
                {
                    if (n.glyphIndex == scannedGlyphIndex)
                    {
                        n.state = NoteState.Success;
                        manager.sound.playWAVFile("success.wav");
                    }
                    else
                    {
                        n.state = NoteState.Attempted;
                        manager.sound.playWAVFile("failure.wav");
                    }
                }
            }
        }
    }

    class GameLevel
    {
        public GameLevel(int levelIndex)
        {
            int expansion = 0;
            LevelGenInfo info = null;
            while (info == null)
            {
                info = new LevelGenInfo(levelIndex, expansion);
                alphabet = new AlphabetState(info);

                if(alphabet.glyphs.Count != Constants.totalGlyphCount)
                {
                    info = null;
                    expansion++;
                }
            }

            var glyphsUsed = new HashSet<int>();
            for (int i = 0; i < Constants.beamCount; i++)
                beams.Add(new Beam(info, info.beamNoteCounts[i], glyphsUsed));

            pulseSpeed = 0.1;

            resetPulse();
        }

        public void resetPulse()
        {
            pulseLocation = 0.0;
            foreach (Beam b in beams)
            {
                foreach(Note n in b.notes)
                {
                    n.state = NoteState.Neutral;
                }
            }
        }

        public void step(double secondsPerFrame, double pulseSpeedModifier)
        {
            pulseLocation += pulseSpeed * secondsPerFrame * pulseSpeedModifier;
            if(pulseLocation > 1.0)
            {
                victory = true;
                foreach (Beam b in beams)
                {
                    foreach (Note n in b.notes)
                    {
                        if (n.state != NoteState.Success)
                            victory = false;
                    }
                }
                resetPulse();
            }
            foreach (Beam b in beams)
            {
                foreach(Note n in b.notes)
                {
                    if (n.state != NoteState.Success && n.end + Constants.noteEpsilon < pulseLocation)
                        n.state = NoteState.Failed;
                }
            }
            foreach(GlyphState g in alphabet.glyphs)
            {
                if(g.residualScanStrength > 0.0)
                {
                    g.residualScanStrength -= secondsPerFrame;
                }
            }
        }

        public void recordGlyphScan(GameManager manager, int scannedGlyphIndex, Color scannerColor)
        {
            foreach(Beam b in beams)
            {
                b.recordGlyphScan(manager, pulseLocation, scannedGlyphIndex);
            }
            GlyphState g = alphabet.glyphs[scannedGlyphIndex];
            g.residualScanStrength = Constants.residualScanMax;
            g.scannerColor = scannerColor;
        }

        public List<Beam> beams = new List<Beam>();
        public AlphabetState alphabet = null;
        public double pulseLocation = 0.0;
        public bool victory = false;
        public double pulseSpeed;
    }

    class GameState
    {
        public GameState(GameManager _manager, string _teamName)
        {
            manager = _manager;
            teamName = _teamName;
            levelIndex = 0;
            level = new GameLevel(levelIndex);
            remainingTime = Constants.gameTimeInMinutes * 60.0;
            //decoderStale = true;
        }
        public void nextLevel()
        {
            //decoderStale = true;
            levelIndex++;
            manager.sound.playSpeech("sector completed. Advancing to sector " + (levelIndex + 1).ToString());
            level = new GameLevel(levelIndex);
        }
        public void step(double secondsPerFrame, double pulseSpeedModifier)
        {
            level.step(secondsPerFrame, pulseSpeedModifier);
            if(level.victory)
            {
                nextLevel();
            }
        }
        public GameManager manager;
        public GameLevel level;
        public int levelIndex;
        public double remainingTime;
        public string teamName;
        //public bool decoderStale;
    }
}
