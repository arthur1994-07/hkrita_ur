using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

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

        public void StreamThread()
        {
            try
            {
                if (mClient.Connected == false)
                {
                    mClient.Connect(URData.IpAddress, URStreamData.Port);
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

                        // Read Joint Values in radians
                        URStreamData.J_Orientation[0] = BitConverter.ToDouble(mBuffer, mBuffer.Length - mFirstPacketSize - (32 * mOffset));
                        URStreamData.J_Orientation[1] = BitConverter.ToDouble(mBuffer, mBuffer.Length - mFirstPacketSize - (33 * mOffset));
                        URStreamData.J_Orientation[2] = BitConverter.ToDouble(mBuffer, mBuffer.Length - mFirstPacketSize - (34 * mOffset));
                        URStreamData.J_Orientation[3] = BitConverter.ToDouble(mBuffer, mBuffer.Length - mFirstPacketSize - (35 * mOffset));
                        URStreamData.J_Orientation[4] = BitConverter.ToDouble(mBuffer, mBuffer.Length - mFirstPacketSize - (36 * mOffset));
                        URStreamData.J_Orientation[5] = BitConverter.ToDouble(mBuffer, mBuffer.Length - mFirstPacketSize - (37 * mOffset));
                        // Read Cartesian (Positon) Values in metres

                        URStreamData.C_Position[0] = BitConverter.ToDouble(mBuffer, mBuffer.Length - mFirstPacketSize - (56 * mOffset));
                        URStreamData.C_Position[1] = BitConverter.ToDouble(mBuffer, mBuffer.Length - mFirstPacketSize - (57 * mOffset));
                        URStreamData.C_Position[2] = BitConverter.ToDouble(mBuffer, mBuffer.Length - mFirstPacketSize - (58 * mOffset));
                        // Read Cartesian (Orientation) Values in metres 
                        URStreamData.C_Orientation[0] = BitConverter.ToDouble(mBuffer, mBuffer.Length - mFirstPacketSize - (59 * mOffset));
                        URStreamData.C_Orientation[1] = BitConverter.ToDouble(mBuffer, mBuffer.Length - mFirstPacketSize - (60 * mOffset));
                        URStreamData.C_Orientation[2] = BitConverter.ToDouble(mBuffer, mBuffer.Length - mFirstPacketSize - (61 * mOffset));
                        t.Stop();
                        if (t.ElapsedMilliseconds < URData.timeStep)
                        {
                            Thread.Sleep(URData.timeStep - (int)t.ElapsedMilliseconds);
                        }

                        t.Restart();
                    }
                }
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
            mThread = new Thread(new ThreadStart(StreamThread));
            mThread.IsBackground = true;
            mThread.Start();
        }

        public void Stop()
        {
            mExitThread = true;
            if (mThread.IsAlive == true)
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
