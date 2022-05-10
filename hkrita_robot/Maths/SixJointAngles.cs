using hkrita_robot.Recorder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hkrita_robot.Maths
{
    public class SixJointAngles : ICloneable
    {
        public double robotBase;
        public double shoulder;
        public double elbow;
        public double wrist1;
        public double wrist2;
        public double wrist3;

        public SixJointAngles(double robotbase, double shoulder, double elbow, double wrist1, double wrist2, double wrist3)
        {
            Set(robotbase, shoulder, elbow, wrist1, wrist2, wrist3);
        }


        public SixJointAngles Set(double robotBase, double shoulder, double elbow, double wrist1, double wrist2, double wrist3)
        {
            this.robotBase = robotBase;
            this.shoulder = shoulder;
            this.elbow = elbow;
            this.wrist1 = wrist1;
            this.wrist2 = wrist2;  
            this.wrist3 = wrist3;
            return this; 
        }

        public SixJointAngles Set(double[] joints)
        {
            return Set(joints.Length > 0 ? joints[0] : 0.0,
                joints.Length > 1 ? joints[1] : 0.0,
                joints.Length > 2 ? joints[2] : 0.0,
                joints.Length > 3 ? joints[3] : 0.0,
                joints.Length > 4 ? joints[4] : 0.0,
                joints.Length > 5 ? joints[5] : 0.0);
        }

        public SixJointAngles Set(SixJointAngles angles)
        {
            this.robotBase = angles.robotBase;
            this.shoulder = angles.shoulder;
            this.elbow = angles.elbow;
            this.wrist1 = angles.wrist1;
            this.wrist2 = angles.wrist2;
            this.wrist3 = angles.wrist3;
            return this;
        }

        public double[] GetRaw() { return new double[] { robotBase, shoulder, elbow, wrist1, wrist2, wrist3 }; }

        public object Clone()
        {
            return new SixJointAngles(robotBase, shoulder, elbow, wrist1, wrist2, wrist3);
        }
    }
}
