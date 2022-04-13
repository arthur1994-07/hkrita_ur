using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hkrita_robot.Network
{
    // Real time interface 
    public class URStreamData
    {
        public static ushort Port = 30013;

        public static double[] J_Orientation = new double[6];
        public static double[] C_Position = new double[3];
        public static double[] C_Orientation = new double[3];
    }
}
