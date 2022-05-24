using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading;
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
        private Pose pose = new Pose(-0.02, 0.01, 0, 0, 0, 0.03);
        Pose scannerTcp = new Pose(-0.02, 0.01, 0.176, 0, 0, 0.03);
        private Pose mStartPose = new Pose(-0.15, -0.412, 0.150, 0, 3.15, 0);
        private Pose mTargetPose = new Pose(-0.15, -0.23, 0.15, 0, 3.15, 0);
        private Pose mTargetPose2 = new Pose(-0.15, -0.314, 0.414, 0, 3.15, 0);
        private Pose mTargetPose3 = new Pose(-0.15, -0.258, 0.444, 1.169, 2.092, -0.008);
        private Pose mTargetPose4 = new Pose(-0.15, -0.312, 0.05, 0, 3.15, 0);
        private SixJointAngles mJoints = new SixJointAngles(-2.35414463678469, - 2.38869373003115, - 0.47816068330874,
            - 2.18282062212099, 2.28414177894592, 0.117019392549992);

        private RobotController mRobot = new RobotController("192.168.56.101");
        private URStream stream = new URStream();
        private URStream stream2 = new URStream();
        private URStream stream3 = new URStream();
        private URStream stream4 = new URStream();


        // the current robot arm control uses primary secondary interface 30001 30002 port
        // streaming data port using 30003/30013 (old version pre 3.5)
        public URLauncher()
        {
            mRobot.SubmitScript(s =>
            {
                Console.WriteLine(s);
            });

            RobotSystem robotSys = new RobotSystem("192.168.56.101");
            robotSys.Connect();
            //mRobot.MoveLocation(mStartPose);
            ////Pose mStartPose = mRobot.GetRobotLocation();
            //stream.Connect();
            //Thread.Sleep(1000);
            //mRobot.MoveLocation(mTargetPose2);
            //stream2.Connect();
            //Thread.Sleep(1000);
            //mRobot.MoveLocation(mTargetPose3);
            ////RobotLaunch(mRobot);
            //stream3.Connect();
            //Thread.Sleep(1000);

            //CloseRobotApp();

        }

        public void RobotLaunch(RobotController robot)
        {
            robot.MoveLocation(mStartPose);
            Pose startPt = robot.GetRobotLocation();
            Matrix3D rot = startPt.GetRotation().ToRotationMatrix();
            Vector3D zRotate = new Vector3D(0, 0, 1.57);
            rot.Multiply(zRotate);
            

            Pose pose2 = robot.GetRobotLocation();
            Pose moveAlongZ = pose2.MoveAlong(0.1, Pose.Direction.Z);
            robot.MoveLocation(moveAlongZ);
            Pose moveAlongX = moveAlongZ.MoveAlong(0.1, Pose.Direction.Y);
            robot.MoveLocation(moveAlongX);

            robot.MoveLocation(mTargetPose4);
          
            //Pose finalLocation = robot.GetRobotLocation();
            //if (moveAlongZ != finalLocation) Console.WriteLine("no match!!");

            //robot.MoveJoint(mJoints);
            Console.WriteLine("");
        }

        public void CloseRobotApp()
        {

            Console.WriteLine("[INFO] Press Q to exit:");
            string stop = Convert.ToString(Console.ReadLine());

            if (stop == "q")
            {
                Console.WriteLine("Program terminated");
                // Application quit
                Environment.Exit(0);
            }
        }

        //public async Task<int> StaticRoller(RobotController robot)
        //{
        //    await Task.Run(() =>
        //    {
        //        robot.MoveJoint();
        //    });
        //}
        public void AsyncRobotmovement()
        {

        }
        public void RotateWrist3(Pose currentPose)
        {
            Matrix3D mat = currentPose.GetRotation().ToRotationMatrix();
            // flange rotation relative to base
            Matrix3D tcp = Quaternion.FromEuler(new Vector3D(0, 0, 0)).ToRotationMatrix();
            // tcp totation relative to flange
            Matrix3D final = mat.Multiply(tcp);
            // rotation of tcp relative to base 
            Vector3D vec = RotationTransform.ToEuler(final);
            Pose newPose = new Pose(currentPose.GetPosition().x, currentPose.GetPosition().y,
                currentPose.GetPosition().z, vec.x, vec.y, vec.z);
            mRobot.MoveLocation(newPose);
        }
        
    }
}
