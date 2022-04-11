using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hkrita_robot.Network
{
    public class ControlData
    {
        public static string IpAddress;
        public static ushort port;
        public static int timeStep;


        // implement Maths Library 


        // Joint Space: 
        //  Orientation {J1, J2, J3, J4, J5, J6} radian 
        public static double[] J_Orientation = new double[6] { -1.6, -1.7, -2.2, -0.8, 1.59, -0.83 };

        // Cartesian Space: 
        //      Position {X, Y, Z} (mm)
        public static double[] C_Position = new double[3] { -0.11, -0.26, 0.15 };

        // Orientation Eular Angles (rad):
        public static double[] C_Orientation = new double[3] { 0.0, 3.11, 0.0 };

        // move Parameters: Velocity, Acceleration
        public static string velocity = "1.0";
        public static string acceleration = "1.0";


        public ControlData(string address, ushort portNumber, int timestep)
        {
            IpAddress = address;
            port = portNumber;
            timeStep = timestep;
        }

    }
}
