using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hkrita_robot.Extension
{
    public class EventHandleHelper
    {
        public event EventHandler mEvent;
        private int mThreshold;
        private int mTotal;
        public EventHandleHelper (int passThreshold)
        {
            mThreshold = passThreshold; 
        }

        public void 
    }
}
