using hkrita_robot.Extension;
using hkrita_robot.Maths;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hkrita_robot.Network.script
{
    public class SetTCPScript : IAbstractScript
    {
        private readonly Pose mTcpPose = new Pose();

        public SetTCPScript(Pose tcpPose)
        {
            Set(tcpPose);
        }

        public void Set(Pose tcpPose) { mTcpPose.Set(tcpPose); }

        public string GetScript()
        {
            return StringHelper.Format("set_tcp({0})", mTcpPose) + "\n";
        }
    }
}
