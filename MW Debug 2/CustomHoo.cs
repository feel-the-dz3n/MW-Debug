using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MW_Debug_2
{
    class CustomHoo
    {
        public static void StartHoo()
        {
            MWDBG.SetLog("Launching hoo script");
            MWDBG.m[Info.BooBoo.PreCullerFunc - 0x400000].Execute();
            MWDBG.SetLog("Done");
        }
    }
}
