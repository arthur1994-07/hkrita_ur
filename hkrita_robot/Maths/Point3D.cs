using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hkrita_robot.Maths
{
    public class Point3D : Coordinate3D
    {
        public Point3D() { }
        public Point3D(double x, double y, double z) { Set(x, y, z); }
        public Point3D(double[] point) { LocalSet(point); }
        public Point3D(Point3D point) { LocalSet(point); }

        public Point3D Set(double x, double y, double z)
        {
            LocalSet(x, y, z);
            return this;
        }

        public Point3D Set(double[] list)
        {
            LocalSet(list);
            return this;
        }

        public Point3D Set(Coordinate3D point)
        {
            LocalSet(point);
            return this;
        }

        public Point3D Add(Coordinate3D point)
        {
            return new Point3D(this).AssignAdd(point);
        }

        public Point3D AssignAdd(Coordinate3D point)
        {
            LocalAdd(point);
            return this;
        }

        public Point3D Subtract(Coordinate3D point)
        {
            return new Point3D(this).AssignSubtract(point);
        }
        public Point3D AssignSubtract(Coordinate3D point)
        {
            LocalSubtract(point);
            return this;
        }

        public Point3D Multiply(double factor)
        {
            return new Point3D(this).AssignMultiply(factor);
        }

        public Point3D AssignMultiply(double factor)
        {
            LocalMultiply(factor);
            return this;
        }

        public Point3D Divide(double factor)
        {
            return new Point3D(this).AssignDivide(factor);
        }

        public Point3D AssignDivide(double factor)
        {
            LocalDivide(factor);
            return this;
        }

        public Point3D Min(Coordinate3D point)
        {
            return new Point3D(this).AssignMin(point);
        }

        public Point3D AssignMin(Coordinate3D point)
        {
            LocalMin(point);
            return this;
        }

        public Point3D Max(Coordinate3D point)
        {
            return new Point3D(this).AssignMax(point);
        }

        public Point3D AssignMax(Coordinate3D point)
        {
            LocalMax(point);
            return this;
        }

        public double FindDistance(double ax, double ay, double az)
        {
            return FindDistance(ax, ay, az, x, y, z);
        }
        public double FindDistance(Coordinate3D point)
        {
            return FindDistance(this, point);
        }

        public double Distance(double ax, double ay, double az)
        {
            return Distance(ax, ay, az, x, y, z);
        }
        public double Distance(Coordinate3D point)
        {
            return Distance(this, point);
        }
    }
}
