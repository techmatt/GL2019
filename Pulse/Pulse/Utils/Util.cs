using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pulse
{
    static class Util
    {
        public static Random random = new Random();
        public static int randInt(int min, int maxExclusive)
        {
            return random.Next(min, maxExclusive);
        }

        public static double uniform(double min, double max)
        {
            return random.NextDouble() * (max - min) + min;
        }

        public static double linearMap(double x, double minValIn, double maxValIn, double minValOut, double maxValOut)
        {
            return ((x - minValIn) * (maxValOut - minValOut) / (maxValIn - minValIn) + minValOut);
        }
    }

    public static class CollectionExtension
    {
        public static T RandomElement<T>(this IList<T> list)
        {
            return list[Util.random.Next(list.Count)];
        }

        public static T RandomElement<T>(this T[] array)
        {
            return array[Util.random.Next(array.Length)];
        }

        public static List<T> Shuffle<T>(this List<T> list)
        {
            List<T> listCopy = new List<T>(list);
            int n = listCopy.Count;
            while (n > 1)
            {
                n--;
                int k = Util.random.Next(n + 1);
                T value = list[k];
                list[k] = list[n];
                list[n] = value;
            }
            return listCopy;
        }

    }

}
