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
        public double K_ROBOT_MOVEL_ACC { get => robot_movel_acc; set => robot_movel_acc = 0.5; }
        public double K_ROBOT_MOVEL_VEC { get => robot_movel_velo; set => robot_movel_velo = 1.2; }
        public double K_ROBOT_MOVEJ_ACC { get => robot_movej_acc; set => robot_movej_acc = 0.3; }
        public double K_ROBOT_MOVEJ_VEC { get => robot_movej_velo; set => robot_movej_velo = 1.2; }




        public void MoveLocation(Pose newLocation, double acceleration, double speed)
        {
            StringHelper writer = new StringHelper();
            throw new NotImplementedException();
        }



        public void MoveJoint()
        {
            throw new NotImplementedException();
        }



        public void SetTCP(Pose tcpOffset)
        {
            throw new NotImplementedException();
        }

        public void SubmitScript(string script)
        {
            throw new NotImplementedException();
        }

        public void SubmitScript()
        {
            throw new NotImplementedException();
        }
        public object Clone()
        {
            throw new NotImplementedException();
        }

    }
}
