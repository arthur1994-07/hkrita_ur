using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hkrita_robot.Extension
{
    public class ActionHelper
    {
        private Action<object> mAction;
        private object mTarget;

        public static void SetAction(Action<object> action, object target)
        {
            action.Invoke(target);
        }


    }
}
