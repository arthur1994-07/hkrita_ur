using hkrita_robot.Extension;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hkrita_robot.Maths
{
    public class Pose : ICloneable
    {
        public enum Direction
        {
            X,
            Y,
            Z
        }

        public string defaultPattern = "#.0000000000";
        private readonly double[] mPose = new double[6];
        
        // constructors
        public Pose() { }
        public Pose(double x, double y, double z, double rx, double ry, double rz) { Set(x, y, z, rx, ry, rz); }
        public Pose(Pose pose) { Set(pose); }
        public Pose(Coordinate3D translate, Quaternion rotation) { Set(translate, rotation); }
        public Pose(Coordinate3D translate, Matrix3D rotation) { Set(translate, rotation); }

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
        public Pose Set(Coordinate3D translate, Matrix3D rotation) { return Set(translate, Quaternion.FromRotationMatrix(rotation)); }
        public Pose Set(Coordinate3D translate, Quaternion rotation)
        {
            mPose[0] = translate.x;
            mPose[1] = translate.y;
            mPose[2] = translate.z;
            double p = rotation.ToAxisAngleMagnitude();
            Vector3D dir = rotation.ToAxisAngleDirection();
            mPose[3] = dir.x * p;
            mPose[4] = dir.y * p;
            mPose[5] = dir.z * p;
            return this;
        }
     
        public double[] GetPose()
        {
            double[] newArray = new double[mPose.Length];
            mPose.CopyTo(newArray,0);
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

        public Matrix4D GetTransformation()
        {
            return AffineTransform3D.Set(new Vector3D().Set(GetPosition()) , GetRotation());
        }

        public Pose MoveAlong(double distance, Direction dir)
        {
            Matrix4D matrix = GetTransformation();
            Point3D vec = null;
            switch (dir)
            {
                case Direction.X: vec = new Point3D(1, 0, 0); break;
                case Direction.Y: vec = new Point3D(0, 1, 0); break;
                case Direction.Z: vec = new Point3D(0, 0, 1); break;
                default: break;
            }
            return AffineTransform3D.ToPose(matrix.Multiply(AffineTransform3D.Set(vec.Multiply(distance))));
        }


        public override bool Equals(Object o)
        {
            if (this == o) return true;
            if (!(o.GetType().IsInstanceOfType(this))) return false;
            return EqualsHelper.Equals(mPose, ((Pose) o).mPose);
        }

        public override string ToString()
        {
            return ToString(false);
        }

        public string ToString(Boolean milliformat)
        {
            double unit = milliformat ? 1000 : 1;
            return StringHelper.Format("p{6}[{0}, {1}, {2}, {3}, {4}, {5}] ",  // {6} = miliformat  , {1-5} = mPose elements
                (mPose[0] * unit).ToString(defaultPattern),
                (mPose[1] * unit).ToString(defaultPattern),
                (mPose[2] * unit).ToString(defaultPattern),
                (mPose[3] * unit).ToString(defaultPattern), (mPose[4] * unit).ToString(defaultPattern), (mPose[5] * unit).ToString(defaultPattern),
                milliformat ? "m" : ""
                );
        }

        public static Pose ToPose(string script)
        {
            string[] str = StringHelper.FormatPoseString(script);
            double[] newPose = new double[str.Length];

            for (int i = 0; i < str.Length; i++)
            {
                newPose[i] = double.Parse(str[i]);
            }
            return new Pose(newPose[0], newPose[1], newPose[2], newPose[3], newPose[4], newPose[5]);
        }


        public object Clone()
        {
            return new Pose(this);
        }
    }
}
