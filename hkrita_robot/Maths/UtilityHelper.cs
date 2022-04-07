using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hkrita_robot.Maths
{
    public class UtilityHelper
    {
        public static double DOUBLE_EPSILON = 1e-7;

        public static double Square(double value) { return value * value; }

        public static int Square(int value) { return value * value; }
        public static double Sign(double x)
        {
            return x < 0 ? -1 : 1;
        }
    }
}
