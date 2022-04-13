using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hkrita_robot.Network
{
    public class URLauncher
    {
        public URLauncher()
        {
            URStream urStream = new URStream();
            urStream.Start();
            URControl urControl = new URControl();
            urControl.Start();

            Console.WriteLine("[INFO] Press Q to exit:");
            string stop = Convert.ToString(Console.ReadLine());

            if (stop == "q")
            {
                Console.WriteLine("Cartesian Space: Position (metres), Orientation (radian):");
                Console.WriteLine("X: {0} | Y: {1} | Z: {2} | RX: {3} | RY: {4} | RZ: {5}",
                                   URStreamData.C_Position[0], URStreamData.C_Position[1], URStreamData.C_Position[2],
                                   URStreamData.C_Orientation[0], URStreamData.C_Orientation[1], URStreamData.C_Orientation[2]);
                // Destroy UR {Control / Stream}
                urStream.Destroy();
                urControl.Destroy();

                // Application quit
                Environment.Exit(0);
            }
        }

    }
}
