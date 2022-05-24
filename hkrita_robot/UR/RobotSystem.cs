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

        public RobotSystem(string ipAddress)
        {
            mAddress = ipAddress;
            mNetworkClient = new NetworkClient(mAddress, NORMAL_PORT);
            mReadDataClient = new ReadDataClient(mAddress);
            mNormalClient = new NormalClient(mAddress, STREAM_PORT);
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

        private bool InternalConnect()
        {
            bool success;
            success = mNormalClient.Connect();
            if (!success) return false;
            mThread = new Thread(() =>
            {
                try
                {
                    Console.WriteLine("The robot connection {0} is established", mAddress);
                    bool dataSuccess = mNormalClient.GetMessage(s =>
                    {
                        byte[] data = (byte[]) s;
                        Console.WriteLine("Callback value:" + data.Length);
                        // TODO : get cartesian data in call back
                        Pair<Pose, SixJointAngles> pair = (Pair<Pose, SixJointAngles>)UpdateRobotCartesianData.ReadCartesianInput(data, BufferedData.firstPacketSize, BufferedData.streamOffset);
                    });
                } 
                catch (Exception e)
                {
                    Console.WriteLine("The robot connection {0} has exception and will be closed", mAddress);
                }
            });
            mThread.Start();

            return true;
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
