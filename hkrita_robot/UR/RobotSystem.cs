using hkrita_robot.Network;
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
        private readonly static int NORMAL_PORT = 30003;
        private readonly static int STREAM_PORT = 30013;
            
        private readonly static int DASHBOARD_PORT = 29999;
        private readonly string mAddress;
        private Thread mThread;
        private Boolean mClosed;
        private NetworkClient mNetworkClient; 


        public RobotSystem(string ipAddress)
        {
            mAddress = ipAddress;
            mNetworkClient = new NetworkClient(mAddress, STREAM_PORT);
        }

        public bool Connect(bool withPoseReady)
        {
            try
            {
                bool success = InternalConnect(withPoseReady);
                if (!success) Close();
                return success;
            }
            catch(Exception e)
            {
                Close();
                throw e;
            }
        }

        public void Close()
        {
            CloseThread();
        }

        private bool InternalConnect(bool withPoseReady)
        {
            Close();
            bool success;           
            mThread = new Thread(() =>
            {
                Console.WriteLine("Robot Connection {0} is established: " , mAddress);
                success = mNetworkClient.Connect();
            });
       
            mClosed = false;


            mThread.IsBackground = true;
            mThread.Start();

            if (!withPoseReady) return true;



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
            catch (Exception ex) { }
            mThread = null;
        }

        
    }
}
