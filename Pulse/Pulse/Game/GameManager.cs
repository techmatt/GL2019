﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace Pulse
{
    class GameManager
    {
        public GameManager(PictureBox _pictureBoxDecoder, PictureBox _pictureBoxPulse)
        {
            state = new GameState(this);
            screenDecoder = new GameScreen(_pictureBoxDecoder, database);
            screenPulse = new GameScreen(_pictureBoxPulse, database);

            if(File.Exists(Constants.glyphIDsFilename))
            {
                string[] lines = File.ReadAllLines(Constants.glyphIDsFilename);
                for(int i = 0; i < lines.Length; i++)
                {
                    glyphIDToIndex[lines[i]] = i;
                }
            }
            sound.playSpeech("welcome to sector 1. ten minutes remaining");
        }

        public GameDatabase database = new GameDatabase();
        public GameState state;
        GameScreen screenDecoder, screenPulse;
        public JoystickManager joystick = new JoystickManager();
        public SoundManager sound = new SoundManager();
        public Dictionary<string, int> glyphIDToIndex = new Dictionary<string, int>();

        static bool timeInRange(double time, double start, double end)
        {
            return (time >= start && time < end);
        }

        public void step(string scannerID, string glyphID, double newTotalTime)
        {
            joystick.poll();
            foreach (RunnerJoystickState j in joystick.joysticks)
            {
                foreach (GamepadButton b in j.buttonsToProcess)
                {
                    Console.WriteLine(b.ToString());
                }
                j.buttonsToProcess.Clear();
            }
            if(glyphID != null)
            {
                if(!glyphIDToIndex.ContainsKey(glyphID))
                {
                    Console.WriteLine("glyphID not found: " + glyphID);
                }
                else
                {
                    int scannedGlyphIndex = glyphIDToIndex[glyphID];
                    string WAVFilename = Constants.scannerIDToWAV["default"];
                    if(!Constants.scannerIDToWAV.ContainsKey(scannerID))
                    {
                        Console.WriteLine("scannerID not found: " + scannerID);
                    }
                    else
                    {
                        WAVFilename = Constants.scannerIDToWAV[scannerID];
                    }
                    sound.playWAVFile(WAVFilename);
                    state.level.recordGlyphScan(this, scannedGlyphIndex);
                }
            }
            double prevTotalTime = state.totalTime;
            double deltaT = newTotalTime - prevTotalTime;
            state.totalTime = newTotalTime;
            double prevRemainingTime = state.remainingTime;
            state.remainingTime -= deltaT;

            for(double intervalCandidate = 0; intervalCandidate < state.remainingTime + 1.0; intervalCandidate += 10.0)
            {
                if (timeInRange(intervalCandidate, state.remainingTime, prevRemainingTime))
                    sound.playSpeech(Constants.randomPhrases.RandomElement());
            }

            if (timeInRange(10.0      , state.remainingTime, prevRemainingTime)) sound.playSpeech("ten seconds remaining");
            if (timeInRange(30.0      , state.remainingTime, prevRemainingTime)) sound.playSpeech("thirty seconds remaining");
            if (timeInRange(60.0 * 1.0, state.remainingTime, prevRemainingTime)) sound.playSpeech("one minutes remaining");
            if (timeInRange(60.0 * 2.0, state.remainingTime, prevRemainingTime)) sound.playSpeech("two minutes remaining");
            if (timeInRange(60.0 * 3.0, state.remainingTime, prevRemainingTime)) sound.playSpeech("three minutes remaining");
            if (timeInRange(60.0 * 4.0, state.remainingTime, prevRemainingTime)) sound.playSpeech("four minutes remaining");
            if (timeInRange(60.0 * 5.0, state.remainingTime, prevRemainingTime)) sound.playSpeech("five minutes remaining");
            if (timeInRange(60.0 * 6.0, state.remainingTime, prevRemainingTime)) sound.playSpeech("six minutes remaining");
            if (timeInRange(60.0 * 7.0, state.remainingTime, prevRemainingTime)) sound.playSpeech("seven minutes remaining");
            if (timeInRange(60.0 * 8.0, state.remainingTime, prevRemainingTime)) sound.playSpeech("eight minutes remaining");
            if (timeInRange(60.0 * 9.0, state.remainingTime, prevRemainingTime)) sound.playSpeech("nine minutes remaining");

            state.step();
        }

        public void render()
        {
            state.level.alphabet.updateTextures();
            if (state.decoderStale)
            {
                screenDecoder.renderDecoder(state, Constants.decoderWindowWidth, Constants.decoderWindowHeight);
                state.decoderStale = false;
            }
            screenPulse.renderPulse(state, Constants.pulseWindowWidth, Constants.pulseWindowHeight);
        }
    }
}
