using hkrita_robot.Network;
using hkrita_robot.Network.ur;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace hkrita_robot.UR
{
    public class RobotSystem
    {
        private readonly static int NORMAL_PORT = 30002;
            
        private readonly string mAddress;
        private Thread mThread;
        private Boolean mExitThread;
        private Boolean mClosed; 
        private NetworkClient mNetworkClient;
        private bool mStreamData = false;

        public RobotSystem(string ipAddress)
        {
            mAddress = ipAddress;
            mNetworkClient = new NetworkClient(mAddress, NORMAL_PORT);
        }

        public void Connect()
        {
            try
            {
                InternalConnect();
            }
            catch(Exception e)
            {
                Close();
                throw e;
            }
        }

        public void Close()
        {
            CloseThread();
        }

        private void InternalConnect()
        {
            mThread = new Thread(() =>
            {
                Console.WriteLine("Robot Connection {0} is established: " , mAddress);
                mNetworkClient.Connect(mStreamData);
            });
       
            mClosed = false;

            mThread.IsBackground = true;
            mThread.Start();
        }

        private void CloseThread()
        {
            if (mThread == null) return;
            try
            {
                mThread.Interrupt();
                mThread.Join();
            }
            catch (Exception ex) { }
            mThread = null;
        }

        private void Stop()
        {
            mExitThread = true;
            Console.WriteLine(mThread.IsAlive);
            if (mThread.IsAlive == true)
            {
                Thread.Sleep(100);
            }
        }

        public void Destroy()
        {
            Stop();
            mNetworkClient.CloseThread();
        }


        
    }
}
