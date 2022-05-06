using hkrita_robot.Maths;
using hkrita_robot.UR;
using hkrita_robot.UR.accessor;
using LanguageExt.TypeClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace hkrita_robot.Network.ur.internalData
{
    public class InternalRobotData : IRobotData
    {

        public ReadAttribute<Pose> robotPose;
        public ReadAttribute<Pose> tcpPose;
        //public ReadAttribute<Boolean> isProgramRunning;

        //public ReadAttribute<Boolean> GetIsProgramRunning() {  return isProgramRunning; }  
        public ReadAttribute<Pose> GetRobotPose() { return robotPose; }
        public ReadAttribute<Pose> GetTCPPose() { return tcpPose; } 

        public InternalRobotData()
        {
            robotPose = new ReadAttribute<Pose>(typeof(Pose).GetTypeInfo(), null);
            tcpPose = new ReadAttribute<Pose>(typeof(Pose).GetTypeInfo(), null);
            //isProgramRunning = new ReadAttribute<Boolean>(typeof(Boolean).GetTypeInfo(), false);
        }
    }
}
