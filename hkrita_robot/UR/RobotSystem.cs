using hkrita_robot.Container;
using hkrita_robot.Maths;
using hkrita_robot.Network;
using hkrita_robot.Network.ur;
using hkrita_robot.Network.ur.internalData;
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
        private readonly static int NORMAL_PORT = 30002;
            
        private readonly string mAddress;
        private Thread mThread;
        private Boolean mExitThread;
        private Boolean mClosed; 
        private NetworkClient mNetworkClient;
        private ReadDataClient mReadDataClient;
        private bool mStreamData = false;
        private InternalRobotData mData = new InternalRobotData();

        public RobotSystem(string ipAddress)
        {
            mAddress = ipAddress;
            mNetworkClient = new NetworkClient(mAddress, NORMAL_PORT);
            mReadDataClient = new ReadDataClient(mAddress);
        }


        public IRobotData GetData()  { return mData; }

        public void SendScript(string script)
        {
            try
            {
                InternalSendScript(script);
            }
            catch(Exception e)
            {
                Close();
                throw e;
            }
        }
        public void ReadData()
        {
            try
            {
                Pose pose = mReadDataClient.ReadStream();
                mData.robotPose.Set(pose);
            }
            catch (Exception e)
            {
                Close();
                throw e;
            }
        }

        public void Close()
        {
            CloseThread();
        }


        private void InternalSendScript(string script)
        {
            mThread = new Thread(() =>
            {
                Console.WriteLine("Robot Connection {0} is established: " , mAddress);
                mNetworkClient.Connect(mStreamData, script);
            });
            mClosed = false;
            mThread.IsBackground = true;
            mThread.Start();
        }

        private void CloseThread()
        {
            if (mThread == null) return;
            try
            {
                mThread.Interrupt();
                mThread.Join();
                Console.WriteLine("Connection status: " + mThread.IsAlive);
                mClosed = true;
            }
            catch (Exception ex) { }
            mThread = null;
        }

        private void Stop()
        {
            mExitThread = true;
            Console.WriteLine(mThread.IsAlive);
            if (mThread.IsAlive == true)
            {
                Thread.Sleep(100);
            }
        }

        public void Destroy()
        {
            Stop();
            mNetworkClient.Close();
        }


        
    }
}
