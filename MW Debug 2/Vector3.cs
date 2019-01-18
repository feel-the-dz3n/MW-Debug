using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MW_Debug_2
{
    public struct Vector3
    {
        public float X;
        public float Y;
        public float Z;
    }
    public class Vector3c
    {
        public static Vector3 GetVector3(float x, float y, float z) { return new Vector3() { X = x, Y = y, Z = z }; }
        public static IntPtr Allocate(Vector3 vec)
        {
            var vMem = MWDBG.m.Memory.Allocate(12);
            vMem.MustBeDisposed = false;
            //bytes[] v3bytes
            //return MWDBG.WriteMemoryArray((uint)vMem.BaseAddress, );
            MWDBG.WriteStruct<Vector3>((uint)vMem.BaseAddress, vec);
            return vMem.BaseAddress;
        }
    }
}
