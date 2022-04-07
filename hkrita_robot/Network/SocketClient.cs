using hkrita_robot.Extension;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace hkrita_robot.Network
{
    public class SocketClient
    {
        private static readonly int CONNECTION_TIMEOUT = 3;
        private static readonly int SOCKET_TIMEOUT = 100;

        private IPEndPoint mEndPoint;
        private IPAddress mAddress;
        private TimeSpan mConnectTimeOut = TimeSpan.FromSeconds(CONNECTION_TIMEOUT);
        private TimeSpan mSocketTimeOut = TimeSpan.FromSeconds(SOCKET_TIMEOUT);


        public SocketClient(string ipAddress, int port)
        {
            mAddress = IPAddress.Parse(ipAddress);
            mEndPoint = new IPEndPoint(mAddress, port);
        }

        public void ConnectClient()
        {
            byte[] bytes = new byte[1024];
            try
            {

                Socket socket = new Socket(mAddress.AddressFamily,
                    SocketType.Stream, ProtocolType.Tcp);


                try
                {
                    socket.Connect(mEndPoint);
                    if (socket.Connected)
                    {
                        Console.WriteLine("Socket connected to : {0} ",
                            socket.RemoteEndPoint.ToString());
                    }
                    SendClientData(socket, bytes);


                    Thread.Sleep(3000);
                    // Release the socket.
                    socket.Shutdown(SocketShutdown.Both);
                    socket.Close();
                }
                catch (ArgumentNullException ane)
                {
                    Console.WriteLine("ArgumentNullException : {0}", ane.ToString());
                }
                catch (SocketException se)
                {
                    Console.WriteLine("SocketException : {0}", se.ToString());
                }
                catch (Exception e)
                {
                    Console.WriteLine("Unexpected exception : {0}", e.ToString());
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }

        public void SetConnectTimeOut(TimeSpan timeSpan)
        {
            mConnectTimeOut = TimeSpan.FromMilliseconds(Math.Max(timeSpan.Milliseconds, 1));
        }

        public void SetSocketTimeOut(TimeSpan timeSpan)
        {
            mSocketTimeOut = TimeSpan.FromMilliseconds(Math.Max(timeSpan.Milliseconds, 1));
        }

        public void SendClientData(Socket socket, byte[] bytes)
        {
            byte[] scriptData = Encoding.ASCII.GetBytes(StringHelper.InputString());
            // Send the data through the socket.
            int bytesSent = socket.Send(scriptData);
            // Receive the response from the remote device.
            int bytesRec = socket.Receive(bytes);
            Console.WriteLine(bytesRec);
            Console.WriteLine("Respone from server = {0}",
                Encoding.ASCII.GetString(bytes, 0, bytesRec));
        }
    }
}
