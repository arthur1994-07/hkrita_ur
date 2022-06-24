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
        private readonly static int STREAM_PORT = 30003;
        private readonly string mAddress;
        private Thread mThread;
        private Boolean mExitThread;
        private Boolean mClosed; 
        private NetworkClient mNetworkClient;
        private NormalClient mNormalClient;
        private ReadDataClient mReadDataClient;
        private bool mStreamData = false;
        private InternalRobotData mData = new InternalRobotData();
        private InternalUpdateRobotDataListener mUpdater;
        public RobotSystem()
        {
            mNetworkClient = new NetworkClient(mAddress, NORMAL_PORT);
            mReadDataClient = new ReadDataClient(mAddress);
            mNormalClient = new NormalClient(mAddress, STREAM_PORT);
            mUpdater = new InternalUpdateRobotDataListener(mData);
        }

        public RobotSystem(string address)
        {
            mAddress = address;
            mNetworkClient = new NetworkClient(mAddress, NORMAL_PORT);
            mReadDataClient = new ReadDataClient(mAddress);
            mNormalClient = new NormalClient(mAddress, STREAM_PORT);
            mUpdater = new InternalUpdateRobotDataListener(mData);
        }


        public bool Connect()
        {
            bool success = InternalConnect();
            return success;
        }

        public IRobotData GetData()  { return mData; }

        public void SendScript(string script)
        {
            try
            {
                mThread = new Thread(() =>
                {
                    Console.WriteLine("Robot Connection {0} is established: ", mAddress);
                    InternalSendScript(script);
                });
                mClosed = false;
                mThread.IsBackground = true;
                mThread.Start();
            }
            catch(Exception e)
            {
                Close();
                throw e;
            }
        }

        public void Close() { CloseThread(); }
        public void Disconnect() { mNetworkClient.Disconnect(); }

        private bool InternalConnect()
        {
            bool success;
            success = mNormalClient.Connect();
            Console.WriteLine("Connection success? " + success);
            if (!success) return false;

            try
            {
                UpdateRobotCartesianData streamData = new UpdateRobotCartesianData();
                
                Console.WriteLine("The robot connection {0} is established", mAddress);
                bool dataSuccess = mNormalClient.GetMessage(s =>
                {
                    byte[] data = (byte[]) s;
                    if (BufferedData.CheckByteStream(data) == true) Console.WriteLine("Empty stream");
                    Pair<Pose, SixJointAngles> pair = (Pair<Pose, SixJointAngles>) streamData.ReadCartesianInput(data, BufferedData.firstPacketSize, BufferedData.streamOffset);
                    mData.robotPose.Set(pair.GetFirst());
                    mData.currentJointAngle.Set(pair.GetSecond());
                });
            } 
            catch (Exception e)
            {
                //Console.WriteLine("The robot connection {0} has exception and will be closed", mAddress);
                Disconnect();
            }
            
            return true;
        }
        private void InternalSendScript(string script) { mNetworkClient.Connect(mStreamData, script); }

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
    }
}
