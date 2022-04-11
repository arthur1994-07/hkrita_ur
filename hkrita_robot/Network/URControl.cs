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
        private Thread mRobotThread = null;
        private bool mExitThread = false;

        // TCP IP Communication 
        private TcpClient mClient = new TcpClient();
        private NetworkStream mStream = null;

        // packet buffer
        private byte[] mBuffer;

        // Encoding 
        private UTF8Encoding mEncoder = new UTF8Encoding();

        public void URControlThread()
        {
            try
            {
                if (mClient.Connected == false)
                {
                    mClient.Connect(ControlData.IpAddress, ControlData.port);
                }
                mStream = mClient.GetStream();

                while (mExitThread == false)
                {
                    // Instruction 1
                    // Get bytes from String 
                    mBuffer = mEncoder.GetBytes("movej([" + ControlData.J_Orientation[0].ToString() + ", " + ControlData.J_Orientation[1].ToString() +
                        ", " + ControlData.J_Orientation[2].ToString() + ", " + ControlData.J_Orientation[3].ToString().ToString() + ", " + ControlData.J_Orientation[4].ToString() +
                        ", " + ControlData.J_Orientation[5].ToString() + "]," + "a=" + ControlData.acceleration + "v=" + ControlData.velocity + ")" + "\n");

                    // send command to robot 
                    mStream.Write(mBuffer, 0, mBuffer.Length);

                    Thread.Sleep(5000);


                    // Instruction 2 (Multiple Positions) : Cartesian Input command, Move Linear Interpolation
                    // Get bytes from String
                    mBuffer = mEncoder.GetBytes("[movel(p[" + ControlData.C_Position[0].ToString() + ", " + ControlData.C_Position[1].ToString() + ", " +
                        (ControlData.C_Position[2] - 0.1).ToString() + ", " + ControlData.C_Orientation[0].ToString() + ", " + ControlData.C_Orientation[1].ToString() + ", " +
                        ControlData.C_Orientation[2].ToString() + "], " + "a=" + ControlData.acceleration + ", " + "v=" + ControlData.velocity + ")" +
                        "[movel(p[" + (ControlData.C_Position[0] - -0.1).ToString() + ", " + ControlData.C_Position[1].ToString() + ", " +
                        (ControlData.C_Position[2] - 0.1).ToString() + ", " + ControlData.C_Orientation[0].ToString() + ", " + ControlData.C_Orientation[1].ToString() + ", " +
                        (ControlData.C_Orientation[2] - 0.1).ToString() + "], " + "a=" + ControlData.acceleration + ", " + "v=" + ControlData.velocity + ")" +
                        "[movel(p[" + ControlData.C_Position[0].ToString() + ", " + ControlData.C_Position[1].ToString() + ", " +
                        (ControlData.C_Position[2] - 0.1).ToString() + ", " + ControlData.C_Orientation[0].ToString() + ", " + ControlData.C_Orientation[1].ToString() + ", " +
                        ControlData.C_Orientation[2].ToString() + "], " + "a=" + ControlData.acceleration + ", " + "v=" + ControlData.velocity + ")" +
                        "[movel(p[" + ControlData.C_Position[0].ToString() + ", " + ControlData.C_Position[1].ToString() + ", " +
                        (ControlData.C_Position[2] - 0.1).ToString() + ", " + ControlData.C_Orientation[0].ToString() + ", " + ControlData.C_Orientation[1].ToString() + ", " +
                        ControlData.C_Orientation[2].ToString() + "], " + "a=" + ControlData.acceleration + ", " + "v=" + ControlData.velocity + ")]" + "\n");

                    mStream.Write(mBuffer, 0, mBuffer.Length);
                    Thread.Sleep(5000);

                    // Instructor 3 (Multiple Positions): Cartesian Input Command, Move Joint Interpolation
                    // Get Bytes from String
                    mBuffer = mEncoder.GetBytes("[movej(p[" + ControlData.C_Position[0].ToString() + ", " + ControlData.C_Position[1].ToString() + ", " +
                       (ControlData.C_Position[2] - 0.1).ToString() + ", " + ControlData.C_Orientation[0].ToString() + ", " + ControlData.C_Orientation[1].ToString() + ", " +
                       ControlData.C_Orientation[2].ToString() + "], " + "a=" + ControlData.acceleration + ", " + "v=" + ControlData.velocity + ")" +
                       "[movej(p[" + (ControlData.C_Position[0] - -0.1).ToString() + ", " + ControlData.C_Position[1].ToString() + ", " +
                       (ControlData.C_Position[2] - 0.1).ToString() + ", " + ControlData.C_Orientation[0].ToString() + ", " + ControlData.C_Orientation[1].ToString() + ", " +
                       (ControlData.C_Orientation[2] - 0.1).ToString() + "], " + "a=" + ControlData.acceleration + ", " + "v=" + ControlData.velocity + ")" +
                       "[movej(p[" + ControlData.C_Position[0].ToString() + ", " + ControlData.C_Position[1].ToString() + ", " +
                       (ControlData.C_Position[2] - 0.1).ToString() + ", " + ControlData.C_Orientation[0].ToString() + ", " + ControlData.C_Orientation[1].ToString() + ", " +
                       ControlData.C_Orientation[2].ToString() + "], " + "a=" + ControlData.acceleration + ", " + "v=" + ControlData.velocity + ")" +
                       "[movej(p[" + ControlData.C_Position[0].ToString() + ", " + ControlData.C_Position[1].ToString() + ", " +
                       (ControlData.C_Position[2] - 0.1).ToString() + ", " + ControlData.C_Orientation[0].ToString() + ", " + ControlData.C_Orientation[1].ToString() + ", " +
                       ControlData.C_Orientation[2].ToString() + "], " + "a=" + ControlData.acceleration + ", " + "v=" + ControlData.velocity + ")]" + "\n");

                    mStream.Write(mBuffer, 0, mBuffer.Length);
                    Thread.Sleep(5000);
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
            //Start a thread to Control UR
            mRobotThread = new Thread(new ThreadStart(URControlThread));
            mRobotThread.IsBackground = true;
            mRobotThread.Start();
        }

        public void Stop()
        {
            mExitThread = true;
            // start a thread
            if (mRobotThread.IsAlive == true)
            {
                Thread.Sleep(100);
            }
        }

        public void Destroy()
        {
            // Start a thread and disconnect tcp/ip communication  
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
