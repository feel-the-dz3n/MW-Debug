using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MW_Debug_2
{
    public static class CarSpawnerClass
    {
        public struct VehicleParams
        {
            public UInt32 typeName;
            public UInt32 someFixedHash;
            public IntPtr pTypeName;
            public UInt32 __unk1; // even after hours of debugging, I don't know what this is
            public UInt32 driverClass;
            public UInt32 carHash;
            public IntPtr pInitialRotation;
            public IntPtr pInitialPosition;
            public UInt32 pFECustomizationRecord; //0 чтобы сток
            public UInt32 __unk_AIRelated; // 0
            public UInt32 __unk2;
            public UInt32 __unk_ImportanceRelated; // 2
            public UInt32[] VARIABLES;
        };

        public static IntPtr AllocateVehicleParams(uint CarHash, IntPtr Vector3Position, IntPtr Vector3Rotation, uint driverClass = 3, uint customization = 0, uint AIRelated = 0, uint importanceRelated = 2)
        {
            var allocatedMemory = MWDBG.m.Memory.Allocate(48);
            allocatedMemory.MustBeDisposed = false;

            uint[] array = {0x9FB193F0, 0x0A6B47FAC, (uint)allocatedMemory.BaseAddress, 0, driverClass, CarHash, (uint)Vector3Position, (uint)Vector3Rotation, customization, AIRelated, 0, importanceRelated};
            byte[] vars = array.SelectMany(BitConverter.GetBytes).ToArray();
            MWDBG.WriteMemoryArray((uint)allocatedMemory.BaseAddress, vars);

            return allocatedMemory.BaseAddress;
        }
        

        public static void SpawnCar(uint CarHash, Vector3 position, Vector3 rotation, uint driverClass = 3, uint customization = 0, uint AIRelated = 0, uint importanceRelated = 2)
        {
            //0x06465EB2 - hash
            IntPtr posV = Vector3c.Allocate(position);
            IntPtr rotV = Vector3c.Allocate(rotation);
            IntPtr vehParams = AllocateVehicleParams(CarHash, posV, rotV, driverClass, customization, AIRelated, importanceRelated);
            MWDBG.mWnd.AppendLog("Position = 0x" + posV.ToString("X4"));
            MWDBG.mWnd.AppendLog("Rotation = 0x" + rotV.ToString("X4"));
            MWDBG.mWnd.AppendLog("vehParams = 0x" + vehParams.ToString("X4"));
            MWDBG.mWnd.AppendLog("    ");
            MWDBG.mWnd.AppendLog("rot[0] from mem: " + MWDBG.readMem<float>(rotV));
            MWDBG.mWnd.AppendLog("rot[1] from mem: " + MWDBG.readMem<float>(rotV + 4));
            MWDBG.mWnd.AppendLog("rot[2] from mem: " + MWDBG.readMem<float>(rotV + 4 + 4));
            MWDBG.mWnd.AppendLog("    ");
            MWDBG.mWnd.AppendLog("mem vehP[0]: 0x" + MWDBG.readMem<uint>(vehParams).ToString("X4"));
            MWDBG.mWnd.AppendLog("mem vehP[1]: 0x" + MWDBG.readMem<uint>(vehParams + 4).ToString("X4"));
            MWDBG.mWnd.AppendLog("mem vehP[2]: 0x" + MWDBG.readMem<uint>(vehParams + 4 + 4).ToString("X4"));
            MWDBG.mWnd.AppendLog("mem vehP[3]: 0x" + MWDBG.readMem<uint>(vehParams + 4 + 4 + 4).ToString("X4"));
            MWDBG.mWnd.AppendLog("mem vehP[4]: 0x" + MWDBG.readMem<uint>(vehParams + 4 + 4 + 4 + 4).ToString("X4"));

            //MWDBG.m[(IntPtr)0x689820, false].Execute(Binarysharp.MemoryManagement.Assembly.CallingConvention.CallingConventions.Cdecl, vehParams, 0x0A6B47FAC);
        }

    }
}
