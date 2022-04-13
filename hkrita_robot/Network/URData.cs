using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace hkrita_robot.Network
{
    public class URData
    {
        public static string IpAddress = "192.168.56.101";
        public static int timeStep = 8;

        // Thread
        private Thread mThread;
        private bool mExitThread = false;
        // TCP/IP Communication 
        private TcpClient mClient = new TcpClient();
        private NetworkStream mStream = null;
    }

}
