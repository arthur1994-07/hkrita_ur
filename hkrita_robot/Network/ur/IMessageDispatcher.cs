using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hkrita_robot.Network.ur
{
    public interface IMessageDispatcher
    {
        void PutMessage(IMessage message);
    }
}
