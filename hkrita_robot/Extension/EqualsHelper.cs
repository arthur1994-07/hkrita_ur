using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using hkrita_robot.API;
namespace hkrita_robot.Extension
{
    public class EqualsHelper
    {

        public static bool Equals(int v1, int v2) {  return v1 == v2; }  
        public static bool Equals(double v1, double v2) { return Equals(v1, v2, Constants.K_numerical_epsilon); }

        public static bool Equals<T>(Task v1, Task v2)
        {
            if (v1 == v2) return true;
            return v1 != null && v1.Equals(v2);
        }

        public static bool Equals(int[] v1, int[] v2)
        {
            if (v1 == v2) return true;
            if (v1 == null || v2 == null) return false;
            if (v1.Length != v2.Length) return false;

            for (int i = 0; i < v1.Length; i++)
            {
                if (v1[i] != v2[i]) return false;
            }
            return true;
        }

        public static bool Equals(bool[] v1, bool[] v2)
        {
            if (v1 == v2) return true;
            if (v1 == null || v2 == null) return false;
            if (v1.Length != v2.Length) return false;

            for (int i = 0; i < v1.Length; i++) {
                if (v1[i] != v2[i]) return false;
            }
            return true;
        }

        public static bool Equals(double[] v1, double[] v2) { return Equals(v1, v2, Constants.K_numerical_epsilon); }

        public static bool Equals<T>(T[] v1, T[] v2)
        {
            if (v1 == v2) return true;
            if (v1 == null || v2 == null) return false;
            if (v1.Length != v2.Length) return false; 
            for (int i = 0; i < v1.Length; i++)
            {
                if (!v1[i].Equals(v2[i])) return false;
            }
            return true;
        }

        public static bool Equals(double v1, double v2, double accep)
        {
            return Math.Abs(v1 - v2) <= Math.Abs(accep);  
        }

        public static Boolean Equals(double[] v1, double[] v2, double accep)
        {
            if (v1 == v2) return true;
            if (v1 == null || v2 == null) return false;
            if (v1.Length != v2.Length) return false;
            for (int i = 0; i < v1.Length; i++)
            {
                if (!Equals(v1[i], v2[i], accep)) return false;
            }
            return true;
        }



    }
}
