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

        public const bool useWebcam = true;
        public const bool useGameEditor = false;
        public const int webcamCaptureIndex = 1;

        public const int medPackRadius = 100;
        public const int kusanagiRadius = 150;
        public const int dysonRadius = 100;
        public const double dysonDamageRate = miasmaGrowthRate * 7.0;

        public const double misamaDamage = 0.01;
        public const double miasmaGrowth = 0.5;
        public const int maxMiasma = 40;

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
        public const String missionBaseDir = dataDir + "missions/";

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
