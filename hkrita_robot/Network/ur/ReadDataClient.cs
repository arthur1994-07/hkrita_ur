using hkrita_robot.Container;
using hkrita_robot.Maths;
using hkrita_robot.Network.ur.internalData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace hkrita_robot.Network.ur
{
    public class ReadDataClient 
    {
        private static int K_STREAM_PORT = 30003;
        private Thread mThread;
        private InternalRobotData mData = new InternalRobotData();
        private bool mExitThread = false;
        private NetworkClient mClient;
        private bool mReadStream = true;
        public ReadDataClient(string ipAddress)
        {
            mClient = new NetworkClient(ipAddress, K_STREAM_PORT);
        }

        public Pose ReadStream()
        {
            try
            {
                return InternalReadStream();
            }
            catch (Exception ex)
            {
                Close();
                throw ex;
            }
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
            mClient.Close();
        }       
        
        private Pose InternalReadStream()
        {
            Pair<Pose, SixJointAngles> pair = (Pair<Pose, SixJointAngles>)mClient.Connect(mReadStream, null);
            mData.robotPose.Set(pair.GetFirst());
            return pair.GetFirst(); 

        }
    } 
}
