using java.nio.charset;
using java.util;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace hkrita_robot.Network
{
    public class BufferedData
    {
        private readonly List<byte[]> mByteData = new List<byte[]>();
        private byte[] mBytes = null;
        private int mCount = 0;
        private int mOffset = 0;


        public void Clear()
        {
            mByteData.Clear();
            mCount = 0;
            mOffset = 0;
            mBytes = null;
        }

        public void Add(byte[] bytes)
        {
            if (bytes == null || bytes.Length == 0) return;
            mByteData.Add(bytes);
            mCount += bytes.Length;
        }

        public int Count() { return mCount; }

        public byte[] getData(int size)
        {
            size = Math.Min(Math.Max(size, 0), mCount);
            byte[] buffer = new byte[size];

            int current = 0;
            if (mBytes != null)
            {
                Array.Copy(mBytes, 0, buffer, current, mBytes.Length);
                current += mBytes.Length;
                mCount -= mBytes.Length;
                mBytes = null;
            }

            while (current < size && mByteData.Count() != 0)
            {
                byte[] first = mByteData.ElementAt(0);
                int read = Math.Min(first.Length - mOffset, size - current);
                Array.Copy(first, mOffset, buffer, current, read);
                current += read;
                mOffset += read;
                mCount -= read;
                if (mOffset >= first.Length)
                {
                    mByteData.RemoveAt(0);
                    mOffset = 0;
                }
            }
            return buffer.Length == current ? buffer : null;
        }

        public int getStringSeparator()
        {
            int current = mByteData == null ? 0 : mBytes.Length;
            bool attempted = false;
            for (int i = 0; i < mByteData.Count(); i++)
            {
                byte[] currentBuffer = mByteData.ElementAt(i);
                for (int j = i == 0? mOffset : 0; j < currentBuffer.Length; j++, current++)
                {
                    if (currentBuffer[j] == '\n') return current;
                    if (attempted) return current - 1;
                    if (currentBuffer[j] == '\r') attempted = true;
                }
            }
            if (attempted) return current - 1;
            mBytes = getData(mCount);
            mCount += mBytes.Length;
            return -1;
        }

        //public static String Convert(byte[] byteData)
        //{
        //    if (byteData == null || byteData.Length == 0) return null;
        //    int length = byteData.Length;
        //    if (byteData[byteData.Length - 1] == '\r')
        //    {
        //        length = byteData.Length - 1;
        //    }
        //    else if (byteData[byteData.Length - 1] == '\n')
        //    {
        //        length = byteData.Length > 1 && byteData[byteData.Length - 2] == '\r' ? byteData.Length - 2 : byteData.Length - 1;
        //    }
        //    return new String(Arrays.copyOf(byteData);
        //}

    }
}
