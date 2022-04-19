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
    public class NetworkClient
    {
        private static readonly int K_CONNECT_TIMEOUT = 3;
        private static readonly int K_SOCKET_TIMEOUT = 100;
        private Thread mThread;
        private bool mExitThread = false;
        private TcpClient mClient = new TcpClient();
        private NetworkStream mStream = null; 
        private byte[] mBuffer = new byte[4096];
        private BufferedData mBufferData = new BufferedData();

        public NetworkClient(String ipAddress, int port)
        {

        }



        private bool InternalConnect()
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

                        // Read stream data 


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
            return true;
        }
        



    }
}
