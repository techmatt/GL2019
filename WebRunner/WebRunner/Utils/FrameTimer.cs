using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace WebRunner
{
    class FrameTimer
    {
        public FrameTimer()
        {
            stopwatch.Restart();
            framesPerSecond = 30.0;
            secondsPerFrame = 1.0 / framesPerSecond;
        }
        
        public void frame()
        {
            double delta = stopwatch.Elapsed.TotalSeconds;
            stopwatch.Restart();
            secondsPerFrame = secondsPerFrame * inertia + delta * (1.0 - inertia);
            framesPerSecond = 1.0 / secondsPerFrame;
        }

        Stopwatch stopwatch = new Stopwatch();
        const double inertia = 0.9;
        public double secondsPerFrame, framesPerSecond;
    }
}
