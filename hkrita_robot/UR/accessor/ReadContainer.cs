﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hkrita_robot.Container
{
    public class ReadContainer<T>
    {
        private T mGeneric;

        public ReadContainer(T type)
        {
            mGeneric = type;
        }

       
        
    }
}
