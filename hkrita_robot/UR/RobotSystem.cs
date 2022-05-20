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
            Task newTask = Task.Factory.StartNew(() =>
            {
                Pair<Pose, SixJointAngles> data = mReadDataClient.ReadStream();
                //mReadDataClient.Close();
                Pose pose = data.GetFirst();
                SixJointAngles joints = data.GetSecond();
                mData.robotPose.Set(pose);
                mData.currentJointAngle.Set(joints);
                Console.WriteLine("");
            });
            newTask.Wait();
            //Disconnect();
        }

        public void Close()
        {
            CloseThread();
        }


        private void InternalSendScript(string script)
        {
            // start new thread 
            mThread = new Thread(() =>
            {
                Console.WriteLine("Robot Connection {0} is established: ", mAddress);
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
                mClosed = true;
                Console.WriteLine("Thread status: " + mThread.IsAlive);
            }
            catch (Exception ex) { }
            mThread = null;
        }


        public void Disconnect()
        {
            mNetworkClient.Disconnect();
        }


        
    }
}
