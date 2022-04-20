using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hkrita_robot.Extension
{
    public class ObservableExtension
    {
        public event EventHandler handler;

        public void DoSomething() => 
            handler?.Invoke(this, EventArgs.Empty);
    }
}
