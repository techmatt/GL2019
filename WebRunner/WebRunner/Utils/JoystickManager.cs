using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using SharpDX.DirectInput;

namespace WebRunner
{
    // code reference: 
    // https://github.com/sharpdx/SharpDX-Samples/blob/master/Desktop/DirectInput/JoystickApp/Program.cs

    class RunnerJoystickState
    {
        public RunnerJoystickState()
        {

        }
        public Vec2 padA = new Vec2(0.0, 0.0);
        public Vec2 padB = new Vec2(0.0, 0.0);

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
            // Initialize DirectInput
            var directInput = new DirectInput();

            // Find a Joystick Guid
            //var joystickGuid = Guid.Empty;
            var joystickGuids = new List<Guid>();
            //Guid.Empty;

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
                    /*Console.WriteLine("offset:" + state.Offset.ToString() + " " +
                                      "roffset:" + state.RawOffset.ToString() + " " +
                                      //"seq:" + state.Sequence.ToString() + " " +
                                      "val:" + Convert.ToString(state.Value, 2).PadLeft(32,'0'));*/
                    /*
                     *  offset:Y roffset:4 seq:1 val:32766
                        offset:X roffset:0 seq:1 val:34303
                        offset:Y roffset:4 seq:12 val:32766
                        */
                }
                JoystickState DXstate = DXJoystick.GetCurrentState();
                runnerState.padA.x = padClamp((DXstate.X - 32767) / 32767.0);
                runnerState.padA.y = padClamp((DXstate.Y - 32767) / 32767.0);
                runnerState.padB.x = padClamp((DXstate.RotationX - 32767) / 32767.0);
                runnerState.padB.y = padClamp((DXstate.RotationY - 32767) / 32767.0);
                Console.WriteLine("j" + joystickIndex.ToString() + ": " + runnerState.ToString());
            }
        }
    }
}
