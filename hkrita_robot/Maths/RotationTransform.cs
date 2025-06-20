using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hkrita_robot.Maths
{
    //    This class will handle for transformation from
    //    eular angle <-> rotation matrix   (Roll-Pitch-Yaw) convention (http://planning.cs.uiuc.edu/node102.html)
    //    ZYX in Tait-Bryan angles
    //    axis angele <-> rotation matrix
    public class RotationTransform
    {
        public static Matrix3D FromEuler(Vector3D angle)
        {
            double cx = Math.Cos(angle.x);
            double sx = Math.Sin(angle.x);
            double cy = Math.Cos(angle.y);
            double sy = Math.Sin(angle.y);
            double cz = Math.Cos(angle.z);
            double sz = Math.Sin(angle.z);

            return new Matrix3D(cy * cz, sx * sy * cz - cx * sz, cx * sy * cz + sx * sz,
                 cy * sz, sx * sy * sz + cx * cz, cx * sy * sz - sx * cz,
                -sy, sx * cy, cx * cy);
        }

        public static Vector3D ToEuler(Matrix3D rotation)
        {
            Quaternion q = Quaternion.FromRotationMatrix(rotation);
            return q.ToEuler();
        }
        public static Matrix3D Normalize(Matrix3D matrix)
        {
            double[] raw = matrix.GetRaw();
            double a00 = raw[0];
            double a10 = raw[1];
            double a20 = raw[2];
            double a01 = raw[3];
            double a11 = raw[4];
            double a21 = raw[5];
            double a02 = raw[6];
            double a12 = raw[7];
            double a22 = raw[8];

            double r0 = Math.Sqrt(a00 * a00 + a10 * a10 + a20 * a20);
            double r1 = Math.Sqrt(a01 * a01 + a11 * a11 + a21 * a21);
            double r2 = Math.Sqrt(a02 * a02 + a12 * a12 + a22 * a22);

            return new Matrix3D
                        (a00 / r0, a01 / r1, a02 / r2,
                          a10 / r0, a11 / r1, a12 / r2,
                          a20 / r0, a21 / r1, a22 / r2);
        }
        public static Matrix3D GetInverse(Matrix3D matrix)
        {
            return matrix.Transpose();
        }
    }
}
