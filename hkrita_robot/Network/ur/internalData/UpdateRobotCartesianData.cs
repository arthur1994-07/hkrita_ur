using hkrita_robot.Container;
using hkrita_robot.Extension;
using hkrita_robot.Maths;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hkrita_robot.Network.ur.internalData
{
    public class UpdateRobotCartesianData
    {
        public double[] J_Orientation = new double[6];
        public double[] C_Position = new double[3];
        public double[] C_Orientation = new double[3];
        public SixJointAngles jointAngles;
        public Pose pose;

        public Pose UpdateRobotPose(BufferedData bufferData, byte packetSize, byte offset)
        {
            C_Position[0] = BitConverter.ToDouble(bufferData.buffer, bufferData.buffer.Length - packetSize - (56 * offset));
            C_Position[1] = BitConverter.ToDouble(bufferData.buffer, bufferData.buffer.Length - packetSize - (57 * offset));
            C_Position[2] = BitConverter.ToDouble(bufferData.buffer, bufferData.buffer.Length - packetSize - (58 * offset));

            // read cartesian (orientation) values in radian
            C_Orientation[0] = BitConverter.ToDouble(bufferData.buffer, bufferData.buffer.Length - packetSize - (59 * offset));
            C_Orientation[1] = BitConverter.ToDouble(bufferData.buffer, bufferData.buffer.Length - packetSize - (60 * offset));
            C_Orientation[2] = BitConverter.ToDouble(bufferData.buffer, bufferData.buffer.Length - packetSize - (61 * offset));
            
            return new Pose(C_Position[0], C_Position[1], C_Position[2],
                C_Orientation[0], C_Orientation[1], C_Orientation[2]);
        }

        public SixJointAngles UpdateRobotJoints(BufferedData bufferData, byte packetSize, byte offset)
        {
            J_Orientation[0] = BitConverter.ToDouble(bufferData.buffer, bufferData.buffer.Length - packetSize - (32 * offset));
            J_Orientation[1] = BitConverter.ToDouble(bufferData.buffer, bufferData.buffer.Length - packetSize - (33 * offset));
            J_Orientation[2] = BitConverter.ToDouble(bufferData.buffer, bufferData.buffer.Length - packetSize - (34 * offset));
            J_Orientation[3] = BitConverter.ToDouble(bufferData.buffer, bufferData.buffer.Length - packetSize - (35 * offset));
            J_Orientation[4] = BitConverter.ToDouble(bufferData.buffer, bufferData.buffer.Length - packetSize - (36 * offset));
            J_Orientation[5] = BitConverter.ToDouble(bufferData.buffer, bufferData.buffer.Length - packetSize - (37 * offset));
            
            return new SixJointAngles(J_Orientation[0], J_Orientation[1], J_Orientation[2],
                J_Orientation[3], J_Orientation[4], J_Orientation[5]);
        }

        public object ReadCartesianInput(byte[] buffer, byte packetSize, byte offset)
        {


            //Read Joint values in Radians
            J_Orientation[0] = BitConverter.ToDouble(buffer, buffer.Length - packetSize - (32 * offset));
            J_Orientation[1] = BitConverter.ToDouble(buffer, buffer.Length - packetSize - (33 * offset));
            J_Orientation[2] = BitConverter.ToDouble(buffer, buffer.Length - packetSize - (34 * offset));
            J_Orientation[3] = BitConverter.ToDouble(buffer, buffer.Length - packetSize - (35 * offset));
            J_Orientation[4] = BitConverter.ToDouble(buffer, buffer.Length - packetSize - (36 * offset));
            J_Orientation[5] = BitConverter.ToDouble(buffer, buffer.Length - packetSize - (37 * offset));

            // Read Cartesian (Position) values in metres
            C_Position[0] = BitConverter.ToDouble(buffer, buffer.Length - packetSize - (56 * offset));
            C_Position[1] = BitConverter.ToDouble(buffer, buffer.Length - packetSize - (57 * offset));
            C_Position[2] = BitConverter.ToDouble(buffer, buffer.Length - packetSize - (58 * offset));

            // read cartesian (orientation) values in radian
            C_Orientation[0] = BitConverter.ToDouble(buffer, buffer.Length - packetSize - (59 * offset));
            C_Orientation[1] = BitConverter.ToDouble(buffer, buffer.Length - packetSize - (60 * offset));
            C_Orientation[2] = BitConverter.ToDouble(buffer, buffer.Length - packetSize - (61 * offset));


            jointAngles = new SixJointAngles(J_Orientation[0], J_Orientation[1], J_Orientation[2],
                J_Orientation[3], J_Orientation[4], J_Orientation[5]);

            pose = new Pose(C_Position[0], C_Position[1], C_Position[2],
                C_Orientation[0], C_Orientation[1], C_Orientation[2]);

            ClearData();

            //Console.WriteLine(pose);
            return new Pair<Pose, SixJointAngles>(pose, jointAngles);
        }

        public void ClearData()
        {
            ArraysHelper.Fill(J_Orientation, 0);
            ArraysHelper.Fill(C_Position, 0);
            ArraysHelper.Fill(C_Orientation, 0);

        }



        
    }
}
