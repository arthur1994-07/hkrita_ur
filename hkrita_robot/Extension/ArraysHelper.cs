using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hkrita_robot.Extension
{
    public class ArraysHelper
    {
        public static int GetLength(int[] original) { return original == null ? 0 : original.Length; }
        public static int getLength(double[] original) { return original == null ? 0 : original.Length; }
        public static int GetLength<T>(T[] original) { return original == null ? 0 : original.Length; }

        //public static T[] Insert<T>(T[] original, T value, int pos, IntFunction creator)
        //{
        //    if (original == null)
        //    {
        //        T[] ret = creator.apply(1);
        //    }


        //    return null;
        //}
        public static void Fill(double[] a, double val)
        {
            for (int i = 0, len = a.Length; i < len; i++)
            {
                a[i] = val;
            }
        }

        public static void Fill(byte[] a)
        {
            for (int i = 0, len = a.Length; i < len; i++)
            {
                a[i] = 0;
            }
        }
        

        public static void Swap<T>(T[] original, int pos1, int pos2)
        {
            T tmp = original[pos1];
            original[pos1] = original[pos2];
            original[pos2] = tmp;
        }

        public static void Swap(double[] original, int pos1, int pos2)
        {
            double tmp = original[pos1];
            original[pos1] = original[pos2];
            original[pos2] = tmp;
        }

        public static void Swap(int[] original, int pos1, int pos2)
        {
            int tmp = original[pos1];
            original[pos1] = original[pos2];
            original[pos2] = tmp;
        }



    }
}
