using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hkrita_robot.Maths
{
    public class AffineTransform3D
    {
        public static Matrix4D Set(Coordinate3D translate)
        {
            Matrix4D matrix = Matrix4D.Identity();
            matrix.SetElement(0, 3, translate.x);
            matrix.SetElement(1, 3, translate.y);
            matrix.SetElement(2, 3, translate.z);
            return matrix; 
        }

        public static Matrix4D Set(Quaternion rotation) { return Set(new Point3D(0, 0, 0), rotation); }
        public static Matrix4D Set(Matrix3D rotation) { return Set(new Point3D(0, 0, 0), rotation); }
        public static Matrix4D Set(Coordinate3D translate, Matrix3D rotation)
        {
            Matrix4D matrix = Matrix4D.Identity();
            matrix.SetElement(0, 0, rotation.GetElement(0, 0));
            matrix.SetElement(0, 1, rotation.GetElement(0, 1));
            matrix.SetElement(0, 2, rotation.GetElement(0, 2));
            matrix.SetElement(1, 0, rotation.GetElement(1, 0));
            matrix.SetElement(1, 1, rotation.GetElement(1, 1));
            matrix.SetElement(1, 2, rotation.GetElement(1, 2));
            matrix.SetElement(2, 0, rotation.GetElement(2, 0));
            matrix.SetElement(2, 1, rotation.GetElement(2, 1));
            matrix.SetElement(2, 2, rotation.GetElement(2, 2));
            matrix.SetElement(0, 3, translate.x);
            matrix.SetElement(1, 3, translate.y);
            matrix.SetElement(2, 3, translate.z);
            return matrix;
        }
        public static Matrix4D Set(Coordinate3D translate, Quaternion rotation)
        {
            return Set(translate, rotation.ToRotationMatrix());
        }

        public static Vector3D GetTranslate(Matrix4D matrix)
        {
            return new Vector3D(
                matrix.GetElement(0, 3),
                matrix.GetElement(1, 3),
                matrix.GetElement(2, 3));
        }

        public static Matrix3D GetRotation(Matrix4D matrix)
        {
            double a00 = matrix.GetElement(0, 0);
            double a01 = matrix.GetElement(0, 1);
            double a02 = matrix.GetElement(0, 2);
            double a10 = matrix.GetElement(1, 0);
            double a11 = matrix.GetElement(1, 1);
            double a12 = matrix.GetElement(1, 2);
            double a20 = matrix.GetElement(2, 0);
            double a21 = matrix.GetElement(2, 1);
            double a22 = matrix.GetElement(2, 2);

            double x = Math.Sqrt(a00 * a00 + a10 * a10 + a20 * a20);
            double y = Math.Sqrt(a01 * a01 + a11 * a11 + a21 * a21);
            double z = Math.Sqrt(a02 * a02 + a12 * a12 + a22 * a22);

            return new Matrix3D(
                a00 / x, a01 / y, a02 / z,
                a10 / x, a11 / y, a12 / z,
                a20 / x, a21 / y, a22 / z);
        }
        public static Pose ToPose(Matrix4D matrix)
        {
            return new Pose(GetTranslate(matrix), GetRotation(matrix));
        }

        

    }
}
