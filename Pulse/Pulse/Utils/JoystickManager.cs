using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using SharpDX.DirectInput;

namespace Pulse
{
    // code reference: 
    // https://github.com/sharpdx/SharpDX-Samples/blob/master/Desktop/DirectInput/JoystickApp/Program.cs

    enum GamepadButton
    {
        X,
        Y,
        A,
        B,
        Invalid
    }

    class RunnerJoystickState
    {
        public RunnerJoystickState()
        {
            buttonStates[GamepadButton.X] = false;
            buttonStates[GamepadButton.Y] = false;
            buttonStates[GamepadButton.A] = false;
            buttonStates[GamepadButton.B] = false;
        }
        public Vec2 padA = new Vec2(0.0, 0.0);
        public Vec2 padB = new Vec2(0.0, 0.0);
        public Dictionary<GamepadButton, bool> buttonStates = new Dictionary<GamepadButton, bool>();
        public List<GamepadButton> buttonsToProcess = new List<GamepadButton>();

        public override string ToString()
        {
            return "padA:" + padA.ToString() + " padB:" + padB.ToString();
        }
    }

    class JoystickManager
    {
        const int debugMaxJoystickCount = 2;

        static double padClamp(double x)
        {
            if (x >= -0.2 && x < 0.2) return 0.0;
            return x;
        }

        public List<RunnerJoystickState> joysticks = new List<RunnerJoystickState>();
        List<Joystick> DXjoysticks = new List<Joystick>();
        
        public JoystickManager()
        {
            var directInput = new DirectInput();

            //var joystickGuid = Guid.Empty;
            var joystickGuids = new List<Guid>();

            foreach (var deviceInstance in directInput.GetDevices(DeviceType.Gamepad, DeviceEnumerationFlags.AllDevices))
                joystickGuids.Add(deviceInstance.InstanceGuid);

            // If Gamepad not found, look for a Joystick
            //if (joystickGuid == Guid.Empty)
            foreach (var deviceInstance in directInput.GetDevices(DeviceType.Joystick, DeviceEnumerationFlags.AllDevices))
                joystickGuids.Add(deviceInstance.InstanceGuid);

            // If Joystick not found, throws an error
            if (joystickGuids.Count == 0)
            {
                Console.WriteLine("No joystick/Gamepad found.");
                Environment.Exit(1);
            }

            // Instantiate the joystick
            foreach (var jGuid in joystickGuids)
            {
                Console.WriteLine("Found Joystick/Gamepad with GUID: {0}", joystickGuids[0]);
                Joystick DXjoystick = new Joystick(directInput, jGuid);
                DXjoystick.Properties.BufferSize = 128;
                DXjoystick.Acquire();

                if (DXjoysticks.Count < debugMaxJoystickCount)
                {
                    DXjoysticks.Add(DXjoystick);
                    joysticks.Add(new RunnerJoystickState());
                }
            }

            // Query all suported ForceFeedback effects
            //var allEffects = joystick.GetEffects();
            //foreach (var effectInfo in allEffects)
            //    Console.WriteLine("Effect available {0}", effectInfo.Name);
        }

        public void poll()
        {
            for (int joystickIndex = 0; joystickIndex < DXjoysticks.Count; joystickIndex++)
            {
                Joystick DXJoystick = DXjoysticks[joystickIndex];
                RunnerJoystickState runnerState = joysticks[joystickIndex];
                DXJoystick.Poll();
                var commands = DXJoystick.GetBufferedData();
                foreach (var command in commands)
                {
                    /*Console.WriteLine("offset:" + command.Offset.ToString() + " " +
                                      "roffset:" + command.RawOffset.ToString() + " " +
                                      //"seq:" + state.Sequence.ToString() + " " +
                                      "val:" + Convert.ToString(command.Value, 2).PadLeft(32,'0'));*/
                    GamepadButton button = GamepadButton.Invalid;
                    if (command.Offset == JoystickOffset.Buttons0) button = GamepadButton.A;
                    if (command.Offset == JoystickOffset.Buttons1) button = GamepadButton.B;
                    if (command.Offset == JoystickOffset.Buttons2) button = GamepadButton.X;
                    if (command.Offset == JoystickOffset.Buttons3) button = GamepadButton.Y;

                    if (button != GamepadButton.Invalid)
                    {
                        if(command.Value == 0)
                        {
                            runnerState.buttonStates[button] = false;
                        }
                        else
                        {
                            runnerState.buttonStates[button] = true;
                            runnerState.buttonsToProcess.Add(button);
                        }
                    }
                    /*
                     *  offset:Y roffset:4 seq:1 val:32766
                        offset:X roffset:0 seq:1 val:34303
                        offset:Y roffset:4 seq:12 val:32766
                        */
                }
                JoystickState DXstate = DXJoystick.GetCurrentState();
                //runnerState.padA.x = padClamp((DXstate.X - 32767) / 32767.0);
                //runnerState.padA.y = padClamp((DXstate.Y - 32767) / 32767.0);
                //runnerState.padB.x = padClamp((DXstate.RotationX - 32767) / 32767.0);
                //runnerState.padB.y = padClamp((DXstate.RotationY - 32767) / 32767.0);
                //Console.WriteLine("j" + joystickIndex.ToString() + ": " + runnerState.ToString());
            }
        }
    }
}
