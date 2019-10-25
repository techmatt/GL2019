﻿using System;
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
        public LevelGenInfo(int _levelIndex, int expansion)
        {
            levelIndex = _levelIndex;
            int d2 = levelIndex / 2 + 1;
            int d3 = levelIndex / 3 + 1;
            int d4 = levelIndex / 4 + 1;
            minNoteAttempts = d3;
            maxNoteAttempts = d3 + d3;
            colorGridX = expansion + 1;
            colorGridY = 2;
            int colorCount = 0;
            if (levelIndex <= 2) colorCount = 6;
            else if (levelIndex <= 4) colorCount = 5;
            else if (levelIndex <= 6) colorCount = 4;
            else if (levelIndex <= 8) colorCount = 3;
            else colorCount = 2;

            colors = Constants.allColors.Shuffle();
            colors = colors.GetRange(0, colorCount);

            validTextureTypes = new List<TextureType>() { TextureType.ColorGrid };

            minNoteLength = 0.1;
            maxNoteLength = 0.2;
        }
        public int levelIndex;
        public int minNoteAttempts, maxNoteAttempts;
        public int colorGridX, colorGridY;
        public double minNoteLength;
        public double maxNoteLength;
        public List<Color> colors;
        public List<TextureType> validTextureTypes = new List<TextureType>();
    }

    class GlyphState
    {
        public GlyphState(LevelGenInfo info)
        {
            texture = new Texture(info);
        }
        public Texture texture;
    }

    class AlphabetState
    {
        public AlphabetState(LevelGenInfo info)
        {
            int maxAttemptCount = 100;
            for (int i = 0; i < Constants.totalGlyphCount; i++)
            {
                for(int attempt = 0; attempt < maxAttemptCount; attempt++)
                {
                    GlyphState newGlyph = new GlyphState(info);
                    bool isValid = true;
                    foreach(GlyphState g in glyphs)
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
        public List<GlyphState> glyphs = new List<GlyphState>();
    }

    enum NoteStateType
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
        public NoteStateType state = NoteStateType.Neutral;
        public int glyphIndex;
    }

    class Beam
    {
        public Beam(LevelGenInfo info)
        {
            int noteAddAttempts = Util.randInt(info.minNoteAttempts, info.maxNoteAttempts + 1);
            for (int i = 0; i < noteAddAttempts; i++)
            {
                Note newNote = new Note(info);
                if(canAddNote(newNote))
                {
                    notes.Add(newNote);
                }
            }
        }
        public bool canAddNote(Note newNote)
        {
            foreach(Note n in notes)
            {
                if(Note.notesOverlap(n, newNote))
                    return false;
            }
            return true;
        }

        public List<Note> notes = new List<Note>();
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

            for (int i = 0; i < Constants.beamCount; i++)
                beams.Add(new Beam(info));

            pulseSpeed = 0.0001;
        }
        public List<Beam> beams = new List<Beam>();
        public AlphabetState alphabet = null;
        double pulseLocation = 0.0;
        double pulseSpeed;
    }

    class GameState
    {
        public GameState()
        {
            levelIndex = 0;
            level = new GameLevel(levelIndex);
            totalTime = 0.0;
        }
        GameLevel level;
        int levelIndex;
        double totalTime;
    }
}