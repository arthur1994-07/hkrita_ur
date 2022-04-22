using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using hkrita_robot.Container;
using hkrita_robot.Network;
using hkrita_robot.Network.ur;
using hkrita_robot.UR;

namespace hkrita_robot.Network
{
    public class URLauncher
    {
        public URLauncher()
        {

            RobotSystem robot = new RobotSystem("192.168.56.101");
            //robot.Connect(true);
            RealTimeSystem stream = new RealTimeSystem("192.168.56.101");
            stream.Connect();

            //NetworkClient network = new NetworkClient("192.168.56.101", 30013);
            //network.Connect(true);

            URStream urStream = new URStream();
            //urStream.Connect();
            //URControl urControl = new URControl();
            //urControl.Connect();

            Console.WriteLine("[INFO] Press Q to exit:");
            string stop = Convert.ToString(Console.ReadLine());

            if (stop == "q")
            {
                Console.WriteLine("Cartesian Space: Position (metres), Orientation (radian):");
                Console.WriteLine("X: {0} | Y: {1} | Z: {2}",
                                   URStreamData.C_Position[0], URStreamData.C_Position[1], URStreamData.C_Position[2]);
                // Destroy UR {Control / Stream}

                stream.Close();

                //robot.Close();
                //network.CloseThread();

                //urStream.Destroy();

                //urControl.Destroy();

                // Application quit
                Environment.Exit(0);
            }
        }

    }
}
