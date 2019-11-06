using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace WebRunner
{
    class Constants
    {
        // music
        // https://www.youtube.com/watch?v=RlVTb3g-COY
        // https://www.youtube.com/watch?v=B9wGMgW-no4

        public const bool useWebcam = false;
        public const bool useGameEditor = false;
        public const int webcamCaptureIndex = 1;

        public const int medPackRadius = 100;
        public const int kusanagiRadius = 150;
        public const int dysonRadius = 100;
        public const double dysonDamageRate = miasmaGrowthRate * 7.0;

        public const double misamaDamage = 0.015;
        public const double miasmaGrowth = 0.5;
        public const int maxMiasma = useGameEditor ? 2 : 40;

        public const double miasmaGrowthRate = 0.1;

        public const double laserGunDamage = 0.12;
        public const double kusanagiGunDamage = 0.25;
        public const double laserTurretDamage = 0.1;
        public const double structureDisableTime = 15.0;
        public const double runnerMaxHealth = 1.0;

        public const double structureHealRate = 0.02;

        public const int runnerMirrorRadius = 75;

        static public Font consoleFont = new Font(new FontFamily("CONSOLAS"), 28, FontStyle.Bold, GraphicsUnit.Pixel);
        static public SolidBrush consoleBackgroundBrush = new SolidBrush(Color.FromArgb(255, 40, 40, 40));
        static public SolidBrush consoleFontBrush = new SolidBrush(Color.FromArgb(255, 37, 192, 84));

        public const int renderWidthWindowed = 1280;
        public const int renderHeightWindowed = 720;

        public const int renderWidthFull = 1280;
        public const int renderHeightFull = 720;

        public const bool useFullscreen = true;

        static public Vec2 viewportSize = new Vec2(1280, 720);
        public const double shoeSpeedMultiplier = 1.3;
        

        public const int backgroundAlpha = 200;

        public const int gridSize = 40;

        public const double runMaxDistA = 150.0;
        public const double runMaxDistB = 400.0;
        public const double runSpeed = 8.0;

        public const double runnerRadius = 20.0;

        public const int maxBeamBounces = 10;

        public const String voiceDir = @"C:\Code\GL2019\TTS\mp3s\";
        public const String dataDir = @"C:\Code\GL2019\WebRunner\gameData\";
        public const String soundEffectsDir = dataDir + "sounds/";
        public const String imageOriginalDir = dataDir + "imagesOriginal/";
        public const String tilesetDir = imageOriginalDir + "tilesets/";
        public const String missionBaseDir = dataDir + "missions/";
        public const String resultsDir = @"C:\Code\GL2019\gameResults\ICEBreaker\";

        static public List<String> randomPhrases = new List<String>()
        {
            "this facility is the property of Onyx Robotics. Please wait for security teams to arrive.",
            "You have entered a restricted area.",
            "Biomia Labs cannot be held responsible for any fatalities that may occur in this facility.",
            "You are in violation of World Bank Article 503 C 1 A 7.",
            "Access to Onyx Robotics server denied. User appears to be human.",
            "Access denied. Corporate ranking insufficient.",
            "Warning. security measures in this location have not been approved for consumer use. Side effects may include mild headaches and violent discorporation.",
            "Intruders detected. Please wait while assassination squad is dispatched.",
            "Miasma contamination detected at unsafe levels.  Please contact your nearest emergency response team.",
            "Visiting hours are no longer in effect. All visitors have been automatically promoted to intruder status.",
            "Congratulations! It has been ERROR: INTEGER UNDERFLOW seconds since the last fatal incident in this facility."
        };
    }
}
