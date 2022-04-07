using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hkrita_robot.Maths
{
    public class Vector3D : Coordinate3D, ICloneable
    {
        public Vector3D() { }
        public Vector3D(double x, double y, double z)
        {
            Set(x, y, z);
        }
        public Vector3D(double[] vector) { Set(vector); }
        public Vector3D(Vector3D vector) { Set(vector); }
        public Vector3D(Point3D point) { Set(point); }

        public Vector3D Set(double x, double y, double z)
        {
            LocalSet(x, y, z);
            return this;
        }

        public Vector3D Set(double[] vector)
        {
            LocalSet(vector);
            return this;

        }
        public Vector3D Set(Coordinate3D point)
        {
            LocalSet(point);
            return this;
        }

        public Point3D ToPoint() { return new Point3D(x, y, z); }

        public double Magnitude() { return Distance(0, 0, 0, x, y, z); }

        public Vector3D Add(Coordinate3D coordinate) { return new Vector3D(this).AssignAdd(coordinate); }

        public Vector3D AssignAdd(Coordinate3D coordinate)
        {
            LocalAdd(coordinate);
            return this;
        }

        public Vector3D Subtract(Coordinate3D coordinate) { return new Vector3D(this).AssignSubtract(coordinate); }

        public Vector3D AssignSubtract(Coordinate3D coordinate)
        {
            LocalSubtract(coordinate);
            return this;
        }

        public Vector3D Multiply(double factor) { return new Vector3D(this).AssignMultiply(factor); }
        public Vector3D AssignMultiply(double factor)
        {
            LocalMultiply(factor);
            return this;
        }

        public Vector3D Multiply(Coordinate3D coordinate) { return new Vector3D(this).AssignMultiply(coordinate); }
        public Vector3D AssignMultiply(Coordinate3D coordinate)
        {
            LocalMultiply(coordinate);
            return this;
        }
        public Vector3D Divide(double factor) { return new Vector3D(this).AssignDivide(factor); }
        public Vector3D AssignDivide(double factor)
        {
            LocalDivide(factor);
            return this;
        }
        public Vector3D Divide(Coordinate3D coorindate)
        {
            LocalDivide(coorindate);
            return this;
        }
        public Vector3D AssignDivide(Coordinate3D coordinate)
        {
            LocalDivide(coordinate);
            return this;
        }
        public Vector3D Min(Coordinate3D coordinate) { return new Vector3D(this).AssignMin(coordinate); }

        public Vector3D AssignMin(Coordinate3D coordinate)
        {
            LocalMin(coordinate);
            return this;
        }

        public Vector3D Max(Coordinate3D coordinate) { return new Vector3D(this).AssignMax(coordinate); }

        public Vector3D AssignMax(Coordinate3D coordinate)
        {
            LocalMax(coordinate);
            return this;
        }
        public Vector3D Normalize()
        {
            return new Vector3D(this).AssignNormalize();
        }
        public Vector3D AssignNormalize()
        {
            double norm = Magnitude();
            if (norm >= UtilityHelper.DOUBLE_EPSILON)
            {
                LocalDivide(norm);
            }
            return this;
        }

        public double Dot(Vector3D vector) { return x * vector.x + y * vector.y + z * vector.z; }

        public Vector3D Cross(Vector3D vector)
        {
            //  |   x,   y,   z |
            //  | v1x, v1y, v1z |
            //  | v2x, v2y, v2z |
            return new Vector3D(
                    this.y * vector.z - this.z * vector.y,
                    this.z * vector.x - this.x * vector.z,
                    this.x * vector.y - this.y * vector.x
            );
        }
        public Vector3D GetOrthogonal()
        {
            // C# equvalent to Java IntStream
            Vector3D[] vec = Enumerable.Range(0, 3).Select(s => Matrix3D.Identity().GetColumn(s).Cross(this)).ToArray();
            //Vector3D[] vec = IntStream.range(0, 3).mapToObj(s->Matrix3D.identity().getColumn(s).cross(this)).toArray(Vector3D[]::new);
            //IComparer<Vector3D> comparer;
            //foreach (Vector3D v in vec)
            //{

            //}
            //return Arrays.stream(vec).max(Comparator.comparingDouble(Vector3D::magnitude)).orElseThrow(RuntimeException::new).normalize();
            return this;
        }

        public double AngleBetween(Vector3D vector)
        {
            return Math.Acos(this.Dot(vector) / (this.Magnitude() * vector.Magnitude()));
        }

        public static Vector3D FromPoints(Point3D from, Point3D to)
        {
            return new Vector3D().Set(to).AssignSubtract(from);
        }



        public object Clone() { return new Vector3D(this); }
        public override Boolean Equals(Object o)
        {
            if (o == this) return true;
            if (!(o.GetType().IsInstanceOfType((Vector3D)o))) return false;
            return Distance(this, (Vector3D)o) < UtilityHelper.DOUBLE_EPSILON;
        }

        public override int GetHashCode()
        {
            int hashCode = 373119288;
            hashCode = hashCode * -1521134295 + x.GetHashCode();
            hashCode = hashCode * -1521134295 + y.GetHashCode();
            hashCode = hashCode * -1521134295 + z.GetHashCode();
            return hashCode;
        }
    }
}
