using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cloudbreaker
{
    public enum PlayerName
    {
        Hacker,
        Fighter,
        Rogue
    }
    public enum ThreatType
    {
        Music,  // DDR-style buttons
        ButtonLinear,   // press buttons in order
        PasswordHunt,   // enter a set of themed passwords
        LogicProbe,     // 
        SymbolScramble, //
    }

    public enum TriggerType
    {
        Joystick,
        World,
        Command,
    }

    public enum JoystickButtonType
    {
        A,
        B,
        X,
        Y,
        Start,
        Back,
        L1,
        L2,
        R1,
        R2,
    }

    public class Constants
    {
        public static double ThreatBarTotalTime = 45.0;
    }

}
