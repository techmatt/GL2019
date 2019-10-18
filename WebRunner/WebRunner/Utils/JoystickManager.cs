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
    
    class JoystickManager
    {
        List<Joystick> joysticks = new List<Joystick>();

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
                Console.ReadKey();
                Environment.Exit(1);
            }

            // Instantiate the joystick
            foreach (var jGuid in joystickGuids)
            {
                Console.WriteLine("Found Joystick/Gamepad with GUID: {0}", joystickGuids[0]);
                Joystick joystick = new Joystick(directInput, jGuid);
                joystick.Properties.BufferSize = 128;
                joystick.Acquire();
                joysticks.Add(joystick);
            }

            // Query all suported ForceFeedback effects
            //var allEffects = joystick.GetEffects();
            //foreach (var effectInfo in allEffects)
            //    Console.WriteLine("Effect available {0}", effectInfo.Name);
        }

        public void poll()
        {
            joysticks[0].Poll();
            var datas = joysticks[0].GetBufferedData();
            foreach (var state in datas)
            {
                Console.WriteLine("offset:" + state.Offset.ToString() + " " +
                                  "roffset:" + state.RawOffset.ToString() + " " +
                                  "seq:" + state.Sequence.ToString() + " " +
                                  "val:" + state.Value.ToString());
            }
        }
    }
}
