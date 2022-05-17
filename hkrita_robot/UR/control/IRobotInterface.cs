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

        void MoveLocation(Pose newLocation, double acceleration, double speed);
        void MoveJoint(SixJointAngles newAngle, double acceleration, double speed);
        void SetTCP(Pose tcpOffset);
        Pose GetTcp();
        Pose GetRobotLocation();
        SixJointAngles GetRobotJointAngle();
        //void MoveLocation(Pose newLocation) { MoveLocation(newLocation, 0.5, 0.5); }
    }
}
