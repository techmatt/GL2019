using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Media;
using System.IO;
using System.Diagnostics;

namespace Pulse
{
    class SoundManager
    {
        Dictionary<String, SoundPlayer> sounds = new Dictionary<String, SoundPlayer>();
        public SoundManager()
        {
            //SoundPlayer simpleSound = new SoundPlayer(strAudioFilePath);
            //simpleSound.Play();
        }
        public void playSpeech(string speech)
        {
            if (!sounds.ContainsKey(speech))
            {
                string filename = Constants.soundDir + speech + ".wav";
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

                    Debug.Assert(File.Exists(filename));
                }
                SoundPlayer newSound = new SoundPlayer(filename);
                sounds[speech] = newSound;
            }
            SoundPlayer sound = sounds[speech];
            sound.Play();
        }
    }
}
