using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Media;
using System.IO;
using System.Diagnostics;

namespace WebRunner
{
    class SoundManager
    {
        Dictionary<String, SoundPlayer> sounds = new Dictionary<String, SoundPlayer>();
        public SoundManager()
        {
            //SoundPlayer simpleSound = new SoundPlayer(strAudioFilePath);
            //simpleSound.Play();
        }
        public void playSpeech(string speechRaw, bool updateLastSpeechPlayed = true)
        {
            try
            {
                string speech = speechRaw;
                if (speechRaw[speechRaw.Length - 1] == '.')
                    speech = speechRaw.Remove(speechRaw.Length - 1, 1);

                if (!sounds.ContainsKey(speech))
                {
                    string filename = Constants.voiceDir + speech + ".wav";
                    if (!File.Exists(filename))
                    {
                        string cmdText = "\"C:/code/GL2019/TTS/ttsGoogle.py\" \"" + speech + "\" \"C:/code/GL2019/TTS/mp3s/\"";
                        Console.WriteLine("creating speech: " + cmdText);
                        //System.Diagnostics.Process.Start("CMD.exe", cmdText);

                        var proc = new Process
                        {
                            StartInfo = new ProcessStartInfo
                            {
                                FileName = "python.exe",
                                Arguments = cmdText,
                                UseShellExecute = false,
                                RedirectStandardOutput = true,
                                CreateNoWindow = true
                            }
                        };

                        proc.Start();
                        while (!proc.StandardOutput.EndOfStream)
                        {
                            string line = proc.StandardOutput.ReadLine();
                            Console.WriteLine(line);
                        }

                        //Debug.Assert(File.Exists(filename));
                    }
                    if (File.Exists(filename))
                    {
                        SoundPlayer newSound = new SoundPlayer(filename);
                        sounds[speech] = newSound;
                    }
                }
                if (!sounds.ContainsKey(speech))
                    return;

                SoundPlayer sound = sounds[speech];
                Console.WriteLine("playing speech: " + speech);
                sound.Play();
                if (updateLastSpeechPlayed)
                    lastSpeechPlayed = DateTime.Now;
            }
            catch(Exception ex)
            {
                int a = 5;
            }
        }

        public void playWAVFile(string WAVFilename)
        {
            if (!sounds.ContainsKey(WAVFilename))
            {
                string path = Constants.soundEffectsDir + WAVFilename;
                Debug.Assert(File.Exists(path));
                SoundPlayer newSound = new SoundPlayer(path);
                sounds[WAVFilename] = newSound;
            }
            SoundPlayer sound = sounds[WAVFilename];
            Console.WriteLine("playing sound: " + WAVFilename);
            sound.Play();
        }

        public DateTime lastSpeechPlayed;
    }
}
