using hkrita_robot.Container;
using hkrita_robot.Extension;
using hkrita_robot.Maths;
using hkrita_robot.Network.ur.internalData;
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
        private byte[] mBuffer = new byte[mByteSize];
        private static int mByteSize = 4096;
        private BufferedData mBufferData = new BufferedData();
        private string mAddress;
        private int mPort;


        public NetworkClient(String ipAddress, int port)
        {
            mAddress = ipAddress;
            mPort = port;
            mBufferData.WriteData(mByteSize);
        }

        public bool Connect()
        {
            return OldInternalConnect();
        }
        public object Connect(bool readStream, string script)
        {
            return InternalConnect(readStream, script);
        }

        public byte[] GetRemainData()
        {
            int count = mBufferData.Count();
            return count == 0 ? null : mBufferData.WriteData(mByteSize);
        }

        public byte[] Read()
        { 
           return InternalReadData();
        }


        private bool OldInternalConnect()
        {
            // method to connect client to UR server
            try
            {
                mTCPClient.Connect(mAddress, mPort);
                if (mTCPClient.Connected == false) return false;
                mStream = mTCPClient.GetStream();
                Console.WriteLine("Connected");
            }
            catch (Exception e) 
            { 
                Disconnect(); 
            }
            return true;
        }

        public void Disconnect()
        {
            if (mTCPClient.Connected == true)
            {
                mStream.Close();
                mTCPClient.Close();
                Console.WriteLine("Status: " + mTCPClient.Connected);   
            }
            Thread.Sleep(100);
        }

        // Client Connection: connection action is performed by one single thread
        private object InternalConnect(bool readStream, string script)
        {
            try
            {
                if (mTCPClient.Connected == false) mTCPClient.Connect(mAddress, mPort);
                mStream = mTCPClient.GetStream();
                var t = new Stopwatch();
                // execute script once
                if (readStream == false)
                {
                    UTF8Encoding encoder = new UTF8Encoding();
                    mBuffer = encoder.GetBytes(script);
                    mStream.Write(mBuffer, 0, mBuffer.Length);
                    Thread.Sleep(1000);
                    return null;
                }
                // Read stream data
                if (readStream == true) return (Pair<Pose, SixJointAngles>)ReadStream(mBuffer, mFirstPacketSize, mOffset, t);
                
            }
            catch (Exception e)
            {}
            return null;
        }

        private object ReadStream(byte[] buffer, byte firstPacketSize, byte offset, Stopwatch timer)
        {
            lock(this)
            {
                if (mStream.Read(buffer, 0, buffer.Length) != 0)
                {
                    timer.Start();
                    Array.Reverse(buffer);

                    // Read stream data 
                    //Pair<Pose, SixJointAngles> pair = (Pair<Pose, SixJointAngles>) UpdateRobotCartesianData.ReadCartesianInput(buffer, firstPacketSize, offset);

                    timer.Stop();
                    if (timer.ElapsedMilliseconds < timeStep) Thread.Sleep(timeStep - (int)timer.ElapsedMilliseconds);
                    timer.Restart();
                    ClearBuffer(mBuffer);
                    return null;
                }
                return null;
            }
        }


        private byte[] ReadByteStream(byte[] buffer, byte firstPacketSize, byte offset, Stopwatch timer)
        //private byte[] ReadByteStream(BufferedData bufferedData, Stopwatch timer)
        {
            lock (this)
            {
                if (mStream.Read(buffer, 0, buffer.Length) == 0) return null;
                timer.Start();
                Array.Reverse(buffer);
                timer.Stop();
                if (timer.ElapsedMilliseconds < timeStep) Thread.Sleep(timeStep - (int)timer.ElapsedMilliseconds);
                return buffer;
            }
        }

        private byte[] InternalReadData()
        {
            // Read Stream while connected 
            if (mStream.CanRead == false) return null;
            var t = new Stopwatch();
            return ReadByteStream(mBufferData.buffer, BufferedData.firstPacketSize, BufferedData.streamOffset, t);
        }
         
        private void ClearBuffer(byte[] buffer)
        {
            ArraysHelper.Fill(buffer);
           
        }
        private void InternalClose()
        {
            mBufferData.Clear();
        }
        



    }
}
