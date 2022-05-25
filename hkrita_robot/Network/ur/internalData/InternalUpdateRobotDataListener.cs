using hkrita_robot.Maths;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hkrita_robot.Network.ur.internalData
{
    public class InternalUpdateRobotDataListener : IMessageDispatcher
    {
        private InternalRobotData mData;
        
        public InternalUpdateRobotDataListener(InternalRobotData data)
        {
            mData = data;   
        }


        public void UpdateInternalData()
        {
            bool done;
            done = ProcessCartesianData();

        }

        private bool ProcessCartesianData()
        {
            Pose current = mData.robotPose.Get();
            if (current == null) mData.robotPose.Set(new Pose());
            return true;
        }


        private bool ProcessJointData()
        {

            return true;
        }

        public void PutMessage(IMessage message)
        {
            throw new NotImplementedException();
        }


    }
}
