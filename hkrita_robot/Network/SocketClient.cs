using java.io;
using java.net;
using java.nio;
using java.nio.channels;
using java.time;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace hkrita_robot.Network
{
    public class SocketClient
    {
        private static readonly int CONNECT_TIMEOUT = 3;
        private static readonly int SOCKET_TIMEOUT = 100;
        private SocketChannel mSocket;
        private InetSocketAddress mAddress;
        private Duration mConnectionTimeOut = Duration.ofSeconds(CONNECT_TIMEOUT);
        private Duration mSocketTimeOut = Duration.ofSeconds(SOCKET_TIMEOUT);


        public void Close()
        {
            lock(this)
            {
                LocalClose();  
            }
        }
        public bool Connect()
        {
            lock (this)
            {
                return LocalConnect();
            }
        }
        public void LocalClose()
        {
            if (mSocket != null) mSocket.close();
            mSocket = null;
            
        }
        private bool LocalConnect()
        {
            if (mSocket != null) return true;
            try
            {
                LocalClose();
            }
            catch (Exception ex) { }

            try
            {
                mSocket = SocketChannel.open();
                mSocket.configureBlocking(false);
                mSocket.connect(mAddress);

                using (Selector selector = Selector.open())
                {
                    mSocket.register(selector, SelectionKey.OP_CONNECT);
                    int num = selector.select((int)mConnectionTimeOut.toMillis());
                    if (num == 0) throw new IOException();
                }

                if (mSocket.isConnectionPending())
                {
                    mSocket.finishConnect();
                }
            }
            catch (Exception ex)
            {
                try { mSocket.close(); }    

                catch (Exception e) { }
                if (ex.GetType().IsInstanceOfType(new InterruptedIOException())) throw (InterruptedIOException)ex;
                return false;
            }
            return true;
        }

        public bool WriteData(byte[] byteData, Func<Boolean> interrupted)
        {
            lock(this)
            {
                Selector selector = Selector.open();
                try
                {
                    mSocket.register(selector, SelectionKey.OP_WRITE);
                    ByteBuffer buffer = ByteBuffer.wrap(byteData);
                    for (int i = 0; i < byteData.Length;)
                    {
                        if (interrupted.Invoke()) throw new InterruptedIOException();
                        int num = selector.select((int)mSocketTimeOut.toMillis());
                        if (num == 0) continue;
                        i += mSocket.write(buffer);
                    }
                    return true;
                }
                catch (InterruptedIOException ex)
                {
                    throw ex;
                }
                catch (Exception ex)
                {
                    return false;
                }
            }
        }
    }
}
