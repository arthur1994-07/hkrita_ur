using hkrita_robot.Container;
using hkrita_robot.Extension;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace hkrita_robot.Network.ur
{
    public class NetworkClient
    {
        private static int timeStep = 8;
        private static readonly int K_CONNECT_TIMEOUT = 3;
        private static readonly int K_SOCKET_TIMEOUT = 100;
        private const byte mFirstPacketSize = 4;
        private const byte mOffset = 8;

        private bool mExitThread = false;
        private TcpClient mTCPClient = new TcpClient();
        private NetworkStream mStream = null; 
        private byte[] mBuffer = new byte[4096];
        private BufferedData mBufferData = new BufferedData();
        private UTF8Encoding mEncoder = new UTF8Encoding();
        private string mAddress;
        private int mPort;

        public NetworkClient(String ipAddress, int port)
        {
            mAddress = ipAddress;
            mPort = port;
        }

        public void Connect(bool readStream)
        {
            InternalConnect(readStream);
        }

        public void CloseThread()
        {
            if (mTCPClient.Connected == true)
            {
                //Console.WriteLine("Still connected");
                mStream.Close();
                mTCPClient.Close();
                Console.WriteLine("Connection Status:" + mTCPClient.Connected);
            }
            Thread.Sleep(100);
        }

        // Client Connection 
        // Connection requires TcpClient and NetworkStream 
        // TODO: add action delegate as input argument
        private void InternalConnect(bool readStream)
        {
            try
            {
                if (mTCPClient.Connected == false) mTCPClient.Connect(mAddress, mPort);
                mStream = mTCPClient.GetStream();
                var t = new Stopwatch();


                while (mExitThread == false)
                {
                    if (readStream == false)
                    {
                        String test = StringHelper.Format("set_tcp({0})", URControlData.testTcpPose, URControlData.testTcpPose2) + "\n";
                        mBuffer = mEncoder.GetBytes(test);
                        mStream.Write(mBuffer, 0, mBuffer.Length);
                        Thread.Sleep(1000);
                    }

                    // Read stream data
                    if (readStream == true) ReadStream(mBuffer, mFirstPacketSize, mOffset, t);
                }
            }
            catch (Exception e)
            {}
        }

        private void ReadStream(byte[] buffer, byte firstPacketSize, byte offset, Stopwatch timer)
        {
            lock(this)
            {
                if (mStream.Read(mBuffer, 0, mBuffer.Length) != 0)
                {
                    timer.Start();
                    Array.Reverse(mBuffer);

                    BufferedData.ReadPoseStreamInput(mBuffer, mFirstPacketSize, mOffset);

                    timer.Stop();
                    if (timer.ElapsedMilliseconds < timeStep) Thread.Sleep(timeStep - (int)timer.ElapsedMilliseconds);
                    timer.Restart();
                }
            }
        }

        private void InternalClose()
        {
            mBufferData.Clear();
        }
        



    }
}
