using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hkrita_robot.Network.realtime
{
    public interface IEncoderDecoder
    {
        object Decode(ref object o, byte[] buffer, ref int offset);
        void Encode(object o, byte[] buffer, ref int offset);

        //https://sourceforge.net/p/firsttestfchaxel/code/HEAD/tree/trunk/Ur_Rtde/RtdeClient.cs#l199

    }
}
