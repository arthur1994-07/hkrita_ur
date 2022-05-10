using hkrita_robot.Maths;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hkrita_robot.Recorder
{
    public class RecordedData
    {


        [Serializable]
        public class BaseData
        {
            public static string typeName;
        }

        [Serializable]
        public class SetTcpData : BaseData
        {
            public static readonly string K_Format = "set-tcp";
            public Pose tcp;
            public SetTcpData() { typeName = K_Format; }
        }

        [Serializable]
        public class GetTcpData : BaseData
        {
            public static readonly string K_Format = "get-tcp";
            public Pose tcp;
            public GetTcpData() { typeName = K_Format; }
        }

        [Serializable]
        public class MovePoseData : BaseData
        {
            public static readonly string K_Format = "move-pose";

            public Pose pose;
            public double acceleration;
            public double speed;
            public MovePoseData() { typeName = K_Format; }
        }

        [Serializable]
        public class GetJointData : BaseData
        {
            public static readonly string K_Format = "get-joint";
            public SixJointAngles joint;
            public GetJointData() { typeName = K_Format; }
        }

        [Serializable]
        public class RunScriptData : BaseData
        {
            public static readonly String K_Format = "run-script";
            public String script;
            public RunScriptData() { typeName = K_Format; } 
        }
    }
}
