using Binarysharp.MemoryManagement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MW_Debug_2
{
    public static class GameSpeed
    {
        // Methods
        public static void Decrease(MemorySharp m)
        {
            MWDBG.writeMem<float>(Info.GameSpeed, Get(m) - 0.1f);
        }

        public static float Get(MemorySharp m)
        {
            return MWDBG.readMem<float>(Info.GameSpeed);
        }

        public static void Increase(MemorySharp m)
        {
            MWDBG.writeMem<float>(Info.GameSpeed, Get(m) + 0.1f);
        }

        public static void Set(MemorySharp m, float value)
        {
            MWDBG.writeMem<float>(Info.GameSpeed, value);
        }
    }


}
