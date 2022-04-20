using hkrita_robot.Maths;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hkrita_robot.UR.control
{
    public class WriteScript
    {

        RobotController mRobot;

        // movel(pose, acceleration, velocity, time, blend radius)

        

        public void test()
        {
            // calculation 
            Pose startPose = new Pose();
            Matrix3D rotMatrix = startPose.GetRotation().ToRotationMatrix();

            // write to script

        }
    }
}
