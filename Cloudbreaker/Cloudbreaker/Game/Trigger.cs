using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cloudbreaker.Game
{
    public class Trigger
    {
        public Trigger(PlayerName _player, TriggerType _type)
        {
            player = _player;
            type = _type;
        }
        public static Trigger makeRandom(PlayerName player)
        {
            Trigger result = new Trigger(player, TriggerType.Joystick);
            int choice = Util.randExclusive(0, 4);
            if (choice == 0) result.button = JoystickButtonType.A;
            if (choice == 1) result.button = JoystickButtonType.B;
            if (choice == 2) result.button = JoystickButtonType.X;
            if (choice == 3) result.button = JoystickButtonType.Y;
            return result;
        }

        public PlayerName player;
        public TriggerType type;

        public JoystickButtonType button;

        public bool decoded;
    }
}
