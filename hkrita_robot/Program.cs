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
        static void Main(string[] args)
        {
            SocketClient client = new SocketClient("192.168.56.101", 30001);
            // continuously send bytes to UR from client
            while (true)
            {
                client.ConnectClient();
            }
        }
    }
}
