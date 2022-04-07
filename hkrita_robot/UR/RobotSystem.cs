using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace hkrita_robot.UR
{
    public class RobotSystem
    {
        private readonly static int MODBUS_PORT = 502;
        private readonly static int NORMAL_PORT = 30002;
        private readonly static int DASHBOARD_PORT = 29999;
        private readonly string mAddress;
        private Thread mThread;
        private Boolean mClosed;

        public RobotSystem(string ipAddress)
        {
            mAddress = ipAddress;

        }
    }
}
