using hkrita_robot.Extension;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hkrita_robot.Maths
{
    public class Matrix4D : ICloneable
    {
        private readonly double[] mData = new double[16];

        public Matrix4D() { }

        public Matrix4D(double[] matrix)
        {
            for (int i = 0; i < 16; i++)
            {
                mData[i] = matrix[i];
            }
        }

        public Matrix4D(double m00, double m01, double m02, double m03,
                            double m10, double m11, double m12, double m13,
                            double m20, double m21, double m22, double m23,
                            double m30, double m31, double m32, double m33)
        {
            Set(m00, m01, m02, m03,
                m10, m11, m12, m13,
                m20, m21, m22, m23,
                m30, m31, m32, m33);
        }

        public Matrix4D Set(double m00, double m01, double m02, double m03,
                            double m10, double m11, double m12, double m13,
                            double m20, double m21, double m22, double m23,
                            double m30, double m31, double m32, double m33)
        {
            mData[0] = m00; mData[4] = m01; mData[8] = m02; mData[12] = m03;
            mData[1] = m10; mData[5] = m11; mData[9] = m12; mData[13] = m13;
            mData[2] = m20; mData[6] = m21; mData[10] = m22; mData[14] = m23;
            mData[3] = m30; mData[7] = m31; mData[11] = m32; mData[15] = m33;
            return this;
        }

        public Matrix4D Set(Matrix4D matrix)
        {
            Array.Copy(matrix.mData, 0, mData, 0, mData.Length);
            return this;
        }

        public Matrix4D SetIdentity()
        {
            ArraysHelper.Fill(mData, 0);
            mData[0] = mData[5] = mData[10] = mData[15] = 1.0;
            return this;
           
        }
        public Matrix4D SetElement(int row, int col, double value)
        {
            mData[col * 4 + row] = value;
            return this;
        }
        public double GetElement(int row, int col) { return mData[col * 4 + row]; }
        public static Matrix4D Identity() { return new Matrix4D().SetIdentity(); }


        public double[] GetRaw() 
        {
            double[] newArray = new double[16];
            Array.Copy(mData, newArray, mData.Length);
            return newArray;
        }

        public Matrix4D Add(Matrix4D mat)
        {
            Matrix4D matrix = new Matrix4D();
            for (int i = 0; i < mData.Length; i++)
            {
                matrix.mData[i] = mData[i] + mat.mData[i];
            }
            return matrix;
        }

        public Matrix4D AssignAdd(Matrix4D mat)
        {
            for (int i = 0; i < mData.Length; i++)
            {
                mData[i] += mat.mData[i];
            }
            return this;
        }

        public Matrix4D Subtract(Matrix4D mat)
        {
            Matrix4D matrix = new Matrix4D();
            for (int i = 0; i < mData.Length; i++)
            {
                matrix.mData[i] = mData[i] - mat.mData[i];
            }
            return matrix;
        }

        public Matrix4D AssignSubtract(Matrix4D mat)
        {
            for (int i = 0; i < mData.Length; i++)
            {
                mData[i] -= mat.mData[i];
            }
            return this;
        }

        public Matrix4D Multiply(double factor)
        {
            Matrix4D ret = new Matrix4D();
            for (int i = 0; i < mData.Length; i++)
            {
                ret.mData[i] = mData[i] * factor;
            }
            return ret;
        }

        public Matrix4D AssignMultiply(double factor)
        {
            for (int i = 0; i < mData.Length; i++)
            {
                mData[i] *= mData[i] * factor;
            }
            return this;
        }

        public Matrix4D Multiply(Matrix4D mat)
        {
            Matrix4D matrix = new Matrix4D();
            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    matrix.mData[4 * i + j] = 0.0;
                    for (int k = 0; k < 4; k++)
                    {
                        matrix.mData[4 * i + j] += mData[k * 4 + j] * mat.mData[i * 4 + k];
                    }
                }
            }
            return matrix;
        }

        // last element as one for transformation use
        public Vector3D Multiply(Vector3D vec)
        {
            return InternalMultiply(vec);
        }

        // last element as one for transformation use
        public Point3D Multiply(Point3D vec)
        {
            return InternalMultiply(vec).ToPoint();
        }

        private Vector3D InternalMultiply(Coordinate3D coord)
        {
            double a0 = mData[0 * 4 + 0] * coord.x + mData[1 * 4 + 0] * coord.y +
                        mData[2 * 4 + 0] * coord.z + mData[3 * 4 + 0];
            double a1 = mData[0 * 4 + 1] * coord.x + mData[1 * 4 + 1] * coord.y +
                        mData[2 * 4 + 1] * coord.z + mData[3 * 4 + 1];
            double a2 = mData[0 * 4 + 2] * coord.x + mData[1 * 4 + 2] * coord.y +
                        mData[2 * 4 + 2] * coord.z + mData[3 * 4 + 2];
            return new Vector3D(a0, a1, a2);
        }

        public Matrix4D AssignMultiply(Matrix4D mat)
        {
            return Set(Multiply(mat));
        }




        public object Clone()
        {
            throw new NotImplementedException();
        }
    }
}
