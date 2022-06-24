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
    //  limitation of robotController : it requires to initialise a new object when the user retreieves stream data from the UR such as joint and pose data
    public class RobotController : IRobotInterface, ICloneable
    {
        private readonly double robot_movel_acc = 0.5;
        private readonly double robot_movel_velo = 0.8;
        private readonly double robot_movej_acc = (Math.PI / 180) * 80;
        private readonly double robot_movej_velo = (Math.PI / 180) * 60;


        private RobotSystem mRobot;
        private string mScript;

        public RobotController()
        {
            mRobot = new RobotSystem();
        }

        public RobotController(string address)
        {
            mRobot = new RobotSystem(address);
        }

        public void MoveJoint(SixJointAngles newAngles)
        {
            MoveJoint(newAngles, robot_movej_acc, robot_movej_velo);
        }
        public void MoveJoint(SixJointAngles newAngles, double acceleration, double speed)
        {
            IAbstractScript script = new JointAngleScript(newAngles, acceleration, speed);
            mScript = script.GetScript() + "\n";
            SubmitScript(mScript); 
        }
        public void MoveLocation(Pose newLocation)
        {
            MoveLocation(newLocation, robot_movel_acc, robot_movel_velo);
        }

        public void MoveLocation(Pose newLocation, double acceleration, double speed)
        {
            IAbstractScript script = new MoveScript(newLocation, MoveScript.Type.L, acceleration, speed);
            mScript = script.GetScript() + "\n";
            SubmitScript(mScript);
        }

        public void Stop()
        {
            IAbstractScript script = new StopScript();
            mScript = script.GetScript() + "\n";
            SubmitScript(mScript);
        }

        public void SubmitScript(string script)
        {
            mRobot.SendScript(mScript);
            Thread.Sleep(3000);
            Close();
        }

        public Pose GetRobotLocation()
        {
            mRobot.Connect();
            Pose pose = mRobot.GetData().GetRobotPose().Get();
            Console.WriteLine("robot location retrieved : {0}", pose);
            return pose;
        }

        public SixJointAngles GetRobotJointAngle()
        {
            mRobot.Connect();
            SixJointAngles jointAngles = mRobot.GetData().GetJointAngles().Get();
            return jointAngles.ToAngles(); 
        }
        public void SetTCP(Pose tcpOffset)
        {
            Console.WriteLine("Setting tcp offset for robot {0}", tcpOffset);
            mRobot.GetData().GetTCPPose().Set(tcpOffset);
            IAbstractScript script = new SetTCPScript(tcpOffset);
            mScript = script.GetScript();
            SubmitScript(mScript);
        }  

        public Pose GetTcp()
        {
            Pose pose = mRobot.GetData().GetTCPPose().Get();
            if (pose == null) return null;
            return pose;
        }

        public void SubmitScript(Action<object> action)
        {
            if (mScript == null)
            {
                Console.WriteLine("null");
                return;
            }
            ActionHelper.SetAction(action, mScript);
        }

        
        public void Close() { mRobot.Close(); }
        public object Clone()
        {
            throw new NotImplementedException();
        }
    }
}
