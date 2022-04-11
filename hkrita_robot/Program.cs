using hkrita_robot.Network;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hkrita_robot
{
    public class Program
    {
        //public static void Main(string[] args)
        //{
        //    //AsyncSocketClient asyncClient = new AsyncSocketClient("192.168.56.101", 30002);
        //    SyncSocketClient client = new SyncSocketClient("192.168.56.101", 30002);
        //    // continuously send bytes to UR from client..
        //    //SyncSocketClient.WriteScript();
        //    while (true)
        //    {
        //        client.ConnectClient();
        //    }
        //}

        public static void Main(string[] args)
        {

            StreamData streamData = new StreamData("192.168.56.101", 30002, 8);


            ControlData controlData = new ControlData("192.168.56.101", 30002, 8);

            //Start Stream UR TCP IP
            URStream urStream = new URStream();
            urStream.Start();


            //Start Contol UR TCP IP
            URControl urControl = new URControl();
            urControl.Start();


            Console.WriteLine("[INFO] Stop (y):");

            string stop_rs = Convert.ToString(Console.ReadLine());

            if (stop_rs == "y")
            {

                //Console.WriteLine("Joint Space: Orientation (radian)");
                //Console.WriteLine("J1: {0} | J2: {1} | J3: {2} | J4: {3} | J5: {4} | J6: {5}",
                //                   StreamData.J_Orientation[0], StreamData.J_Orientation[1], StreamData.J_Orientation[2],
                //                   StreamData.J_Orientation[3], StreamData.J_Orientation[4], StreamData.J_Orientation[5]);

                Console.WriteLine("Cartesian Space: Position (metres), Orientation (radian):");
                Console.WriteLine("X: {0} | Y: {1} | Z: {2} | RX: {3} | RY: {4} | RZ: {5}",
                                   StreamData.C_Position[0], StreamData.C_Position[1], StreamData.C_Position[2],
                                   StreamData.C_Orientation[0], StreamData.C_Orientation[1], StreamData.C_Orientation[2]);

                urStream.Destroy();
                Environment.Exit(0);
            }
        }
    }
}
