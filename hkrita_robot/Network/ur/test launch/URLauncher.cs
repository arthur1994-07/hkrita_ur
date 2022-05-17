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
            SixJointAngles joints = new SixJointAngles(-2.35414463678469, - 2.38869373003115, - 0.47816068330874,
                - 2.18282062212099, 2.28414177894592, 0.117019392549992);

            RobotController mRobot = new RobotController("192.168.56.101");
            //mRobot.MoveLocation(startPose);
            //mRobot.MoveLocation(targetPose);
            //mRobot.MoveLocation(targetPose2);
            //mRobot.MoveLocation(targetPose3);
            mRobot.GetRobotLocation();
            mRobot.MoveJoint(joints);

            

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
