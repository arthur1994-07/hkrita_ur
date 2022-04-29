using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using hkrita_robot.Extension;
using hkrita_robot.Network;
using hkrita_robot.Network.ur;

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

        public void InternalConnect()
        { 
            try
            {
                if (mClient.Connected == false)
                {
                    mClient.Connect(URControlData.IpAddress, URControlData.Port);
                }

                mStream = mClient.GetStream();

                while (mExitThread == false)
                {

                    //Instruction 1(Home Position): Joint Input Command, Move Joint Interpolation
                    //Get Bytes from String
                    //mPackedScript = mEncoder.GetBytes("movej([" + URControlData.J_Orientation[0].ToString() + "," + URControlData.J_Orientation[1].ToString() + "," + URControlData.J_Orientation[2].ToString() + ","
                    //                                      + URControlData.J_Orientation[3].ToString() + "," + URControlData.J_Orientation[4].ToString() + "," + URControlData.J_Orientation[5].ToString() + "],"
                    //                                       + "a=" + URControlData.acceleration + ", v=" + URControlData.velocity + ")" + "\n");
                    //mStream.Write(mPackedScript, 0, mPackedScript.Length);
                    //Thread.Sleep(1000);


                    //mPackedScript = mEncoder.GetBytes("[movel(p[" + URControlData.C_Position[0].ToString() + "," + URControlData.C_Position[1].ToString() + "," + (URControlData.C_Position[2] - 0.1).ToString() + ","
                    //                       + URControlData.C_Orientation[0].ToString() + "," + URControlData.C_Orientation[1].ToString() + "," + URControlData.C_Orientation[2].ToString() + "],"
                    //                       + "a=" + URControlData.acceleration + ", v=" + URControlData.velocity + ")," +
                    //           "movel(p[" + (URControlData.C_Position[0] - 0.1).ToString() + ", " + URControlData.C_Position[1].ToString() + ", " + (URControlData.C_Position[2] - 0.1).ToString() + ", "
                    //                       + URControlData.C_Orientation[0].ToString() + "," + URControlData.C_Orientation[1].ToString() + "," + URControlData.C_Orientation[2].ToString() + "],"
                    //                       + "a=" + URControlData.acceleration + ", v=" + URControlData.velocity + ")," +
                    //           "movel(p[" + (URControlData.C_Position[0] - 0.1).ToString() + ", " + URControlData.C_Position[1].ToString() + ", " + URControlData.C_Position[2].ToString() + ", "
                    //                       + URControlData.C_Orientation[0].ToString() + "," + URControlData.C_Orientation[1].ToString() + "," + URControlData.C_Orientation[2].ToString() + "],"
                    //                       + "a=" + URControlData.acceleration + ", v=" + URControlData.velocity + ")," +
                    //           "movel(p[" + URControlData.C_Position[0].ToString() + ", " + URControlData.C_Position[1].ToString() + ", " + (URControlData.C_Position[2]).ToString() + ", "
                    //                       + URControlData.C_Orientation[0].ToString() + "," + URControlData.C_Orientation[1].ToString() + "," + URControlData.C_Orientation[2].ToString() + "],"
                    //                       + "a=" + URControlData.acceleration + ", v=" + URControlData.velocity + ")]" + "\n");


                    //  //Send command to the robot
                    //mStream.Write(mPackedScript, 0, mPackedScript.Length);
                    //  //Wait Time(5 seconds)
                    //Thread.Sleep(1000);
                    //String test = StringHelper.FormatString("set_tcp({0})", URControlData.testTcpPose);
                    String test = StringHelper.Format("set_tcp({0})", URControlData.testTcpPose, URControlData.testTcpPose2);
                    test += "\n";
                    //mPackedScript = mEncoder.GetBytes(test);
                    //string test2 = "set_tcp(p[" + URControlData.testTcpPose.GetPosition().x.ToString() + ", " +
                    //    URControlData.testTcpPose.GetPosition().y.ToString() + ", "
                    //    + URControlData.testTcpPose.GetPosition().z.ToString() + ", "
                    //    + URControlData.testTcpPose.GetRotation().x.ToString() + ", "
                    //    + URControlData.testTcpPose.GetRotation().y.ToString() + ", "
                    //    + URControlData.testTcpPose.GetRotation().z.ToString() + "])" + "\n";
                    mPackedScript = mEncoder.GetBytes(test);
                    mStream.Write(mPackedScript, 0, mPackedScript.Length);
                    Thread.Sleep(1000);
                }
            }

            catch (SocketException e)
            {
                Console.WriteLine("SocketException: {0}", e);
            }
        }

        public void Connect()
        {
            mExitThread = false;
            // Start a thread to control UR
            mThread = new Thread(new ThreadStart(InternalConnect));
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
