using hkrita_robot.Maths;
using hkrita_robot.Network;
using hkrita_robot.UR.control;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hkrita_robot
{
    public class Program
    {
        Action<string> mAction;
        public static void Main(string[] args)
        {
            Pose pose = new Pose(1, 2, 3, 4, 5, 6);
            string script = "";
            //URLauncher launcher = new URLauncher();
            RobotController robot = new RobotController("192.168.56.101");
            robot.SetTCP(pose);
            robot.SubmitScript();

            robot.SetAction((s) => 
            {
                Console.WriteLine(s);
            }, null);
        }
    }
}
