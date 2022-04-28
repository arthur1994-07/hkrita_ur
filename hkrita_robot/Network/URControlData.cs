using hkrita_robot.Maths;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hkrita_robot.Network
{
    public class URControlData
    {
        public const ushort Port = 30002;
        public static string IpAddress = "192.168.56.101";
        public static int timeStep = 8;
        public static Pose testTcpPose = new Pose(-0.020, 0.010, 0.178, 0, 0, 3.142);
        public static Pose testTcpPose2 = new Pose(-0.020, 0.010, 0, 0, 0, 3.142);

        public static double[] J_Orientation = new double[6] { -1.6, -1.7, -2.2, -0.8, 1.59, -0.03 };
        //  Cartesian Space:
        //      Position {X, Y, Z} (mm)
        public static double[] C_Position = new double[3] { -0.11, -0.26, 0.15 };
        //      Orientation {Euler Angles} (rad):
        public static double[] C_Orientation = new double[3] { 0.0, 3.11, 0.0 };
        // Move Parameters: Velocity, Acceleration
        public static string velocity = "1.0";
        public static string acceleration = "1.0";


        public static void ReadURData()
        {

        }
    }

}