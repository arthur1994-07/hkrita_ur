using hkrita_robot.Container;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace hkrita_robot.Network.ur
{
    public class RealTimeSystem 
    {
        private static int K_STREAM_PORT = 30004;
        private Thread mThread;
        private RealTimeRobotData mData = new RealTimeRobotData();
        private bool mExitThread = false;
        private NetworkClient mClient;
        private bool mReadStream = true;
        public RealTimeSystem(string ipAddress)
        {
            mClient = new NetworkClient(ipAddress, K_STREAM_PORT);
        }

        public void Connect()
        {
            InternalConnect();
        }

        public void Close()
        {
            InternalCloseThread();
        }

        public void Stop()
        {
            mExitThread = true;
            if (mThread.IsAlive == true)
            {
                Thread.Sleep(100);
            }
        }

        public void InternalCloseThread()
        {
            Stop();
            mClient.CloseThread();
        }       
        
        private void InternalConnect()
        {
            bool success;
            mThread = new Thread(() =>
            {
                if (mThread.IsAlive) Console.WriteLine("Stream Connection via {0} is established: ", K_STREAM_PORT);
                mClient.Connect(mReadStream);
            });

            mThread.IsBackground = true;
            mThread.Start();
        }
    } 
}
