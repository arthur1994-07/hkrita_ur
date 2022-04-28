using hkrita_robot.Extension;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hkrita_robot.Maths
{
    public class Pose : ICloneable
    {
        private readonly double[] mPose = new double[6];

        // constructors
        public Pose() { }
        public Pose(double x, double y, double z, double rx, double ry, double rz) { Set(x, y, z, rx, ry, rz); }
        public Pose(Pose pose) { Set(pose); }


        //methods
        public Pose Set(double x, double y, double z, double rx, double ry, double rz)
        {
            mPose[0] = x;
            mPose[1] = y;
            mPose[2] = z;
            mPose[3] = rx;
            mPose[4] = ry;
            mPose[5] = rz;
            return this;
        }

        public void Set(Pose pose) { Array.Copy(pose.mPose, 0, mPose, 0, mPose.Length); }
        public double[] GetPose()
        {
            double[] newArray = new double[mPose.Length];
            mPose.CopyTo(newArray, mPose.Length);
            return newArray;
        }

        public Point3D GetPosition() { return new Point3D(mPose[0], mPose[1], mPose[2]); }
        public Vector3D GetAxisAngleRepresentation() { return new Vector3D(mPose[3], mPose[4], mPose[5]); }
        public Quaternion GetRotation()
        {
            Vector3D value = new Vector3D(mPose[3], mPose[4], mPose[5]);
            double angle = value.Magnitude();
            return Quaternion.FromAxisAngle(value.AssignNormalize(), angle);
        }


        public override bool Equals(Object o)
        {
            if (this == o) return true;
            if (!(o.GetType().IsInstanceOfType(this))) return false;
            return EqualsHelper.Equals(mPose, ((Pose) o).mPose);
        }


        public object Clone()
        {
            throw new NotImplementedException();
        }
    }
}
