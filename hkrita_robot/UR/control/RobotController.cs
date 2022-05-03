using hkrita_robot.Extension;
using hkrita_robot.Maths;
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





        public void MoveLocation(Pose newLocation, double acceleration, double speed)
        {
            StringHelper writer = new StringHelper();
            throw new NotImplementedException();
        }



        public void MoveJoint()
        {
        }



        public void SetTCP(Pose tcpOffset)
        {
        }

        public void SubmitScript(string script)
        {
        }

        public void SubmitScript()
        {
        }
        public object Clone()
        {
            throw new NotImplementedException();
        }

    }
}
