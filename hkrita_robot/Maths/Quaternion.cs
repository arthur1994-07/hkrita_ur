using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hkrita_robot.Maths
{
    public class Quaternion
    {
        public double x;
        public double y;
        public double z;
        public double w;

        public Quaternion() { Set(0, 0, 0, 1); }
        public Quaternion(double x, double y, double z, double w) { Set(x, y, z, w); }

        public Quaternion(Quaternion quat) { Set(quat); }
        public Quaternion Set(double x, double y, double z, double w)
        {
            this.x = x;
            this.y = y;
            this.z = z;
            this.w = w;
            return this;
        }
        public Quaternion Set(Quaternion quat)
        {
            this.x = quat.x;
            this.y = quat.y;
            this.z = quat.z;
            this.w = quat.w;
            return this;
        }
        public double Magnitude() { return Math.Sqrt(x * x + y * y + z * z + w * w); }
        public Quaternion Normalize() { return new Quaternion(this).AssignNormalize(); }

        public Quaternion AssignNormalize()
        {
            double normalize = Magnitude();
            x /= normalize;
            y /= normalize;
            z /= normalize;
            w /= normalize;
            return this;
        }
        public Quaternion Inverse()
        {
            double d = w * w + x * x + y * y + z * z;
            return new Quaternion(w / d, -x / d, -y / d, -z / d);
        }

        public Vector3D ToEuler()
        {
            //https://en.wikipedia.org/wiki/Conversion_between_quaternions_and_Euler_angles
            double xx = 2 * (this.w * this.x + this.y * this.z);
            double xy = 1 - 2 * UtilityHelper.Square(this.x) - 2 * UtilityHelper.Square(this.y);
            double y = 2 * (this.w * this.y - this.z * this.x);
            double zx = 2 * (this.w * this.z + this.x * this.y);
            double zy = 1 - 2 * UtilityHelper.Square(this.y) - 2 * UtilityHelper.Square(this.z);
            return new Vector3D(Math.Atan2(xx, xy), Math.Asin(y), Math.Atan2(zx, zy));
        }
        public Matrix3D ToRotationMatrix()
        {
            //https://en.wikipedia.org/wiki/Quaternions_and_spatial_rotation
            return new Matrix3D(
                    1 - 2 * y * y - 2 * z * z, 2 * (x * y - z * w), 2 * (x * z + y * w),
                    2 * (x * y + z * w), 1 - 2 * x * x - 2 * z * z, 2 * (y * z - x * w),
                    2 * (x * z - y * w), 2 * (y * z + x * w), 1 - 2 * x * x - 2 * y * y);
        }
        public double ToAxisAngleMagnitude()
        {
            double angle = Math.Acos(w / Magnitude()) * 2;
            return angle > Math.PI ? angle - 2 * Math.PI : angle;
        }
        public Vector3D ToAxisAngleDirection() { return new Vector3D(x, y, z).AssignNormalize(); }

        public static Quaternion FromEuler(Vector3D angle)
        {
            double cx = Math.Cos(angle.x / 2);
            double sx = Math.Sin(angle.x / 2);
            double cy = Math.Cos(angle.y / 2);
            double sy = Math.Sin(angle.y / 2);
            double cz = Math.Cos(angle.z / 2);
            double sz = Math.Sin(angle.z / 2);
            return new Quaternion(
                    sx * cy * cz - cx * sy * sz,
                    cx * sy * cz + sx * cy * sz,
                    cx * cy * sz - sx * sy * cz,
                    cx * cy * cz + sx * sy * sz).AssignNormalize();
        }

        public static Quaternion FromRotationMatrix(Matrix3D matrix)
        {
            // http://www.iri.upc.edu/files/scidoc/2068-Accurate-Computation-of-Quaternions-from-Rotation-Matrices.pdf
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

            double f1 = 1 + a00 + a11 + a22;
            double f2 = 1 + a00 - a11 - a22;
            double f3 = 1 - a00 + a11 - a22;
            double f4 = 1 - a00 - a11 + a22;

            if (f1 >= f2 && f1 >= f3 && f3 >= f4)
            {
                double r1 = Math.Sqrt(f1);
                return new Quaternion(
                        0.5 * (a21 - a12) / r1,
                        0.5 * (a02 - a20) / r1,
                        0.5 * (a10 - a01) / r1,
                        0.5 * r1).AssignNormalize();
            }
            if (f2 >= f3 && f2 >= f4)
            {
                double r2 = Math.Sqrt(f2);
                double sign2 = UtilityHelper.Sign(a21 - a12);
                return new Quaternion(
                        sign2 * 0.5 * r2,
                        sign2 * 0.5 * (a01 + a10) / r2,
                        sign2 * 0.5 * (a20 + a02) / r2,
                        sign2 * 0.5 * (a21 - a12) / r2).AssignNormalize();
            }
            if (f3 >= f4)
            {
                double r3 = Math.Sqrt(f3);
                double sign3 = UtilityHelper.Sign(a02 - a20);
                return new Quaternion(
                        sign3 * 0.5 * (a01 + a10) / r3,
                        sign3 * 0.5 * r3,
                        sign3 * 0.5 * (a12 + a21) / r3,
                        sign3 * 0.5 * (a02 - a20) / r3).AssignNormalize();
            }
            double r4 = Math.Sqrt(f4);
            double sign4 = UtilityHelper.Sign(a10 - a01);
            return new Quaternion(
                    sign4 * 0.5 * (a20 + a02) / r4,
                    sign4 * 0.5 * (a21 + a12) / r4,
                    sign4 * 0.5 * r4,
                    sign4 * 0.5 * (a10 - a01) / r4).AssignNormalize();
        }

        public static Quaternion FromAxisAngle(Vector3D vector, double angle)
        {
            if (Math.Abs(angle) < UtilityHelper.DOUBLE_EPSILON) return new Quaternion();
            double ct = Math.Cos(angle / 2);
            double st = Math.Sin(angle / 2);
            return new Quaternion(st * vector.x, st * vector.y, st * vector.z, ct).AssignNormalize();
        }

    }
}
