using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cloudbreaker
{
    public static class Util
    {
        public static Random rnd = new Random();

        public static int randExclusive(int minimum, int maximum)
        {
            return rnd.Next(minimum, maximum);
        }

        public static double uniform(double minimum, double maximum)
        {
            return rnd.NextDouble() * (maximum - minimum) + minimum;
        }
    }
}
