using hkrita_robot.Extension;
using hkrita_robot.Maths;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using hkrita_robot.API;
namespace hkrita_robot.Network.script
{
    public class MoveScript : IAbstractScript
    {

        public static readonly double K_ACCEPTABLE_ROTATION_DIFF = 0.002;
        public static readonly double K_ACCEPTABLE_POSITION_DIFF = 0.004;

        public enum Type
        {
            l,
            j,
            p
        }

        public readonly static double[] preferred_speed = new double[] { 0.25, 0.2, 0.75, 0.1, 0.8, 2 };
        public readonly static double[] preferred_acceleration = new double[] { 1.2, 1, 1.5, 0.5, 1, 3 };
        public readonly int preferred_set = 3;
        public readonly static double blend_radius = 0.0;

        private readonly static string format_6 = "#.000000";
        private readonly static string format_4 = "#.0000";


        private string mScript = "";
        protected Pose mPose = new Pose();

        //public MoveScript(Pose pose) { this(pose, Type.l); }

        public MoveScript(Pose pose, Type type)
        {
            Set(pose, type, preferred_acceleration[preferred_set], preferred_speed[preferred_set],
                blend_radius);
        }






        public string getScript()
        {
            throw new NotImplementedException();
        }




        public void Set(Pose pose, Type type, double acceleration, double speed, double blendRadius)
        {
            mPose.Set(pose);
            double[] poseVec = pose.GetPose();
            string poseStr = StringHelper.Format("p[{0},{1},{2},{3},{4},{5}]",
                poseVec[0].ToString(format_6), poseVec[1].ToString(format_6), poseVec[2].ToString(format_6),
                poseVec[3].ToString(format_4), poseVec[4].ToString(format_4), poseVec[5].ToString(format_4));
            string accelerationString = acceleration < Constants.K_numerical_epsilon ? "" :
                StringHelper.Format("a = {0}", acceleration.ToString(format_4));
            string velocityString = speed < Constants.K_numerical_epsilon ? "" :
                StringHelper.Format("V = {0}", speed.ToString(format_4));

            if (type == Type.j)
            {
                mScript = StringHelper.Format("movej(get_inverse_kin{0}, get_actual_joint_positions()) {1}{2})",
                    poseStr, accelerationString, velocityString);
            }
            else
            {
                String addString = type != Type.p || blendRadius < Constants.K_Double_epsilon ? "" :
                    StringHelper.Format("r={0}", blendRadius.ToString(format_4));
                //mScript = StringHelper.Format("move{0}({1}{2}{3}{4}",
                //    )
            }
        }
    }
}
