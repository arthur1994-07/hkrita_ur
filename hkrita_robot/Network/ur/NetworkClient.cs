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

        public void Connect(bool readStream, string script)
        {
            InternalConnect(readStream, script);
        }

        public void CloseThread()
        {
            if (mTCPClient.Connected == true)
            {
                mStream.Close();
                mTCPClient.Close();
                Console.WriteLine("Connection Status:" + mTCPClient.Connected);
            }
            Thread.Sleep(100);
        }

        // Client Connection: connection action is performed by one single thread
        // 
        private void InternalConnect(bool readStream, string script)
        {
            try
            {
                if (mTCPClient.Connected == false) mTCPClient.Connect(mAddress, mPort);
                mStream = mTCPClient.GetStream();
                var t = new Stopwatch();

                //loop thread
                while (mExitThread == false)
                {
                    // Client connected 
                    if (readStream == false)
                    {

                        mBuffer = mEncoder.GetBytes(script);
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
