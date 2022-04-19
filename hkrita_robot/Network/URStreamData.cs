using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hkrita_robot.Network
{
    // Real time interface 
    public static class URStreamData 
    {
        public const ushort Port = 30013;
        public static string IpAddress = "192.168.56.101";
        public static int timeStep = 8;

        public static double[] J_Orientation = new double[6];
        public static double[] C_Position = new double[3];
        public static double[] C_Orientation = new double[3];
    }
}
