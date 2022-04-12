using System;
using System.Diagnostics;
using System.Net.Sockets;
using System.Threading;

namespace hkrita_robot.Network
{
    public class URStream
    {
        //Thread 
        private Thread mRobotThread = null;

        private bool mExitThread = false;

        // TCP IP communication
        private TcpClient mClient = new TcpClient();
        private NetworkStream mStream = null;

        // Packet buffer (Read) 
        private byte[] mBuffer = new byte[4096];

        private const byte mFirstPacketSize = 4;
        private const byte mOffset = 8;

        private const UInt32 mTotalMsgLength = 3288596480;

        public void URStreamThread()
        {
            try
            {
                if (mClient.Connected == false)
                {
                    mClient.Connect(StreamData.IpAddress, StreamData.port);
                    Console.WriteLine("Connected: " + mClient.Connected);
                }
                // initialise TCP IP communication stream
                mStream = mClient.GetStream();

                var timer = new Stopwatch();
                while (mExitThread == false)
                {
                    //Console.WriteLine(mTotalMsgLength +", " + BitConverter.ToUInt32(mBuffer, mFirstPacketSize - 4));
                    // get data from robot
                    if (mStream.Read(mBuffer, 0, mFirstPacketSize) != 0)
                    {
                        //if (BitConverter.ToUInt32(mBuffer, mFirstPacketSize - 4) == mTotalMsgLength)
                        //{
                        timer.Start();

                        // reverses the order of the element in a one dimensional array 
                        Array.Reverse(mBuffer);
                        // for joint values in radians 


                        StreamData.J_Orientation[0] = BitConverter.ToDouble(mBuffer, mBuffer.Length - mFirstPacketSize - (32 * mOffset));
                        StreamData.J_Orientation[1] = BitConverter.ToDouble(mBuffer, mBuffer.Length - mFirstPacketSize - (33 * mOffset));
                        StreamData.J_Orientation[2] = BitConverter.ToDouble(mBuffer, mBuffer.Length - mFirstPacketSize - (34 * mOffset));
                        StreamData.J_Orientation[3] = BitConverter.ToDouble(mBuffer, mBuffer.Length - mFirstPacketSize - (35 * mOffset));
                        StreamData.J_Orientation[4] = BitConverter.ToDouble(mBuffer, mBuffer.Length - mFirstPacketSize - (36 * mOffset));
                        StreamData.J_Orientation[5] = BitConverter.ToDouble(mBuffer, mBuffer.Length - mFirstPacketSize - (37 * mOffset));

                        // read Cartesian Position values in metres
                        StreamData.C_Position[0] = BitConverter.ToDouble(mBuffer, mBuffer.Length - mFirstPacketSize - (56 * mOffset));
                        StreamData.C_Position[1] = BitConverter.ToDouble(mBuffer, mBuffer.Length - mFirstPacketSize - (57 * mOffset));
                        StreamData.C_Position[2] = BitConverter.ToDouble(mBuffer, mBuffer.Length - mFirstPacketSize - (58 * mOffset));

                        // read Cartesian Orientation values in metres 
                        StreamData.C_Orientation[0] = BitConverter.ToDouble(mBuffer, mBuffer.Length - mFirstPacketSize - (59 * mOffset));
                        StreamData.C_Orientation[1] = BitConverter.ToDouble(mBuffer, mBuffer.Length - mFirstPacketSize - (60 * mOffset));
                        StreamData.C_Orientation[2] = BitConverter.ToDouble(mBuffer, mBuffer.Length - mFirstPacketSize - (61 * mOffset));

                            timer.Stop();

                            if (timer.ElapsedMilliseconds < StreamData.timeStep)
                            {
                                Thread.Sleep(StreamData.timeStep - (int)timer.ElapsedMilliseconds);
                            }
                            timer.Restart();
                        }
                    }
                //}
            }
            catch (SocketException e)
            {
                Console.WriteLine("SocketException: {0}", e);
            }
        }

        public void Start()
        {
            mExitThread = false;
            // Start a thread to control UR 
            mRobotThread = new Thread(new ThreadStart(URStreamThread));
            mRobotThread.IsBackground = true;
            mRobotThread.Start();
        }

        public void Stop()
        {
            mExitThread = true;
            if (mRobotThread.IsAlive == true)
            {
                Thread.Sleep(100);
            }
        }

        public void Destroy()
        {
            Stop();
            if (mClient.Connected == true)
            {
                mStream.Dispose();
                mClient.Close();
            }
            Thread.Sleep(100);
        }
    }
}

