using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace hkrita_robot.Network.ur
{
    public class RealTimeSystem 
    {
        private static int K_STREAM_PORT = 30013;
        private Thread mThread;
        private RealTimeClient mClient;
        private bool mClosed;
        private RealTimeRobotData mData = new RealTimeRobotData();

        public RealTimeSystem(string ipAddress)
        {
            mClient = new RealTimeClient(ipAddress, K_STREAM_PORT);
        }

        public void Connect()
        {
            bool success = InternalConnect(); 
        }

        public void Close()
        {
            CloseThread();
        }

        private bool InternalConnect()
        {
            Close();
            bool success;
            mThread = new Thread(() =>
            {
                Console.WriteLine("Stream Connection via {0} is established: ", K_STREAM_PORT);
                success = mClient.Connect();
            });

            mClosed = false;

            mThread.IsBackground = true;
            mThread.Start();

            return true;
        }

        private void CloseThread()
        {
            if (mThread == null) return;
            try
            {
                mThread.Interrupt();
                mThread.Join(); 
            }
            catch (Exception e)
            {
                mThread = null;
            }
        }
        
    } 
}
