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

        public double GetElement(int row, int col) { return mData[col * 4 + row]; }



        public double[] GetRaw() 
        {
            double[] newArray = new double[16];
            Array.Copy(mData, newArray, mData.Length);
            return newArray;
        }

        public object Clone()
        {
            throw new NotImplementedException();
        }
    }
}
