using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hkrita_robot.Network
{
    // primary secondary interface 
    public class URControlData
    {
        public const ushort Port = 30003;

        public static double[] J_Orientation = new double[6];
        public static double[] C_Position = new double[3];
        public static double[] C_Orientation = new double[3];

        public static string velocity = "1,0";
        public static string acceleration = "1.0";

    }
}
