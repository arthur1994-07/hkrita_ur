using hkrita_robot.API;
using hkrita_robot.Extension;
using hkrita_robot.Maths;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hkrita_robot.Network.script
{
    public class JointAngleScript : IAbstractScript
    {
        private readonly SixJointAngles mJoints;
        private readonly double mAcceleration;
        private readonly double mSpeed;

        private static readonly string mFormatter = "#.0000";


        //public JointAngleScript(SixJointAngles joints) { this(joints, 0, 0); }
        public JointAngleScript(SixJointAngles joints, double acc, double speed)
        {
            mJoints = joints;
            mAcceleration = acc;
            mSpeed = speed;
        }
         
        public static String CreateURScript(SixJointAngles jointAngles, double acceleration, double speed)
        {
            double[] raw = jointAngles.GetRaw();
            string jointAngleStr = StringHelper.Format("[{0}, {1}, {2}, {3}, {4}, {5}]", 
                raw[0].ToString(mFormatter), raw[1].ToString(mFormatter), raw[2].ToString(mFormatter),
                raw[3].ToString(mFormatter), raw[4].ToString(mFormatter), raw[5].ToString(mFormatter));
            string accelerationStr = acceleration < ConstantsParameter.K_numerical_epsilon ? "" :
                StringHelper.Format(", a={0}", acceleration);
            string speedStr = speed < ConstantsParameter.K_numerical_epsilon ? "" :
                StringHelper.Format(", v={0]}", speed);
            return StringHelper.Format("movej({0}{1}{2}", jointAngleStr, accelerationStr, speedStr);
        }

   
        public string GetScript()
        {
           return CreateURScript(mJoints, mAcceleration, mSpeed);
        }
    }
}
