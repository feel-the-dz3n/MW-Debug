using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MW_Debug_2
{
    public class Replays
    {
        public static int Double = 2;
        public static bool Recording = false;
        public static List<ReplayInfo> rInfo = new List<ReplayInfo>();

        public static void AddInfo(ReplayInfo r)
        {
            /*if (!rInfo.Contains(r)) */rInfo.Add(r);
        }
    }
}
