using hkrita_robot.Maths;
using hkrita_robot.UR.accessor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hkrita_robot.UR
{
    public interface IRobotData
    {
        ReadAttribute<Pose> GetRobotPose();
        ReadAttribute<Pose> GetTCPPose();
        ReadAttribute<bool> GetIsProgramRunning();
    }
}
