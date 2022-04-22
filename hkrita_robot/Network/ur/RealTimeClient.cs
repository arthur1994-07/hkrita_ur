using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace hkrita_robot.Network.ur
{
    public class RealTimeClient
    {
        private static readonly int K_STREAM_PORT = 30013;
        private NetworkClient mClient;
        private bool mReadStream = true;


        public RealTimeClient(string ipAddress, int port)
        {
            mClient = new NetworkClient(ipAddress, port);
        }

        public bool Connect() { return mClient.Connect(mReadStream); }



    }
}
