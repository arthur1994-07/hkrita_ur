using hkrita_robot.CodeTest;
using hkrita_robot.Extension;
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
            //URLauncher launcher = new URLauncher();
            var tcp = "p[-0.020, 0.010, 0.180, 0, 0, 1.5710]";
            //var str = tcp.Split(',');
            Pose p = Pose.ToPose(tcp);
            Console.WriteLine(p);
        }
    }
}
