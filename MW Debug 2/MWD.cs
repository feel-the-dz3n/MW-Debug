using Binarysharp.MemoryManagement.Assembly.CallingConvention;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MW_Debug_2
{
    public class MWD
    {
        /*unsigned int bStringHash(const char* input)
{
    const char* temp;
    unsigned char current_character;
    unsigned int result;

    temp = input;
    current_character = a1[0];
    for(result = 0xFFFFFFFF; current_character != 0; temp++)
    {
        result = current_character + 33 * result;
        current_character = temp[1];
    }
    return result;
}
}*/
        public static int bStringHash(string input)
        {
            string temp;
            char current_character;
            uint result = 0;
            int index = 0;

            temp = input;
            current_character = input[0];

            for (result = 0xFFFFFFFF; current_character != 0; index++)
            {
                result = current_character + 33 * result;
                if (index == input.Length - 1) break;
                current_character = temp[index + 1];
            }

            //DC.WriteLine("bStringHash(\"" + input + "\") > 0x" + result.ToString("X4"));
            return (int)result;
        }

        public static void StartSkipFERace()
        {
            int v1; // eax
            int v2; // ecx

            
            //MWDBG.writeMem((IntPtr)0x904FC4 = LODWORD(SkipFEDifficulty);
            MWDBG.writeMem<int>((IntPtr)0x904FD8, MWDBG.readMem<int>(Info.SkipFE.NumLaps));
            MWDBG.writeMem<int>((IntPtr)0x904FB8, MWDBG.readMem<int>(Info.SkipFE.TrackNumber));
            MWDBG.writeMem<int>((IntPtr)0x905008, MWDBG.readMem<int>(Info.SkipFE.MaxCops));
            MWDBG.writeMem<int>((IntPtr)0x904FCC, 1);//RaceType
            MWDBG.writeMem<int>((IntPtr)0x904FDC, MWDBG.readMem<int>(Info.SkipFE.NumPlayerCars));
            MWDBG.writeMem<int>((IntPtr)0x904FE0, MWDBG.readMem<int>(Info.SkipFE.NumAICars));
            MWDBG.writeMem<byte>((IntPtr)0x904FED, (byte)(MWDBG.readMem<int>(Info.SkipFE.NumAICars) + 2));
            MWDBG.writeMem<byte>((IntPtr)0x904FEC, (byte)(MWDBG.readMem<int>(Info.SkipFE.NumAICars) + 1));

            MWDBG.writeMem<int>((IntPtr)0x90501C, 1065353216);
            MWDBG.writeMem<int>((IntPtr)0x905020, 1065353216);
            MWDBG.writeMem<int>((IntPtr)0x905030, 0);
            MWDBG.m[(IntPtr)0x56C5B0 - 0x400000].Execute();
            /*dword_ = SkipFEMaxCops;
            dword_904FBC = SkipFETrackDirection;
            dword_ = SkipFERaceType;
            dword_904FD0 = dword_92609C;
            dword_905014 = SkipFEDifficulty_0;
            dword_90500C = SkipFEDifficulty_0;
            dword_ = SkipFENumPlayerCars;
            dword_ = SkipFENumAICars;
            byte_ = SkipFENumAICars + 2;
            byte_904FEE = dword_8F86D0 != 0;
            byte_[0] = SkipFENumAICars + 1;*/
        }

        public static void SkipFE_Load()
        {
            if (MWDBG.readMem<int>(Info.GameState) != 3)
            {
                MWDBG.m[(IntPtr)0x6052B0 - 0x400000].Execute();
                while (MWDBG.readMem<int>(Info.GameState) != 3) Thread.Sleep(20);
            }
            MWDBG.writeMem<int>(Info.SkipFE.Enabled, 1);
            MWDBG.m[(IntPtr)0x56C5B0 - 0x400000].Execute();
        }

        public static int SetPlayerCar(string carName)
        {
            int ecx = MWDBG.readMem<int>((IntPtr)0x9B08F8);
            int result = MWDBG.m[(IntPtr)0x755340, false].Execute<int>(CallingConventions.Thiscall, ecx, carName);
            return result;
        }
    }
}
