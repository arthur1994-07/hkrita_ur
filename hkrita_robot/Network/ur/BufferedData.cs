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
        public readonly List<byte[]> mBufferlist = new List<byte[]>();
        public byte[] mBuffer = null;
        public int mCount = 0;
        public int mOffset = 0;


        public void Clear()
        {
            mBufferlist.Clear();
            mBuffer = null;
            mCount = 0;
            mOffset = 0;
        }

        public int Count() {  return mCount; }

        public byte[] WriteData(int size)
        {
            mBuffer = new byte[size];
            return mBuffer;
        }
        public int GetByteSize() { return mBuffer.Length; }
         
    }

}
