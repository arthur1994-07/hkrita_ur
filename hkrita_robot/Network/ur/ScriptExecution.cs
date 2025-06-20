using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hkrita_robot.Network.ur
{
    public class ScriptExecution
    {
        private string mScript;
        public ScriptExecution(string script)
        {
            mScript = script;   
        }


        public string GetScript() { return mScript; }   

        public void PassFunction()
        {
            //Action<string> function = () =>
            //{

            //}
        }
    }
}
