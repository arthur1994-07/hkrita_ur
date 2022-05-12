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
            Pose pose = new Pose(-0.02, 0.01, 0.0, 0, 0, 0.03);
            URLauncher launcher = new URLauncher();


            //RobotController robot = new RobotController("192.168.56.101");
            //robot.SetTCP(pose);
            //robot.Close();
            //robot.SubmitScript((s) => 
            //{
            //    Console.WriteLine("obtained script: "+ s);
            //});
        }
    }
}
