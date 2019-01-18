using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace MW_Debug_2
{
    public static class Info
    {
        public static List<pVehicleInfo> pVehicle = new List<pVehicleInfo>();

        public static int[] loadingWorldStates = { 5 };
        public static int[] loadingMenuStates = { 7, 8, 1 };

        public static int pOffset = 176;

        public struct Object
        {
            public static IntPtr X = (IntPtr)0x9386e0;
            public static IntPtr Y = (IntPtr)0x9386d8;
            public static IntPtr Z = (IntPtr)0x9386DC;
            public static IntPtr RotX = (IntPtr)0x9386CC;
            public static IntPtr RotY = (IntPtr)0x9386D0;
            public static IntPtr RotZ = (IntPtr)0x9386D4;
            public static IntPtr RotW = (IntPtr)0x9386C8;
            public static IntPtr Vel1 = (IntPtr)0x9386e8;
            public static IntPtr Vel2 = (IntPtr)0x9386ec;
            public static IntPtr Vel3 = (IntPtr)0x9386f0;
            public static IntPtr Mass = (IntPtr)0x9386F4;
        }
        public struct Camera
        {
            public static IntPtr X = (IntPtr)0x9868B0;
            public static IntPtr Y = (IntPtr)0x9868B4;
            public static IntPtr Z = (IntPtr)0x9868B8;
        }

        public static IntPtr GameSpeed = (IntPtr)0x901B1C;
        public static IntPtr GameState = (IntPtr)0x925e90;
        public static IntPtr GamePlayA = (IntPtr)0x8F40C4;

        public struct SkipFE
        {
            public static IntPtr Enabled = (IntPtr)0x00926064;
            public static IntPtr PlayerCar = (IntPtr)0x008F86A8;
            public static IntPtr RaceID = (IntPtr)0x00926068;
            public static IntPtr RaceType = (IntPtr)0x926098;
            public static IntPtr NumPlayerCars = (IntPtr)0x8F86B8;
            public static IntPtr FEDatabse = (IntPtr)0x91CF90;
            public static IntPtr NumLaps = (IntPtr)0x008F86BC;
            public static IntPtr TrackNumber = (IntPtr)0x008F86A4;
            public static IntPtr MaxCops = (IntPtr)0x926088;
            public static IntPtr NumAICars = (IntPtr)0x926080;
        }

        public struct BooBoo
        {
            public static IntPtr PreCullerStr = (IntPtr)0x8B2AB4;
            public static IntPtr PreCullerFunc = (IntPtr)0x728BF0;
            public static int Timeout = 100;
        }

        public struct FNG
        {
            public static Dictionary<IntPtr, string> list = new Dictionary<IntPtr, string>();
            public static IntPtr MW_LS_Splash = (IntPtr)0x8A0114;
        }

        public static void WriteResourceToFile(string resourceName, string fileName)
        {
            using (var resource = System.Reflection.Assembly.GetExecutingAssembly().GetManifestResourceStream(resourceName))
            {
                using (var file = new FileStream(fileName, FileMode.Create, FileAccess.Write))
                {
                    resource.CopyTo(file);
                }
            }
        }
    }
}
