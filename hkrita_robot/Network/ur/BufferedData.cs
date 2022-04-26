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
        
        // read input stream data 
        public static void ReadPoseStreamInput(byte[] buffer, byte packetSize, byte offset)
        {
            //Read Joint values in Radians
            URStreamData.J_Orientation[0] = BitConverter.ToDouble(buffer, buffer.Length - packetSize - (32 * offset));
            URStreamData.J_Orientation[1] = BitConverter.ToDouble(buffer, buffer.Length - packetSize - (33 * offset));
            URStreamData.J_Orientation[2] = BitConverter.ToDouble(buffer, buffer.Length - packetSize - (34 * offset));
            URStreamData.J_Orientation[3] = BitConverter.ToDouble(buffer, buffer.Length - packetSize - (35 * offset));
            URStreamData.J_Orientation[4] = BitConverter.ToDouble(buffer, buffer.Length - packetSize - (36 * offset));
            URStreamData.J_Orientation[5] = BitConverter.ToDouble(buffer, buffer.Length - packetSize - (37 * offset));

            // Read Cartesian (Position) values in metres
            URStreamData.C_Position[0] = BitConverter.ToDouble(buffer, buffer.Length - packetSize - (56 * offset));
            URStreamData.C_Position[1] = BitConverter.ToDouble(buffer, buffer.Length - packetSize - (57 * offset));
            URStreamData.C_Position[2] = BitConverter.ToDouble(buffer, buffer.Length - packetSize - (58 * offset));

            // read cartesian (orientation) values in radian
            URStreamData.C_Orientation[0] = BitConverter.ToDouble(buffer, buffer.Length - packetSize - (59 * offset));
            URStreamData.C_Orientation[1] = BitConverter.ToDouble(buffer, buffer.Length - packetSize - (60 * offset));
            URStreamData.C_Orientation[2] = BitConverter.ToDouble(buffer, buffer.Length - packetSize - (61 * offset));


            Console.WriteLine("Position: " + URStreamData.C_Position[0] + ", " + URStreamData.C_Position[1] + ", " + URStreamData.C_Position[2]);

        }

         


    }

}
