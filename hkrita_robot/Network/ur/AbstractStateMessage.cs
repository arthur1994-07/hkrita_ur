using hkrita_robot.Extension;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hkrita_robot.Network.ur
{
    public abstract class AbstractStateMessage : IMessage   
    {
        public abstract string GetName();
     
        
        protected string ToFormatString(string data)
        {
            return StringHelper.Format("{0} [{1}]", GetName(), data);
        }
    }
}
