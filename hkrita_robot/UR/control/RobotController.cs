using hkrita_robot.Extension;
using hkrita_robot.Maths;
using hkrita_robot.Network;
using hkrita_robot.Network.script;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace hkrita_robot.UR.control
{
    public class RobotController : IRobotInterface, ICloneable
    {
        private double robot_movel_acc;
        private double robot_movel_velo;
        private double robot_movej_acc;
        private double robot_movej_velo;
        public double MOVEL_ACCELERATION { get => robot_movel_acc; set => robot_movel_acc = 0.5; }
        public double MOVEL_VELOCITY { get => robot_movel_velo; set => robot_movel_velo = 1.2; }
        public double MOVEJ_ACCELERATION { get => robot_movej_acc; set => robot_movej_acc = 0.3; }
        public double MOVEJ_VELOCITY { get => robot_movej_velo; set => robot_movej_velo = 1.2; }

        private readonly string mRobotAddress;
        private RobotSystem mRobot;
        private string mScript;
        public RobotController(string robotAddress)
        {
            mRobotAddress = robotAddress;
            mRobot = new RobotSystem(mRobotAddress);
           
        }


        public void MoveLocation(Pose newLocation, double acceleration, double speed)
        {
            Console.WriteLine("Moving robot with location {0}, acc = {1}, speed = {2}",
                newLocation, acceleration, speed);
            IAbstractScript script = new MoveScript(newLocation, MoveScript.Type.L, acceleration, speed);
            mScript = script.GetScript() + "\n";
            SubmitScript(mScript);
        }


        public void SetTCP(Pose tcpOffset)
        {
            Console.WriteLine("Setting tcp offset for robot {0}", tcpOffset);
            IAbstractScript script = new SetTCPScript(tcpOffset);
            //access this variable in callback 
            mScript = script.GetScript();
            SubmitScript(mScript);
        }

        public void SubmitScript(Action<object> action)
        {
            ActionHelper.SetAction(action, mScript);
        }

        public void SubmitScript(string script)
        {
            mRobot.Connect(mScript);
            Thread.Sleep(3000);
            mRobot.Close();
        }


        public Pose GetRobotLocation()
        {
            Console.WriteLine("Getting robot location");
            Pose pose = mRobot.GetData().GetRobotPose().Get();
            Console.WriteLine("robot location retrieved : {0}", pose);
            return pose;
        }

        public Pose GetTcp()
        {
            Console.WriteLine("Request to get tcp offset action");
            Pose pose = mRobot.GetData().GetTCPPose().Get();
            if (pose == null) return null;
            Console.WriteLine("Request to get tcp offset action {0} is successfully", pose);
            return pose;
        }

        public void MoveJoint(SixJointAngles newAngles, double acceleration, double speed) 
        {

        }

        public SixJointAngles GetRobotJointAngle() { return null; }
        public void Close() { mRobot.Close(); }
        public object Clone()
        {
            throw new NotImplementedException();
        }
    }
}
