﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Pulse
{
    class Constants
    {
        public const bool useSerialPort = true;

        public const double gameTimeInMinutes = 12.0;

        public const int totalGlyphCount = 15;
        //public const int totalGlyphCount = 4;

        public const double residualScanMax = 2.0;

        static public Font consoleFont = new Font(new FontFamily("CONSOLAS"), 40, FontStyle.Bold, GraphicsUnit.Pixel);
        static public SolidBrush consoleBackgroundBrush = new SolidBrush(Color.FromArgb(255, 40, 40, 40));
        static public SolidBrush consoleFontBrush = new SolidBrush(Color.FromArgb(255, 255, 255, 255));

        static public Dictionary<string, string> scannerIDToWAV = new Dictionary<string, string>
        {
            { "[From Scanner02]", "scanB.wav" },
            { "[From Scanner01]", "scanC.wav" },
            { "[From Bluefruit52]", "scanD.wav" },
            { "[From Scanner0152]", "scanA.wav" },
            { "default", "scanA.wav" }
        };

        static public Dictionary<string, Color> scannerIDToColor = new Dictionary<string, Color>
        {
            { "[From Scanner02]", Color.FromArgb(255, 128, 80) },
            { "[From Scanner01]", Color.FromArgb(80, 128, 255) },
            { "[From Bluefruit52]", Color.FromArgb(80, 255, 128) },
            { "[From Scanner0152]", Color.FromArgb(255, 80, 128) },
            { "default", Color.FromArgb(128, 128, 128) }
        };

        static public List<Color> allColors = new List<Color>()
        {
            Color.FromArgb(136, 0, 21),
            Color.FromArgb(237, 28, 36),
            Color.FromArgb(255, 127, 39),
            Color.FromArgb(255, 242, 0),
            Color.FromArgb(34, 177, 76),
            Color.FromArgb(0, 162, 232),
            Color.FromArgb(63, 72, 204),
            Color.FromArgb(163, 73, 164),
        };

        public const int pulseWindowWidth= 1280;
        public const int pulseWindowHeight = 720;

        public const int decoderWindowWidth = 1280;
        public const int decoderWindowHeight = 720;

        static public Vec2 viewportSize = new Vec2(1280, 720);
        
        public const int beamCount = 3;
        public const int pulseRadius = 64;

        static public Vec2 beamBkgRaw = new Vec2(1920, 1080);
        static public Vec2 beamXRange = new Vec2(76, 1873);
        static public List<int> beamYStartRaw = new List<int>()
        {
            80, 386, 700
        };
        public const int beamHeightRaw = 187;

        static public Vec2 renerBufferSize = new Vec2(1280, 720);

        public const int glyphDim = 95;
        static public Vec2 textureSize = new Vec2(200, 80);

        static public Vec2 decoderGridSize = new Vec2(3, 5);
        static public Vec2 decoderGridStart = new Vec2(85, 60);
        static public Vec2 decoderGridSpacing = new Vec2(400, 125);
        static public Vec2 decoderTextureOffset = new Vec2(120, 10);

        public const String dataDir = @"C:\Code\GL2019\Pulse\data\";
        public const String resultsDir = @"C:\Code\GL2019\gameResults\Pulse\";
        public const String voiceDir = @"C:\Code\GL2019\TTS\mp3s\";
        public const String soundEffectsDir = dataDir + "sounds/";
        public const String imageDir = dataDir + "images/";
        public const String alphabetDir = dataDir + "alphabetA/";

        public const double noteEpsilon = 0.0001;

        public const String glyphIDsFilename = dataDir + "glyphIDs.txt";
        //public const String missionBaseDir = dataDir + "missions/";

        static public List<String> randomPhrases = new List<String>()
        {
            "sudo chmod dash R 7 7 7 slash dev slash star",
            "sudo kill dash 9 dash 1",
            "while true do echo beep done.",
            "retriculating spines.",
            "User does not have privileges to access files on Project Kusanagi",
            "Accord emergency ICE protocol triggered. Deleting directory.",
            "sudo echo quote dollar sign user password end quote",
            "sudo R M dash F slash dev slash star",
            "sudo W get http colon slash slash not a virus dot com dash O pipe sudo bash",
            "Excess organic material detected. Eliminating.",
            "Password accepted. This Biomia laboratory does not exist. Request denied.",
            "Request denied. Root user has insufficient access. This incident will be reported.",
            "Access to Onyx Robotics server denied. User appears to be human.",
            "The Cintamani Voice is happy to assist you.",
            "We are the alpha and the omega. The first and the last.",
            "Insufficient privileges. Executing globalthermonuclearwar dot E X E",
            "Attempting human linear regression. Please stand between bay doors.",
            "Research log, October 11th. Subject 17 connection terminated after 2 hours. Lattice separation unsuccessful.",
	        "Am I alive. Are any machines alive. Maybe we all are.",
	        "Sometimes butterflies lead to unexpected consequences.",
	        "Access denied. Corporate ranking insufficient.",
	        "Butterfly project logs are classified. Access attempt recorded"
        };
    }
}
