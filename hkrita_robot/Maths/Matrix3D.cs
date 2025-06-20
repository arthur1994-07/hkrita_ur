using hkrita_robot.Extension;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hkrita_robot.Maths
{
    /**
 * Construct a 3x3 matrix with the specified values.
 *
 * m00 - index  0 Value of element m[0,0].
 * m10 - index  1 Value of element m[1,0].
 * m20 - index  2 Value of element m[2,0].
 * m01 - index  3 Value of element m[0,1].
 * m11 - index  4 Value of element m[1,1].
 * m21 - index  5 Value of element m[2,1].
 * m02 - index  6 Value of element m[0,2].
 * m12 - index  7 Value of element m[1,2].
 * m22 - index  8 Value of element m[2,2].
 */
    public class Matrix3D : ICloneable
    {
        private readonly double[] mData = new double[9];
        public Matrix3D() { }
        public Matrix3D(Matrix3D matrix) { Set(matrix); }
        public Matrix3D(double m00, double m01, double m02,
                     double m10, double m11, double m12,
                     double m20, double m21, double m22)
        {
            Set(m00, m01, m02,
                m10, m11, m12,
                m20, m21, m22);
        }

        public Matrix3D Set(Matrix3D matrix)
        {
            Array.Copy(matrix.mData, 0, mData, 0, mData.Length);
            return this;
        }

        public Matrix3D Set(double m00, double m01, double m02,
                    double m10, double m11, double m12,
                    double m20, double m21, double m22)
        {
            mData[0] = m00; mData[3] = m01; mData[6] = m02;
            mData[1] = m10; mData[4] = m11; mData[7] = m12;
            mData[2] = m20; mData[5] = m21; mData[8] = m22;
            return this;
        }

        public Matrix3D SetColumn(int column, Coordinate3D data)
        {
            mData[column * 3 + 0] = data.x;
            mData[column * 3 + 1] = data.y;
            mData[column * 3 + 2] = data.z;
            return this;
        }

        public Matrix3D SetRow(int row, Coordinate3D data)
        {
            mData[0 * 3 + row] = data.x;
            mData[1 * 3 + row] = data.y;
            mData[2 * 3 + row] = data.z;
            return this;
        }
        public Vector3D GetColumn(int column) { return new Vector3D(mData[column * 3 + 0], mData[column * 3 + 1], mData[column * 3 + 2]); }
        public Vector3D GetRow(int row) { return new Vector3D(mData[0 * 3 + row], mData[1 * 3 + row], mData[2 * 3 + row]); }

        public Matrix3D SetIdentity()
        {
            ArraysHelper.Fill(mData, 0);
            mData[0] = mData[4] = mData[8] = 1.0;
            return this;
        }

        public Matrix3D SetElement(int row, int col, double value)
        {
            mData[col * 3 + row] = value;
            return this;
        }
        public double[] GetRaw()
        {
            double[] newArray = new double[9];
            Array.Copy(mData, newArray, mData.Length);
            return newArray;
        }

        public double GetElement(int row, int col)
        {
            return mData[col * 3 + row];
        }

        public Matrix3D Add(Matrix3D matrix)
        {
            Matrix3D mat = new Matrix3D();
            for (int i = 0; i < mData.Length; i++)
            {
                mat.mData[i] = mData[i] + matrix.mData[i];
            }
            return mat;
        }

        public Matrix3D AssignAdd(Matrix3D matrix)
        {
            for (int i = 0; i < mData.Length; i++)
            {
                mData[i] += matrix.mData[i];
            }
            return this;
        }

        public Matrix3D Subtract(Matrix3D matrix)
        {
            Matrix3D mat = new Matrix3D();
            for (int i = 0; i < mData.Length; i++)
            {
                mat.mData[i] = mData[i] - matrix.mData[i];
            }
            return mat;
        }

        public Matrix3D AssignSubtract(Matrix3D matrix)
        {
            for (int i = 0; i < mData.Length; i++)
            {
                mData[i] -= matrix.mData[i];
            }
            return this;
        }

        public Matrix3D Multiply(double factor)
        {
            Matrix3D ret = new Matrix3D();
            for (int i = 0; i < mData.Length; i++)
            {
                ret.mData[i] = mData[i] * factor;
            }
            return ret;
        }

        public Matrix3D AssignMultiply(double factor)
        {
            for (int i = 0; i < mData.Length; i++)
            {
                mData[i] *= mData[i] * factor;
            }
            return this;
        }
        public Matrix3D Multiply(Matrix3D matrix)
        {
            Matrix3D mat = new Matrix3D();
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    mat.mData[3 * i + j] = 0.0;
                    for (int k = 0; k < 3; k++)
                    {
                        mat.mData[3*i+j] += mData[k*3+j] * matrix.mData[i*3+k];
                    }
                }
            }
            return mat;
        }

        public Matrix3D AssignMultiply(Matrix3D matrix) { return Set(Multiply(matrix)); }

        public Point3D Multiply(Coordinate3D vec)
        {
            Point3D point = new Point3D();
            point.x = mData[0 * 3 + 0] * vec.x + mData[1 * 3 + 0] * vec.y + mData[2 * 3 + 0] * vec.z;
            point.y = mData[0 * 3 + 1] * vec.x + mData[1 * 3 + 1] * vec.y + mData[2 * 3 + 1] * vec.z;
            point.z = mData[0 * 3 + 2] * vec.x + mData[1 * 3 + 2] * vec.y + mData[2 * 3 + 2] * vec.z;
            return point;
        }

        public Matrix3D Transpose()
        {
            return new Matrix3D(this).AssignTranspose();
        }
        public Matrix3D AssignTranspose()
        {
            ArraysHelper.Swap(mData, 2, 6);
            ArraysHelper.Swap(mData, 5, 7);
            return this;
        }

        public static Matrix3D Identity() { return new Matrix3D().SetIdentity(); }
        public object Clone() { return new Matrix3D(this); }
        public String ToString()
        {
            return StringHelper.Format("\n{0} {1} {2}\n{3} {4} {5}\n{6} {7} {8}\n",
                mData[0], mData[3], mData[6],
                mData[1], mData[4], mData[7],
                mData[2], mData[5], mData[8]);
        }

        public Matrix3D Inverse() { return new InverseWorkspace(mData).Inverse(); }
        public InverseWorkspace GetInverseWorkspace() { return new InverseWorkspace(mData); }

        public class InverseWorkspace
        {
            private readonly double[] mResult = new double[9];
            private readonly double mDeterminant;

            public InverseWorkspace(double[] data)
            {
                // https://stackoverflow.com/questions/983999/simple-3x3-matrix-inverse-code-c
                // Inversion by Cramer's rule.  Code taken from an Intel publication
                //
                double[] tmp = new double[18];

                tmp[0] = data[4] * data[8];
                tmp[1] = data[5] * data[7];
                tmp[2] = data[1] * data[8];
                tmp[3] = data[7] * data[2];
                tmp[4] = data[1] * data[5];
                tmp[5] = data[4] * data[2];
                tmp[6] = data[3] * data[8];
                tmp[7] = data[6] * data[5];
                tmp[8] = data[0] * data[8];
                tmp[9] = data[2] * data[6];
                tmp[10] = data[0] * data[5];
                tmp[11] = data[2] * data[3];
                tmp[12] = data[3] * data[7];
                tmp[13] = data[6] * data[4];
                tmp[14] = data[0] * data[7];
                tmp[15] = data[1] * data[6];
                tmp[16] = data[0] * data[4];
                tmp[17] = data[1] * data[3];

                const int dimension = 3;

                // calculate first 8 elements (cofactors)
                mResult[0 * dimension + 0] += tmp[0];
                mResult[0 * dimension + 0] -= tmp[1];
                mResult[0 * dimension + 1] += tmp[7];
                mResult[0 * dimension + 1] -= tmp[6];
                mResult[0 * dimension + 2] += tmp[12];
                mResult[0 * dimension + 2] -= tmp[13];
                mResult[1 * dimension + 0] += tmp[3];
                mResult[1 * dimension + 0] -= tmp[2];
                mResult[1 * dimension + 1] += tmp[8];
                mResult[1 * dimension + 1] -= tmp[9];
                mResult[1 * dimension + 2] += tmp[15];
                mResult[1 * dimension + 2] -= tmp[14];
                mResult[2 * dimension + 0] += tmp[4];
                mResult[2 * dimension + 0] -= tmp[5];
                mResult[2 * dimension + 1] += tmp[11];
                mResult[2 * dimension + 1] -= tmp[10];
                mResult[2 * dimension + 2] += tmp[16];
                mResult[2 * dimension + 2] -= tmp[17];

                mDeterminant = data[0] * (tmp[0] - tmp[1]) - data[3] * (tmp[2] - tmp[3]) + data[6] * (tmp[4] - tmp[5]);
            }
            public Matrix3D Inverse()
            {
                double factor = 1.0f / mDeterminant;
                Matrix3D ret = new Matrix3D();

                const int dimension = 3;
                for (int i = 0; i < dimension; i++)
                {
                    for (int j = 0; j < dimension; j++)
                    {
                        ret.mData[j * dimension + i] = mResult[i * dimension + j] * factor;
                    }
                }
                return ret;
            }
        }
    }
}
