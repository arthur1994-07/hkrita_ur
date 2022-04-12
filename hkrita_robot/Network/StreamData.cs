using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hkrita_robot.Network
{
    public class StreamData
    {
        public static string IpAddress; // 192.168.56.101
        public static ushort port; // 30002
        public static int communicationSpeed;
        public static int timeStep;
        public static bool saveData;

        public static double[] J_Orientation = new double[6];
        public static double[] C_Position = new double[3];
        public static double[] C_Orientation = new double[3];


        // Joint space, Orientation { J1, J2, J3, J4, J5, J6 }

        public StreamData(string address, ushort portNumber, int timestep)
        {
            IpAddress = address;
            port = portNumber;
            timeStep = timestep;
        }
    }
}