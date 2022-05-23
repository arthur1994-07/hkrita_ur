using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using hkrita_robot.Container;
using hkrita_robot.Maths;
using hkrita_robot.Network;
using hkrita_robot.Network.ur;
using hkrita_robot.Network.ur.internalData;

namespace hkrita_robot.Network
{
    public class URStream
    {
        // Thread
        private Thread mThread;
        private bool mExitThread = false;
        // TCP/IP Communication 
        private TcpClient mClient = new TcpClient();
        private NetworkStream mStream = null;
        // Packet Buffer
        private byte[] mBuffer = new byte[4096];
        // Size of first packet in bytes (integer)
        private const byte mFirstPacketSize = 4;
        // Size of other packets in bytes (double)
        private const byte mOffset = 8;

        // Total message length in bytes
        private const UInt32 mTotalMsg = 3288596480;

        public void InternalConnect()
        {
            try
            {
                if (mClient.Connected == false)
                {
                    mClient.Connect(URStreamData.IpAddress, URStreamData.Port);
                    Console.WriteLine("Connected: " + mClient.Connected);
                }
                mStream = mClient.GetStream();
                var t = new Stopwatch();

                while (mExitThread == false)
                {
                    if (mStream.Read(mBuffer, 0, mBuffer.Length) != 0)
                    {
                        t.Start();
                        Array.Reverse(mBuffer);
                        UpdateRobotCartesianData.ReadCartesianInput(mBuffer, mFirstPacketSize, mOffset);

                        t.Stop();
                        if (t.ElapsedMilliseconds < URStreamData.timeStep)
                        {
                            Thread.Sleep(URStreamData.timeStep - (int)t.ElapsedMilliseconds);
                        }
                        t.Restart();
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("SocketException: {0}", e);
            }    
        }

        public void ReadPoseOnce()
        {
            // read Stream data once
            try
            {
                if (mClient.Connected == false)
                {
                    mClient.Connect(URStreamData.IpAddress, URStreamData.Port);
                    Console.WriteLine("Connected: " + mClient.Connected);
                }
                mStream = mClient.GetStream();
                var t = new Stopwatch();
                if (mStream.Read(mBuffer, 0, mBuffer.Length) != 0)
                {
                    t.Start();
                    Array.Reverse(mBuffer);
                    Pair<Pose, SixJointAngles> pair = (Pair<Pose, SixJointAngles>) UpdateRobotCartesianData.ReadCartesianInput(mBuffer, mFirstPacketSize, mOffset);
                    //Console.WriteLine(pair.GetFirst());
                    t.Stop();
                    if (t.ElapsedMilliseconds < URStreamData.timeStep)
                    {
                        Thread.Sleep(URStreamData.timeStep - (int)t.ElapsedMilliseconds);
                    }
                    t.Restart();
                }
            }
            catch (Exception e) { }
        }
        

        public void Connect()
        {
            mExitThread = false;
            // Start a thread to control UR
            mThread = new Thread(() =>
            {
                ReadPoseOnce();
            });
            mThread.IsBackground = true;
            mThread.Start();
            //ReadPoseOnce();
            //Disconnect();
        }



        public void Disconnect()
        {
            if (mClient.Connected == true)
            {
                mThread.Interrupt();
                mThread.Join();

                mStream.Close();
                mClient.Close();
                Console.WriteLine("Status:" + mClient.Connected);
            }
            Thread.Sleep(100);
        }
    }
}
