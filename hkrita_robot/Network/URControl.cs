using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace hkrita_robot.Network
{
    public class URControl
    {
        private Thread mThread = null;
        private bool mExitThread = false;

        private TcpClient mClient = new TcpClient();
        private NetworkStream mStream = null;

        private byte[] mPackedScript;

        private UTF8Encoding mEncoder = new UTF8Encoding();

        public void ControlThread()
        {
            try
            {
                if (mClient.Connected == false)
                {
                    mClient.Connect(URData.IpAddress, URControlData.Port);
                }

                mStream = mClient.GetStream();
                while (mExitThread == false)
                {
                    // Instruction 1 (Home Position): Joint Input Command, Move Joint Interpolation
                    //  Get Bytes from String
                    mPackedScript = mEncoder.GetBytes("movej([" + URControlData.J_Orientation[0].ToString() + "," + URControlData.J_Orientation[1].ToString() + "," + URControlData.J_Orientation[2].ToString() + ","
                                                        + URControlData.J_Orientation[3].ToString() + "," + URControlData.J_Orientation[4].ToString() + "," + URControlData.J_Orientation[5].ToString() + "],"
                                                         + "a=" + URControlData.acceleration + ", v=" + URControlData.velocity + ")" + "\n");
                    mStream.Write(mPackedScript, 0, mPackedScript.Length);
                    Thread.Sleep(1000);
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
            mThread = new Thread(new ThreadStart(ControlThread));
            mThread.IsBackground = true;
            mThread.Start();
        }

        public void Stop()
        {
            mExitThread = true;
            // Start a thread
            if (mThread.IsAlive == true)
            {
                Thread.Sleep(1000); 
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
