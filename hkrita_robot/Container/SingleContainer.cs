using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hkrita_robot.Container
{
    public class SingleContainer<A> 
    {
        private A mA;

        public SingleContainer(A a) { Set(a); }

        public SingleContainer<A> Set(A a)
        {
            mA = a;
            return this;
        }

        public A GetA() { return mA; }


    }
}
