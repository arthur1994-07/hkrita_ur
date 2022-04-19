using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hkrita_robot.Network
{
    public static class URControlData
    {
        public const ushort Port = 30003;
        public static string IpAddress = "192.168.56.101";
        public static int timeStep = 8;


        public static double[] J_Orientation = new double[6] { -1.6, -1.7, -2.2, -0.8, 1.59, -0.03 };
        //  Cartesian Space:
        //      Position {X, Y, Z} (mm)
        public static double[] C_Position = new double[3] { -0.11, -0.26, 0.15 };
        //      Orientation {Euler Angles} (rad):
        public static double[] C_Orientation = new double[3] { 0.0, 3.11, 0.0 };
        // Move Parameters: Velocity, Acceleration
        public static string velocity = "1.0";
        public static string acceleration = "1.0";
    }
}