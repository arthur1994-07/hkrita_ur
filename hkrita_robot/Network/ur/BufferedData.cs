using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace hkrita_robot.Network.ur
{
    public class BufferedData
    {
        public byte[] buffer = null;
        public int mCount = 0;
        public int mOffset = 0;
        public static int timeStep = 8;
        public static byte firstPacketSize = 4;
        public static byte streamOffset = 8;

        public void Clear()
        {
            buffer = null;
            mCount = 0;
            mOffset = 0;
        }

        public int Count() {  return mCount; }

        public byte[] WriteData(int size)
        {
            buffer = new byte[size];
            return buffer;
        }
        public int GetByteSize() { return buffer.Length; }
         
    }

}
