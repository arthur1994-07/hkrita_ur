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

            Pose pose = new Pose(-0.02, 0.01, 0, 0, 0, 0.03);
            Pose scannerTcp = new Pose(-0.02, 0.01, 0.176, 0, 0, 0.03);
            Pose startPose = new Pose(-0.15, -0.412, 0.150, 0, 3.15, 0);
            Pose targetPose = new Pose(-0.15, -0.23, 0.15, 0, 3.15, 0);
            Pose targetPose2 = new Pose(-0.15, -0.314, 0.414, 0, 3.15, 0);
            Pose targetPose3 = new Pose(-0.15, -0.258, 0.444, 1.169, 2.092, -0.008);


            RobotController mRobot = new RobotController("192.168.56.101");
            ////mRobot.SetTCP(pose2);
            mRobot.MoveLocation(startPose, 0.5, 0.5);
            mRobot.MoveLocation(targetPose, 0.5, 0.5);
            mRobot.MoveLocation(targetPose2, 0.5, 0.5);
            mRobot.MoveLocation(targetPose3, 0.5, 0.5);
            mRobot.GetRobotLocation();
            mRobot.SetTCP(scannerTcp);
            Console.WriteLine(mRobot.GetTcp());
            Console.WriteLine("[INFO] Press Q to exit:");
            string stop = Convert.ToString(Console.ReadLine());

            if (stop == "q")
            {
                Console.WriteLine("X: {0} | Y: {1} | Z: {2}",
                URStreamData.C_Position[0], URStreamData.C_Position[1], URStreamData.C_Position[2]);

                // Application quit
                Environment.Exit(0);
            }
        }

    }
}
