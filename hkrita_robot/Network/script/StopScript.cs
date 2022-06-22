using hkrita_robot.Extension;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hkrita_robot.Network.script
{
    public class StopScript : IAbstractScript
    {
        public StopScript() { } 

        public string GetScript()
        {
            return StringHelper.Format("abort");
        }

    }
}
