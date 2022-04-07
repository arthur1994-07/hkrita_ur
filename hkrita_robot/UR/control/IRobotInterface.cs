using hkrita_robot.Maths;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hkrita_robot.UR.control
{
    public interface IRobotInterface
    {
        double MOVEL_ACCELERATION { get; set; }
        double MOVEL_VELOCITY { get; set; }
        double MOVEJ_ACCELERATION { get; set; }
        double MOVEJ_VELOCITY { get; set; }


        void MoveLocation(Pose newLocation, double acceleration, double speed);
        void MoveJoint();
        void SetTCP(Pose tcpOffset);
        void SubmitScript(String script);
        void SubmitScript();

    }
}
