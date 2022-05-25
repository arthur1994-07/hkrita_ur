using hkrita_robot.Extension;
using hkrita_robot.Network.ur;
using hkrita_robot.Network.ur.internalData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hkrita_robot.Network
{
    public class NormalClient
    {
        private NetworkClient mConnection;
        private InternalUpdateRobotDataListener mInternalSystem;
        public NormalClient(string ipAddress, int port)
        {
            mConnection = new NetworkClient(ipAddress, port);
        }

        public bool Connect() { return mConnection.Connect(); }


        public bool GetMessage(Action<object> action)
        {
            byte[] streamData = mConnection.Read();
            if (streamData == null) return false;
            ActionHelper.SetAction(action, streamData);
            return true;
        }


        public bool SendMessage()
        {
            return true;
        }

    }
}
