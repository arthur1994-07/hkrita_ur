using hkrita_robot.Container;
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
        private static readonly int K_CONNECT_TIMEOUT = 3;
        private static readonly int K_SOCKET_TIMEOUT = 100;
        private const byte mFirstPacketSize = 4;
        private const byte mOffset = 8;


        private bool mExitThread = false;
        
        private TcpClient mClient = new TcpClient();
        private NetworkStream mStream = null; 
        private byte[] mBuffer = new byte[4096];
        private BufferedData mBufferData = new BufferedData();
        private string mAddress;
        private int mPort;

        public NetworkClient(String ipAddress, int port)
        {
            mAddress = ipAddress;
            mPort = port;
        }

        public void Connect(bool readStream)
        {
            lock (this)
            {
                InternalConnect(readStream);
            }
        }

        public void CloseThread()
        {
            if (mClient.Connected == true)
            {
                mStream.Dispose();
                mClient.Close();
            }
            Thread.Sleep(100);
        }

        // Client Connection 
        // Connection requires TcpClient and NetworkStream 
        private void InternalConnect(bool readStream)
        {
            try
            {
                if (mClient.Connected == false)
                {
                    mClient.Connect(mAddress, mPort);
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

                        // Read stream data
                        if (readStream) BufferedData.ReadPoseStreamInput(mBuffer, mFirstPacketSize, mOffset);
                         
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

        private void InternalClose()
        {
            mBufferData.Clear();
        }
        





    }
}
