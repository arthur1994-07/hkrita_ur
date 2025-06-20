using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hkrita_robot.Network.ur
{
    public class RealTimeRobotData
    {
        public double[] J_Orientation;
        public double[] C_Position;
        public double[] C_Orientation;

        public RealTimeRobotData()
        {
            J_Orientation = new double[6];
            C_Position = new double[3];
            C_Orientation = new double[3];
        }
    }
}
