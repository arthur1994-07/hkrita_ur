using hkrita_robot.Network;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hkrita_robot
{
    public class Program
    {
        public static void Main(string[] args)
        {
            //AsyncSocketClient asyncClient = new AsyncSocketClient("192.168.56.101", 30002);
            SyncSocketClient client = new SyncSocketClient("192.168.56.101", 30002);
            // continuously send bytes to UR from client..
            //SyncSocketClient.WriteScript();
            while (true)
            {
                client.ConnectClient();
            }
        }
    }
}
