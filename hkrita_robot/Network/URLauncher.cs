using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using com.sun.istack.@internal.logging;
using hkrita_robot.Container;
using hkrita_robot.Maths;
using hkrita_robot.Network;
using hkrita_robot.Network.script;
using hkrita_robot.Network.ur;
using hkrita_robot.UR;
using hkrita_robot.UR.accessor;
using hkrita_robot.UR.control;

namespace hkrita_robot.Network
{
    public class URLauncher
    {

        // the current robot arm control uses primary secondary interface 30001 30002 port
        // streaming data port using 30003/30013 (old version pre 3.5)
        public URLauncher()
        {
            //RobotSystem robot = new RobotSystem("192.168.56.101");
            //robot.Connect();
            RobotController mRobot = new RobotController("192.168.56.101");
            mRobot.Connect();
            mRobot.GetRobotLocation();


            Pose p = new Pose(0, 0.1, 0.1, 0, 0, 0);
            mRobot.MoveLocation(p, 0.1, 0.1);
            
            //RealTimeSystem stream = new RealTimeSystem("192.168.56.101");
            //stream.Connect();

            Console.WriteLine("[INFO] Press Q to exit:");
            string stop = Convert.ToString(Console.ReadLine());

            if (stop == "q")
            {
                Console.WriteLine("X: {0} | Y: {1} | Z: {2}",
                URStreamData.C_Position[0], URStreamData.C_Position[1], URStreamData.C_Position[2]);
                //stream.Close();

                //robot.Close();

                // Application quit
                Environment.Exit(0);
            }
        }

    }
}
