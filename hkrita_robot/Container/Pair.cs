using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hkrita_robot.Container
{
    public class Pair<A, B> : ICloneable
    {
        private A mA;
        private B mB;

        public Pair(A a, B b) { Set(a, b); }
        public Pair(Pair<A, B> pair) { Set(pair); }

        public Pair<A, B> Set(A a, B b)
        {
            mA = a;
            mB = b;
            return this;
        }

        public Pair<A, B> Set(Pair<A,B> pair)
        {
            mA = pair.mA;
            mB = pair.mB;
            return this;
        }

        public A GetFirst() { return mA; }
        public B GetSecond() { return mB; }

        public object Clone() { return new Pair<A, B>(this); }

    }
}
