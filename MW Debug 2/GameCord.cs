using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MW_Debug_2
{
    public class GameCord
    {
        public float x, y, z, r1, r2, r3, r4;
    }
    public class GameCordFuncs
    {
        public static GameCord GetCurrentCords(int offset)
        {
            GameCord c = new GameCord();
            c.x = MWDBG.readMem<float>(Info.Object.X + MWDBG.GetOffsetId(offset));
            c.y = MWDBG.readMem<float>(Info.Object.Y + MWDBG.GetOffsetId(offset));
            c.z = MWDBG.readMem<float>(Info.Object.Z + MWDBG.GetOffsetId(offset));
            c.r1 = MWDBG.readMem<float>(Info.Object.RotX + MWDBG.GetOffsetId(offset));
            c.r2 = MWDBG.readMem<float>(Info.Object.RotY + MWDBG.GetOffsetId(offset));
            c.r3 = MWDBG.readMem<float>(Info.Object.RotZ + MWDBG.GetOffsetId(offset));
            c.r4 = MWDBG.readMem<float>(Info.Object.RotW + MWDBG.GetOffsetId(offset));
            return c;
        }
    }
}
