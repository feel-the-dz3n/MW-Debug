using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MW_Debug_2.Funcs
{
    public static class FuncList
    {
        public static Dictionary<string, string> funcs = new Dictionary<string, string>();
        public static void Init()
        {
            funcs.Add("Start pursuit", "0x60AAC0");
            funcs.Add("Jump to safehouse", "0x6052B0");
            funcs.Add("Jump to car lot", "0x605250");
            funcs.Add("Start career freeroam", "0x56C5B0");
            funcs.Add("Force reload world", "0x6057E0");
            funcs.Add("Unpause", "0x632190");
            funcs.Add("Loading screen off", "0x62D520");
            funcs.Add("Loading screen on", "0x62D6F0");
            funcs.Add("Show marker", "0x611FB0");
            funcs.Add("Hide marker", "0x611FF0");
            funcs.Add("Abandon race", "0x60DEB0");
            funcs.Add("Hide GPS", "0x605340");
            funcs.Add("Camera Shake (pfuncs.txt)", "0x0064c590");
            funcs.Add("Camera Shake", "0x62b110");
            funcs.Add("Police radio msg 1", "0x0062D2B0");
            funcs.Add("Police radio msg 2", "0x0062B6B0");
            funcs.Add("World map on", "0x00632370");
            funcs.Add("Show pause menu thx nlgzrgn", "0x6050F0");
            funcs.Add("CameraAIReset thx nlgzrgn", "0x0047CC50");
        }
        public static IntPtr StrToPtr(string hexString)
        {
            IntPtr ptr;
            hexString = hexString.Substring(2);
            long decAgain = long.Parse(hexString, System.Globalization.NumberStyles.HexNumber);
            ptr = new IntPtr(decAgain);
            return ptr;
        }
    }
}
