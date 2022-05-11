using hkrita_robot.Extension;
using hkrita_robot.Maths;
using hkrita_robot.Network.script;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
        private Action<string> mAction;
        public RobotController(string robotAddress)
        {
            mRobotAddress = robotAddress;
            mRobot = new RobotSystem(mRobotAddress);
           
        }

        public void Connect()
        {
            Console.WriteLine("Connect to robot with address {0} ", mRobotAddress);
            mRobot.Connect();
        }


        public void MoveLocation(Pose newLocation, double acceleration, double speed)
        {
            Console.WriteLine("Moving robot with location {0}, acc = {1}, speed = {2}",
                newLocation, acceleration, speed);
            IAbstractScript script = new MoveScript(newLocation, MoveScript.Type.L, acceleration, speed);
            string moveScript = script.GetScript();
            Console.WriteLine("");

        }


        public void SetTCP(Pose tcpOffset)
        {
            Console.WriteLine("Setting tcp offset for robot {0}", tcpOffset);
            IAbstractScript script = new SetTCPScript(tcpOffset);
            string setScript = script.GetScript();
        }

        public Action<string> SubmitScript()
        {
            // submit script and connect client to run script function as one single thread
            return (s) =>
            {
                mRobot.Connect();
                Console.WriteLine(s);
            };
        }

        public RobotController SetAction(Action<string> action, string script)
        {
            action.Invoke(script);
            return this;
        }

        public void TestAction()
        {
            Action<int, int> val = (x, y) =>
            {
                Console.WriteLine(x + y);
            };
            val(10, 20);
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

        public SixJointAngles GetRobotJointAngle() { return null; }
        public void MoveJoint() {}

        public object Clone()
        {
            throw new NotImplementedException();
        }
    }
}
