using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hkrita_robot.Maths
{
    public class Coordinate3D
    {
        public double x;
        public double y;
        public double z;

        protected void LocalSet(double x, double y, double z)
        {
            this.x = x;
            this.y = y;
            this.z = z;
        }

        protected void LocalSet(double[] list)
        {
            x = list != null & list.Length > 0 ? list[0] : 0.0;
            y = list != null && list.Length > 1 ? list[1] : 0.0;
            z = list != null && list.Length > 2 ? list[2] : 0.0;
        }

        protected void LocalSet(Coordinate3D point)
        {
            x = point.x;
            y = point.y;
            z = point.z;
        }

        protected void LocalAdd(Coordinate3D point)
        {
            x += point.x;
            y += point.y;
            z += point.z;
        }

        protected void LocalSubtract(Coordinate3D point)
        {
            x -= point.x;
            y -= point.y;
            z -= point.z;
        }

        protected void LocalMultiply(Coordinate3D point)
        {
            x *= point.x;
            y *= point.y;
            z *= point.z;
        }
        protected void LocalMultiply(double factor)
        {
            x *= factor;
            y *= factor;
            z *= factor;
        }

        protected void LocalDivide(Coordinate3D point)
        {
            x /= point.x;
            y /= point.y;
            z /= point.z;
        }

        protected void LocalDivide(double factor)
        {
            x /= factor;
            y /= factor;
            z /= factor;
        }

        protected void LocalMax(Coordinate3D point)
        {
            x = Math.Max(x, point.x);
            y = Math.Max(y, point.y);
            z = Math.Max(z, point.z);
        }

        protected void LocalMin(Coordinate3D point)
        {
            x = Math.Min(x, point.x);
            y = Math.Min(y, point.y);
            z = Math.Min(z, point.z);
        }

        public double Sum()
        {
            return x + y + z;
        }

        public static double FindDistance(double x1, double y1, double z1, double x2, double y2, double z2)
        {
            double x = x2 - x1;
            double y = y2 - y1;
            double z = z2 - z1;
            return x * x + y * y + z * z;
        }

        public static double FindDistance(Coordinate3D point1, Coordinate3D point2)
        {
            return FindDistance(point1.x, point1.y, point1.z, point2.x, point2.y, point2.z);
        }

        public static double Distance(double x1, double y1, double z1, double x2, double y2, double z2)
        {
            return Math.Sqrt(FindDistance(x1, y1, z1, x2, y2, z2));
        }

        public static double Distance(Coordinate3D point1, Coordinate3D point2)
        {
            return Distance(point1.x, point1.y, point1.z, point2.x, point2.y, point2.z);
        }
    }
}
