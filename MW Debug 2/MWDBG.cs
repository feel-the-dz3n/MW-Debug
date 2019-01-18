using Binarysharp.MemoryManagement;
using DX9OverlayAPI;
using FMUtils.KeyboardHook;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Forms;

namespace MW_Debug_2
{
    public class MWDBG
    {
        public static IntPtr pHandle;
        public static MemorySharp m;
        public static MainWindow mWnd;
        

        static int idSelected = 0;
        static int task = 0;
        static bool doTask = false;
        static bool working = false;
        static bool showCoords = false;
        static string debugLog = "MW Debug by Dz3n";
        static int idGameSpeed = -1;
        static int idText = -1;  
        #region P_S_C
        public static Dictionary<int, int> psc = new Dictionary<int, int>();
        public static int PscPower = 100;
        public static bool PscShow = false;
#endregion
        public static bool rulezShown = false;
        //public Comparator cpF;
        public static bool workMin = false;
        public static bool disableLoading = true;
        public static int thrSleep = 0;
        public static int fps = 0;
        public static System.Timers.Timer BustedT;
        static int visibility = 0;
        public static bool BustedF;
        static float hudX, hudY;
        //static int LogoId = -1;
        public static int CurrentReplayFrame = 0;
        public static bool CordLogEnabled = false;
        
        public static bool PlayerToPoint(float radi, float oldposx, float oldposy, float oldposz, float x, float y, float z)
        {
            float tempposx, tempposy, tempposz;
            //GetPlayerPos(playerid, oldposx, oldposy, oldposz);
            tempposx = (oldposx - x); tempposy = (oldposy - y); tempposz = (oldposz - z);
            if (((tempposx < radi) && (tempposx > -radi)) && ((tempposy < radi) && (tempposy > -radi)) && ((tempposz < radi) && (tempposz > -radi))) { return true; }
            return false;
        } public static void MW_Debug_Intro()
        {
            int width = 0, height = 0;
            while (width == 0 && height == 0)
            {
                DX9Overlay.GetScreenSpecs(out width, out height);
            }
            //LogoId = DX9Overlay.ImageCreate("DebRes\\dbglogo.png", width/3+50, height-55, 0, 0, true);
            GameSlider.Init(height, width);
            int x_psc = 230, y_psc = 100;
            uint yellow = 0xFFFFFF00, red = 0xFFFF0000, green = 0xFF00FF00;

            psc.Add(DX9Overlay.BoxCreate(x_psc - 40, y_psc - 60, 80, 100, 0x55000000, true), 0);
            psc.Add(DX9Overlay.TextCreate("Arial", 7, false, false, x_psc - 40, y_psc - 60, 0xFFFFFFFF, "Controls: RAlt +", false, true), 1);
            psc.Add(DX9Overlay.TextCreate("Arial", 7, false, false, x_psc - 40, y_psc + 40, 0xFFFFFFFF, "You can use this keys even if task is not selected", true, true), 1);

            psc.Add(DX9Overlay.LineCreate(x_psc, y_psc, x_psc + 30, y_psc, 1, red, true), 2);
            psc.Add(DX9Overlay.LineCreate(x_psc, y_psc, x_psc, y_psc - 30, 1, yellow, true), 2);
            psc.Add(DX9Overlay.LineCreate(x_psc, y_psc, x_psc - 10, y_psc + 10, 1, green, true), 2);

            psc.Add(DX9Overlay.LineCreate(x_psc, y_psc, x_psc - 30, y_psc, 1, red, true), 2);
            psc.Add(DX9Overlay.LineCreate(x_psc, y_psc, x_psc, y_psc + 30, 1, yellow, true), 2);
            psc.Add(DX9Overlay.LineCreate(x_psc, y_psc, x_psc + 10, y_psc - 10, 1, green, true), 2);

            psc.Add(DX9Overlay.TextCreate("Arial", 7, false, false, x_psc + 30, y_psc, 0xFFFFFFFF, "k", false, true), 1);
            psc.Add(DX9Overlay.TextCreate("Arial", 7, false, false, x_psc - 3, y_psc - 45, 0xFFFFFFFF, "u", false, true), 1);
            psc.Add(DX9Overlay.TextCreate("Arial", 7, false, false, x_psc - 10, y_psc + 10, 0xFFFFFFFF, "h", false, true), 1);

            psc.Add(DX9Overlay.TextCreate("Arial", 7, false, false, x_psc - 30, y_psc, 0xFFFFFFFF, "y", false, true), 1);
            psc.Add(DX9Overlay.TextCreate("Arial", 7, false, false, x_psc + 3, y_psc + 25, 0xFFFFFFFF, "J", false, true), 1);
            psc.Add(DX9Overlay.TextCreate("Arial", 7, false, false, x_psc + 10, y_psc - 10, 0xFFFFFFFF, "i", false, true), 1);

            PscShow = false;
            /*hudX = readMem<float>((IntPtr)0x6E6F55);
            hudY = readMem<float>((IntPtr)0x6E6F5D);
            writeMem<float>((IntPtr)0x6E6F55, 0);
            writeMem<float>((IntPtr)0x6E6F5D, 0);*/

        }
        public static void PscShown(bool show)
        {
            foreach (var i in psc)
            {
                if (i.Value == 0) DX9Overlay.BoxSetShown(i.Key, show);
                if (i.Value == 1) DX9Overlay.TextSetShown(i.Key, show);
                if (i.Value == 2) DX9Overlay.LineSetShown(i.Key, show);
            }
        }
        public static List<int> GetNearestObject(float x, float y, float z, float radius = 30)
        {
            int k = 1;
            float x1 = 0, y1 = 0, z1 = 0;
            List<int> n = new List<int>();
            while (k != 41)
            {
                x1 = readMem<float>((IntPtr)(Info.Object.X + GetOffsetId(k)));
                y1 = readMem<float>((IntPtr)(Info.Object.Y + GetOffsetId(k)));
                z1 = readMem<float>((IntPtr)(Info.Object.Z + GetOffsetId(k)));
                if (PlayerToPoint(radius, x1, y1, z1, x, y, z)) n.Add(k);
                k++;
            }
            if (n.Count >= 1) return n;
            return null;
        }

        static public T readMem<T>(IntPtr address)
        {
            return m.Read<T>(address, false);
        }
        public static void InsertNop(IntPtr address)
        {
            m.Assembly.Inject("nop", address);
        }
        static public void writeMem<T>(IntPtr address, T value)
        {
            m.Write<T>(address, value, false);
        }
        public static int GetOffsetId(int id)
        {
            if (id == 0) return 0;
            return Info.pOffset * id;
        }
        public static void SetLog(string txt)
        {
            debugLog = txt;
        }

        public static void SetFE()
        {
            try
            {
                if (int.Parse(Funcs.ProgCfg.Settings["skipfe"]) == 1)
                {
                    mWnd.SetStatusD("Skipping frontend");
                    string name = Funcs.ProgCfg.Settings["skipfePlayerCar"];
                    var MemModel = m.Memory.Allocate(name.Length);
                    MemModel.WriteString(name);
                    MemModel.MustBeDisposed = false;


                    writeMem<int>(Info.SkipFE.Enabled, 1);
                    writeMem<int>(Info.SkipFE.PlayerCar, MemModel.BaseAddress.ToInt32());

                    MWD.SkipFE_Load();
                }
            }
            catch { mWnd.AppendLogT("SkipFE error!"); }
        }
        public static void dxset(int maxO)
        {
            mWnd.SetStatusD("Setting up DX Overlay");
            DX9Overlay.SetParam("process", "speed.exe");
            DX9Overlay.DestroyAllVisual();

            int tc = 0, tp = 2;
            while (tc != maxO)
            {
                DX9Overlay.TextCreate("Arial", 7, false, false, 2, tp, 0xFFFFFFFF, " ", true, true);
                tc++;
                tp += 10;
            }

            DX9Overlay.TextCreate("Arial", 7, false, false, 2, tp, 0xFFFFFFFF, " ", true, true);
            idText = DX9Overlay.TextCreate("Arial", 7, true, false, 230, 2, 0xFFFFFFFF, "idText", true, true);
            idGameSpeed = DX9Overlay.TextCreate("Arial", 7, true, false, 230, 13, 0xFFFFFFFF, "idGameSpeed", true, true);
            //int loadingLogo = DX9Overlay.ImageCreate("C:\\dbglogo.png", 0, 0, 0, 0, false);

            // LoadingId = DX9Overlay.ImageCreate("DebRes\\LoadingTemplate.png", 0, 0, 0, 0, false);

            MW_Debug_Intro();
        }
        public static void StartTrainer(MainWindow mainWnd)
        {
            mWnd = mainWnd;
            mWnd.SetStatus("Starting thread");
            new Thread(work).Start();
        }

        public static void TaskChanged(int oldtask, int newtask)
        {
            if (oldtask == 14)
            {
                MassSlider(false);
            }
            if (oldtask == 15)
            {
                RBSlider(false);
                PscShow = false;
            }
            if (newtask == 14)
            {
                MassSlider(true);
            }
            if (newtask == 15)
            {
                RBSlider(true);
                PscShow = true;
            }
            if (newtask == 0x13)
            {
                SetLog("{FF0000}This function can totally break your game! Don't try to respawn!");
            }
            if (newtask == 0x19)
            {
                SetLog("Select object ID. Use ID 0 or Ctrl+Q to return to normal state.");
            }
        }
        public static void SelectedIdChanged()
        {
            if ((task == 14) && (idSelected >= 0))
            {
                GameSlider.Value = (int)readMem<float>(Info.Object.Mass + GetOffsetId(idSelected));
            }
        }
        public static void RBSlider(bool show)
        {
            if (show)
            {
                GameSlider.IsVisible = true;
                GameSlider.Min = 0;
                GameSlider.Max = 0x2710;
                GameSlider.Value = PscPower;
            }
            else
            {
                GameSlider.IsVisible = false;
            }
        }
        public static void MassSlider(bool show)
        {
            if (show)
            {
                GameSlider.IsVisible = true;
                GameSlider.Min = 0;
                GameSlider.Max = 0x7a120;
                GameSlider.Value = 0x3e8;
                if (idSelected >= 0)
                {
                    GameSlider.Value = (int)readMem<float>(Info.Object.Mass + GetOffsetId(idSelected));
                }
            }
            else
            {
                GameSlider.IsVisible = false;
            }
        }

        public static void TaskStateChanged()
        {
            if (!doTask)
            {
                if (task == 20)
                {
                    Replays.Recording = false;
                    SetLog("{00FF00}RECORDING DONE");
                }
            }
            else
            {
                if (task == 20)
                {
                    Replays.rInfo.Clear();
                    Replays.Recording = true;
                    DX9Overlay.TextSetString(idText, " ");
                }
                if (task == 0x15)
                {
                    CurrentReplayFrame = 0;
                }
            }
        }


        private static void BustedT_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            if (readMem<int>(Info.GameState) == 6)
            {
                m[((IntPtr)0x60ae00) - 0x400000, true].Execute();
            }
            DC.WriteLine("anti busted", true);
        }


        public static unsafe void WriteStruct<T>(uint adress, T input) where T : struct
        {
            int size = Marshal.SizeOf(typeof(T));
            fixed (byte* ib = new byte[size])
            {
                IntPtr ibPtr = new IntPtr(ib);
                byte[] arrayOfBytes = new byte[size];
                Marshal.StructureToPtr(input, ibPtr, true);
                Marshal.Copy(ibPtr, arrayOfBytes, 0, size);
                WriteMemoryArray(adress, arrayOfBytes);
            }
        }
        public static IntPtr WriteMemoryArray(uint pOffset, byte[] pBytes)
        {
            IntPtr n = IntPtr.Zero;
            ExternalUtilsCSharp.WinAPI.WriteProcessMemory(pHandle, (IntPtr)pOffset, pBytes, pBytes.Length, out n);
            return n;
        }

        static void work()
        {
            Thread.Sleep(1000);
            mWnd.AppendLogT("Thread is ok!");
            mWnd.SetStatusD("Waiting for speed.exe");
            bool found = false;

            while (true)
            {
                foreach (var p in Process.GetProcesses())
                {
                    if (p.ProcessName.StartsWith("speed")) found = true;
                }
                if (found) break;
                try
                {
                    if (int.Parse(Funcs.ProgCfg.Settings["startGame"]) == 1)
                    {
                        Process.Start(new ProcessStartInfo() { FileName = Funcs.ProgCfg.Settings["gamePath"], WorkingDirectory = Funcs.ProgCfg.Settings["gameDir"] });

                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                Thread.Sleep(100);
            }

            mWnd.SetStatusD("Hacking NFS...");
            while (true)
            {
                DC.WriteLine("check 1");
                try
                {
                    if (File.Exists(GetWD(Process.GetProcessesByName("speed").FirstOrDefault().MainModule.FileName) + "\\d3d9.dll"))
                    {
                        System.Windows.MessageBox.Show("MW Debug is not compatible with ENB/ModLoader/ReShade or another mods that hooking DirectX\n\nRemove d3d9.dll from the game folder", "MW Debug Compatibility Error");
                        Environment.Exit(0);
                    }
                    break;
                }
                catch { }
            }
            Thread.Sleep(1000);


            Process[] proc = Process.GetProcessesByName("speed");
            m = new MemorySharp(proc[0].Id);
            pHandle = proc[0].Handle;

            mWnd.AppendLogT("Setting new splash");
            m.WriteString(Info.FNG.MW_LS_Splash, Funcs.ProgCfg.Settings["splashFNG"], false);


            mWnd.AppendLogT("Setting new loading screen");
            if (Funcs.ProgCfg.Settings["loading"] == "0")
            {
                m.WriteString((IntPtr)0x8A85F8, "", false);//PC_Loading.fng
                m.WriteString((IntPtr)0x89F814, "", false);//loading_tips.fng
                m.WriteString((IntPtr)0x89E7A0, "", false);//Loading.fng
            }
            else if (Funcs.ProgCfg.Settings["loading"] == "2")
            {
                m.WriteString((IntPtr)0x8A85F8, "loading_boot.fng", false);//PC_Loading.fng
                m.WriteString((IntPtr)0x89F814, "loading_boot.fng", false);//loading_tips.fng
                m.WriteString((IntPtr)0x89E7A0, "loading_boot.fng", false);//Loading.fng
            }


            new Thread(SetFE).Start();


            int maxO = 41;
            dxset(maxO);
            Version v = System.Reflection.Assembly.GetExecutingAssembly().GetName().Version;
            debugLog += String.Format(" {0}.{1}.{2}", v.Major, v.Minor, v.Build);
            if (v.Revision >= 1) debugLog += " {FF0000}[TEST BUILD " + v.Revision + "]";

            float x = 0;
            float y = 0;
            float z = 0;
            int id;
            List<int> nearest = null;

            int tmp = 0;
            string taskName;

            mWnd.AppendLogT("Starting main loop");
            mWnd.SetStatusD("Working");
            if (mWnd.GridWelcome.Visibility == Visibility.Visible)
            {
                mWnd.SetActiveGridT(mWnd.GridLog);
            }
            working = true;
            while (true)
            {
                while (true)
                {
                    if (m.Windows.MainWindow.IsActivated || workMin)
                    {
                        id = 0;
                        if (!Replays.Recording)
                        {
                            if (CordLogEnabled)
                            {
                                while (id != maxO)
                                {
                                    if (id == 0)
                                    {
                                        x = readMem<float>((IntPtr)Info.Object.X);
                                        y = readMem<float>((IntPtr)Info.Object.Y);
                                        z = readMem<float>((IntPtr)Info.Object.Z);
                                    }
                                    else
                                    {
                                        x = readMem<float>((IntPtr)(Info.Object.X + (Info.pOffset * id)));
                                        y = readMem<float>((IntPtr)(Info.Object.Y + (Info.pOffset * id)));
                                        z = readMem<float>((IntPtr)(Info.Object.Z + (Info.pOffset * id)));
                                    }
                                    if ((x == 0 && y == 0 && z == 0) || (showCoords == false && id >= 5)) DX9Overlay.TextSetString(id, " ");
                                    else DX9Overlay.TextSetString(id, "[ID: {FFFF00}" + id + "{FFFFFF}] \tX: {FF6347}" + x + "{FFFFFF} \tY: {9ACD32}" + y + "{FFFFFF} \tZ: {33AAFF}" + z);

                                    x = readMem<float>((IntPtr)Info.Camera.X);
                                    y = readMem<float>((IntPtr)Info.Camera.Y) * -1;
                                    z = readMem<float>((IntPtr)Info.Camera.Z);
                                    DX9Overlay.TextSetString(maxO /* bad idea */, "[{FFFF00}Camera{FFFFFF}] \tX: {FF6347}" + x + "{FFFFFF} \tY: {9ACD32}" + y + "{FFFFFF} \tZ: {33AAFF}" + z);
                                    id++;
                                }
                            }
                            GameSlider.Update();
                            PscShown(PscShow);
                            DX9Overlay.TextSetString(idGameSpeed, string.Concat(new object[] { "{FFFF00}", DX9Overlay.GetFrameRate(), "{FFFFFF} FPS | Game speed: {FFFF00}", GameSpeed.Get(m), "{FFFFFF} | Game state: {FFFF00}", readMem<int>((IntPtr)0x925e90), "{FFFFFF} | ", debugLog }));
                            switch (task)
                            {
                                case -2:
                                    taskName = "Tha sicret fanction";
                                    break;

                                case -1:
                                    taskName = ":p ^";
                                    break;

                                case 0:
                                    taskName = "Attach player to object";
                                    break;

                                case 1:
                                    taskName = "Attach object to camera";
                                    break;

                                case 2:
                                    taskName = "Spinner party";
                                    break;

                                case 3:
                                    taskName = "Tor's Hammer";
                                    break;

                                case 4:
                                    taskName = "Freeze object";
                                    break;

                                case 5:
                                    taskName = "Remove object";
                                    break;

                                case 6:
                                    taskName = "Gravity party";
                                    break;

                                case 7:
                                    taskName = "Start pursuit";
                                    break;

                                case 8:
                                    taskName = "Jump to safe house";
                                    break;

                                case 9:
                                    taskName = "Force reload world";
                                    break;

                                case 10:
                                    taskName = "Start free roam from FE";
                                    break;

                                case 11:
                                    taskName = "Call custom function";
                                    break;

                                case 12:
                                    taskName = "Cords: Save to file";
                                    break;

                                case 13:
                                    taskName = "Cords: Add checkpoint";
                                    break;

                                case 14:
                                    taskName = "Object props: Set weight";
                                    break;

                                case 15:
                                    taskName = "Rigidbody: Change AddForce power";
                                    break;

                                case 0x10:
                                    taskName = "Comparator: Update";
                                    break;

                                case 0x11:
                                    if (!BustedF)
                                    {
                                        taskName = "Never Busted: {9ACD32}Enable";
                                        break;
                                    }
                                    taskName = "Never Busted: {FF6347}Disable";
                                    break;

                                case 0x12:
                                    taskName = "Disable car reset";
                                    break;

                                case 0x13:
                                    taskName = "Disable world collision";
                                    break;

                                case 20:
                                    taskName = "Replay: Record";
                                    break;

                                case 0x15:
                                    taskName = "Replay: Play";
                                    break;

                                case 0x16:
                                    taskName = "Cutscene Camera > Default";
                                    break;

                                case 0x17:
                                    taskName = "Cutscene Camera > Drive";
                                    break;

                                case 0x18:
                                    taskName = "Cutscene Camera > Debug";
                                    break;

                                case 0x19:
                                    taskName = "Watch car camera (nlgzrgn, osdever)";
                                    break;

                                case 0x1a:
                                    taskName = "Start BooBoo script";
                                    break;

                                case 0x1b:
                                    taskName = "Toggle NIS";
                                    break;

                                case TaskMan.Tasks.EnableHeadlights:
                                    taskName = "Enable Headlights";
                                    break;

                                default:
                                    taskName = "----";
                                    break;
                            }
                            if (doTask)
                            {
                                taskName = string.Concat(new object[] { "{9ACD32}[", task, "] ", taskName });
                            }
                            else
                            {
                                taskName = string.Concat(new object[] { "{FFC488}[", task, "] ", taskName });
                            }
                            if (idSelected >= 0)
                            {
                                x = readMem<float>(Info.Object.X + GetOffsetId(idSelected));
                                y = readMem<float>(Info.Object.Y + GetOffsetId(idSelected));
                                z = readMem<float>(Info.Object.Z + GetOffsetId(idSelected));
                                DX9Overlay.TextSetString(idText, string.Concat(new object[] { taskName, "{FFFFFF} | Selected object ID: {FFFF00}", idSelected, "{FFFFFF} (X: {FF6347}", x, "{FFFFFF} Y: {9ACD32}", y, "{FFFFFF} Z: {33AAFF}", z, "{FFFFFF})" }));
                            }
                            else if (idSelected == -1)
                            {
                                DX9Overlay.TextSetString(idText, taskName + "{FFFFFF} | Selected all objects (-1)");
                            }
                            else if (idSelected == -2)
                            {
                                nearest = GetNearestObject(readMem<float>(Info.Camera.X), readMem<float>(Info.Camera.Y) * -1f, readMem<float>(Info.Camera.Z), 30f);
                                if (nearest == null)
                                {
                                    DX9Overlay.TextSetString(idText, taskName + "{FFFFFF} | Selected nearest objects (-2) {FF6347}can't find nearest");
                                }
                                else
                                {
                                    string str2 = "";
                                    foreach (int num7 in nearest)
                                    {
                                        str2 = str2 + num7.ToString() + " ";
                                    }
                                    DX9Overlay.TextSetString(idText, taskName + "{FFFFFF} | Selected nearest objects (-2) {FFFF00}" + str2);
                                }
                            }
                        }
                        if (doTask)
                        {
                            if (task == -7)
                            {
                                /*.text:00569CC0 sub_569CC0      proc near               ; CODE XREF: sub_54EDA0:loc_54EE87↑p
.text:00569CC0                                         ; sub_54EF70+69↑p ...
.text:00569CC0                 mov     ecx, _5cFEng$mInstance ; cFEng::mInstance
.text:00569CC6                 push    offset aEa_trax_fng ; "EA_Trax.fng"
.text:00569CCB                 call    IsPackagePushed__5cFEngPCc ; cFEng::IsPackagePushed(char const *)
.text:00569CD0                 test    al, al
.text:00569CD2                 jz      short locret_569CE4
.text:00569CD4                 mov     ecx, _5cFEng$mInstance ; cFEng::mInstance
.text:00569CDA                 push    offset aEa_trax_fng ; "EA_Trax.fng"
.text:00569CDF                 call    sub_5169B0
.text:00569CE4
.text:00569CE4 locret_569CE4:                          ; CODE XREF: sub_569CC0+12↑j
.text:00569CE4                 retn*/
                                int feptr = readMem<int>((IntPtr)0x91CADC);
                                IntPtr isPackagePushedFunc = (IntPtr)0x516B50;
                                IntPtr wFunc2 = (IntPtr)0x5169B0;
                                IntPtr PushNoControlPackage = (IntPtr)0x516990;
                                string fngName = "DiscErrorPC.fng";
                                var alloc = m.Memory.Allocate(fngName.Length);
                                alloc.WriteString(fngName);
                                IntPtr nameAlloc = alloc.BaseAddress;
                                string Inject1 = String.Format("", feptr, nameAlloc, isPackagePushedFunc);
                                string Inject2 = Inject1 + String.Format("mov ecx, {0}\npush {1}\ncall {2}\nmov ecx, {0}\npush 64h\npush {1}\ncall {3}\nretn", feptr, nameAlloc, isPackagePushedFunc, PushNoControlPackage);
                                mWnd.AppendLogT(Inject2);
                                m.Assembly.InjectAndExecute(Inject2);
                                // int result = m[(IntPtr)0x5BBB40, false].Execute<int>(Binarysharp.MemoryManagement.Assembly.CallingConvention.CallingConventions.Thiscall, feptr, nameAlloc, 0, 0, 0, 0);
                              //  mWnd.AppendLogT("String adr: 0x" + nameAlloc.ToString("X4"));
                              // mWnd.AppendLogT("Result: " + result);
                             //  SetLog("Result: " + result);
                               // mWnd.AppendLogT("Done");
                                doTask = false;
                            }
                            if (task == 0x1a)
                            {
                                CustomHoo.StartHoo();
                                doTask = false;
                            }
                            if (task == 0x13)
                            {
                                writeMem<float>((IntPtr)0x8933d4, 0.499f);
                                SetLog("Done. {FFFF00}Press Ctrl+Q to enable collision again.");
                                doTask = false;
                            }
                            if(task == TaskMan.Tasks.EnableHeadlights)
                            {
                                m.Assembly.Inject("nop", (IntPtr)0x7429E0);
                                m.Assembly.Inject("nop", (IntPtr)0x7429E1);
                                SetLog("{00FF00}Done");
                                doTask = false;
                            }
                            if (task == 0x1b)
                            {
                                IntPtr address = (IntPtr)0x91e140;
                                if (readMem<int>(address) == 0)
                                {
                                    SetLog("{FFFF00}NIS disabled");
                                    writeMem<int>(address, 1);
                                }
                                else
                                {
                                    SetLog("{FFFF00}NIS enabled");
                                    writeMem<int>(address, 0);
                                }
                                doTask = false;
                            }
                            if (task == 0x12)
                            {
                                InsertNop((IntPtr)0x6a6ffa);
                                InsertNop((IntPtr)0x6a6ffb);
                                InsertNop((IntPtr)0x6a6fff);
                                InsertNop((IntPtr)0x6a7000);
                                InsertNop((IntPtr)0x6a7001);
                                InsertNop((IntPtr)0x6a7005);
                                InsertNop((IntPtr)0x6a7006);
                                InsertNop((IntPtr)0x6a7007);
                                InsertNop((IntPtr)0x696fde);
                                InsertNop((IntPtr)0x696fdf);
                                InsertNop((IntPtr)0x696fe3);
                                InsertNop((IntPtr)0x696fe4);
                                InsertNop((IntPtr)0x696fe5);
                                InsertNop((IntPtr)0x696fe9);
                                InsertNop((IntPtr)0x696fea);
                                InsertNop((IntPtr)0x696feb);
                                InsertNop((IntPtr)0x6a6fe3);
                                InsertNop((IntPtr)0x6a6fe4);
                                InsertNop((IntPtr)0x6a6fe5);
                                InsertNop((IntPtr)0x6a6fe6);
                                InsertNop((IntPtr)0x6a6fe7);
                                InsertNop((IntPtr)0x6a6fe8);
                                SetLog("Done");
                                doTask = false;
                            }
                            if (task == -4)
                            {
                                doTask = false;
                            }
                            if (task == -3)
                            {
                                m.Assembly.Inject("mov eax, 0x1B", (IntPtr)0x62d5b0);
                                doTask = false;
                            }
                            if (task == 0x11)
                            {
                                if (!BustedF)
                                {
                                    BustedT = new System.Timers.Timer(1000.0);
                                    BustedT.Elapsed += BustedT_Elapsed;
                                    BustedT.Start();
                                }
                                else
                                {
                                    BustedT.Dispose();
                                }
                                SetLog("Done");
                                BustedF = !BustedF;
                                doTask = false;
                            }
                            if (task == 0x16)
                            {
                                m.WriteString(((IntPtr)0x894cbc) - 0x400000, "CDActionIce", true);
                                SetLog("Done");
                                doTask = false;
                            }
                            if (task == 0x17)
                            {
                                m.WriteString(((IntPtr)0x894cbc) - 0x400000, "CDActionDrive", true);
                                SetLog("Done");
                                doTask = false;
                            }
                            if (task == 0x18)
                            {
                                m.WriteString(((IntPtr)0x894cbc) - 0x400000, "CDActionDebug", true);
                                SetLog("Done");
                                doTask = false;
                            }
                            if (task == 0x10)
                            {
                                SetLog("Due the project recovering function was remoed.");
                                doTask = false;
                            }
                            if (task == -5)
                            {
                                writeMem<float>(Info.Object.X, readMem<float>(Info.Object.X + GetOffsetId(idSelected)));
                                writeMem<float>(Info.Object.Y, readMem<float>(Info.Object.Y + GetOffsetId(idSelected)));
                                writeMem<float>(Info.Object.Z, readMem<float>(Info.Object.Z + GetOffsetId(idSelected)));
                                writeMem<float>((IntPtr)0x9386fc, 1000f);
                            }
                            if (task == -2)
                            {
                                SetLog("{00FF00}Bad idea :0");
                                writeMem<float>((IntPtr)0x8b5b18, 20f);
                                writeMem<float>((IntPtr)0x891654, 444f);
                                writeMem<float>((IntPtr)0x8b5b04, -1f);
                                float num8 = -44f;
                                writeMem<float>(Info.GameSpeed, 0.01f);
                                while (num8 <= 0.4f)
                                {
                                    writeMem<float>((IntPtr)0x8933d4, num8);
                                    num8 += 0.1f;
                                    Thread.Sleep(4);
                                }
                                writeMem<float>(Info.GameSpeed, 1.1f);
                                writeMem<float>((IntPtr)0x8933d4, 0.35f);
                                doTask = false;
                                m.WriteString((IntPtr)0x8912f0, "AIActionRace", false);
                            }
                            if (task == 0)
                            {
                                if (idSelected >= 1)
                                {
                                    writeMem<float>(Info.Object.X, readMem<float>(Info.Object.X + GetOffsetId(idSelected)));
                                    writeMem<float>(Info.Object.Y, readMem<float>(Info.Object.Y + GetOffsetId(idSelected)));
                                    writeMem<float>(Info.Object.Z, readMem<float>(Info.Object.Z + GetOffsetId(idSelected)) + 2f);
                                    writeMem<float>(Info.Object.Vel1, 0f);
                                    writeMem<float>(Info.Object.Vel2, 0f);
                                    writeMem<float>(Info.Object.Vel3, 0f);
                                }
                                else if (idSelected >= -1)
                                {
                                    SetLog("{FF6347}ERROR: {FFFFFF}You can't attach your vehicle to you/all objects");
                                    doTask = false;
                                }
                                else if ((idSelected == -2) && (nearest != null))
                                {
                                    writeMem<float>(Info.Object.X, readMem<float>(Info.Object.X + GetOffsetId(nearest[0])));
                                    writeMem<float>(Info.Object.Y, readMem<float>(Info.Object.Y + GetOffsetId(nearest[0])));
                                    writeMem<float>(Info.Object.Z, readMem<float>(Info.Object.Z + GetOffsetId(nearest[0])) + 2f);
                                    writeMem<float>(Info.Object.Vel1, 0f);
                                    writeMem<float>(Info.Object.Vel2, 0f);
                                    writeMem<float>(Info.Object.Vel3, 0f);
                                }
                            }
                            if (task == 1)
                            {
                                if (idSelected >= 0)
                                {
                                    writeMem<float>(Info.Object.X + GetOffsetId(idSelected), readMem<float>((IntPtr)0x9868b0));
                                    writeMem<float>(Info.Object.Y + GetOffsetId(idSelected), readMem<float>((IntPtr)0x9868b4) * -1f);
                                    writeMem<float>(Info.Object.Z + GetOffsetId(idSelected), readMem<float>((IntPtr)0x9868b8));
                                    writeMem<float>(Info.Object.Vel1 + GetOffsetId(idSelected), 0f);
                                    writeMem<float>(Info.Object.Vel2 + GetOffsetId(idSelected), 0f);
                                    writeMem<float>(Info.Object.Vel3 + GetOffsetId(idSelected), 0f);
                                }
                                else if (idSelected == -1)
                                {
                                    for (id = 1; id != 40; id++)
                                    {
                                        writeMem<float>(Info.Object.X + GetOffsetId(id), readMem<float>((IntPtr)0x9868b0));
                                        writeMem<float>(Info.Object.Y + GetOffsetId(id), readMem<float>((IntPtr)0x9868b4) * -1f);
                                        writeMem<float>(Info.Object.Z + GetOffsetId(id), readMem<float>((IntPtr)0x9868b8));
                                        writeMem<float>(Info.Object.Vel1 + GetOffsetId(id), 0f);
                                        writeMem<float>(Info.Object.Vel2 + GetOffsetId(id), 0f);
                                        writeMem<float>(Info.Object.Vel3 + GetOffsetId(id), 0f);
                                    }
                                }
                                else if ((idSelected == -2) && (nearest != null))
                                {
                                    foreach (int num9 in nearest)
                                    {
                                        writeMem<float>(Info.Object.X + GetOffsetId(num9), readMem<float>((IntPtr)0x9868b0));
                                        writeMem<float>(Info.Object.Y + GetOffsetId(num9), readMem<float>((IntPtr)0x9868b4) * -1f);
                                        writeMem<float>(Info.Object.Z + GetOffsetId(num9), readMem<float>((IntPtr)0x9868b8));
                                        writeMem<float>(Info.Object.Vel1 + GetOffsetId(num9), 0f);
                                        writeMem<float>(Info.Object.Vel2 + GetOffsetId(num9), 0f);
                                        writeMem<float>(Info.Object.Vel3 + GetOffsetId(num9), 0f);
                                    }
                                }
                            }
                            if (task == 2)
                            {
                                if (idSelected >= 0)
                                {
                                    writeMem<float>((IntPtr)(0x9386fc + GetOffsetId(idSelected)), 1000f);
                                }
                                else if (idSelected == -1)
                                {
                                    for (id = 1; id != 40; id++)
                                    {
                                        writeMem<float>((IntPtr)(0x9386fc + GetOffsetId(id)), 1000f);
                                    }
                                }
                                else if ((idSelected == -2) && (nearest != null))
                                {
                                    foreach (int num10 in nearest)
                                    {
                                        writeMem<float>((IntPtr)(0x9386fc + GetOffsetId(num10)), 1000f);
                                    }
                                }
                            }
                            if (task == 4)
                            {
                                if (idSelected >= 0)
                                {
                                    writeMem<float>(Info.Object.Vel1 + GetOffsetId(idSelected), 0f);
                                    writeMem<float>(Info.Object.Vel2 + GetOffsetId(idSelected), 0f);
                                    writeMem<float>(Info.Object.Vel3 + GetOffsetId(idSelected), 0f);
                                    writeMem<float>((IntPtr)(0x9386f8 + GetOffsetId(idSelected)), 0f);
                                    writeMem<float>((IntPtr)(0x938700 + GetOffsetId(idSelected)), 0f);
                                }
                                else if (idSelected == -1)
                                {
                                    for (id = 1; id != 40; id++)
                                    {
                                        writeMem<float>(Info.Object.Vel1 + GetOffsetId(id), 0f);
                                        writeMem<float>(Info.Object.Vel2 + GetOffsetId(id), 0f);
                                        writeMem<float>(Info.Object.Vel3 + GetOffsetId(id), 0f);
                                        writeMem<float>((IntPtr)(0x9386f8 + GetOffsetId(id)), 0f);
                                        writeMem<float>((IntPtr)(0x938700 + GetOffsetId(id)), 0f);
                                    }
                                }
                                else if ((idSelected == -2) && (nearest != null))
                                {
                                    foreach (int num11 in nearest)
                                    {
                                        writeMem<float>(Info.Object.Vel1 + GetOffsetId(num11), 0f);
                                        writeMem<float>(Info.Object.Vel2 + GetOffsetId(num11), 0f);
                                        writeMem<float>(Info.Object.Vel3 + GetOffsetId(num11), 0f);
                                        writeMem<float>((IntPtr)(0x9386f8 + GetOffsetId(num11)), 0f);
                                        writeMem<float>((IntPtr)(0x938700 + GetOffsetId(num11)), 0f);
                                    }
                                }
                            }
                            if (task == 15)
                            {
                                PscPower = GameSlider.Value;
                                SetLog("Rigidbody AddForce Power was changed to: " + PscPower);
                                doTask = false;
                            }
                            if (task == 14)
                            {
                                float num12 = GameSlider.Value;
                                if (idSelected == 0)
                                {
                                    writeMem<float>(Info.Object.Mass, num12);
                                }
                                else if (idSelected >= 1)
                                {
                                    writeMem<float>(Info.Object.Mass + GetOffsetId(idSelected), num12);
                                }
                                else if (idSelected == -1)
                                {
                                    for (id = 1; id != 40; id++)
                                    {
                                        writeMem<float>(Info.Object.Mass + GetOffsetId(id), num12);
                                    }
                                }
                                else if ((idSelected == -2) && (nearest != null))
                                {
                                    foreach (int num13 in nearest)
                                    {
                                        writeMem<float>(Info.Object.Mass + GetOffsetId(num13), num12);
                                    }
                                }
                                SetLog("New object weight: " + num12);
                                doTask = false;
                            }
                            if (task == 5)
                            {
                                if (idSelected == 0)
                                {
                                    SetLog("{FF6347}ERROR:{FFFFFF} You can't remove your own vehicle");
                                    doTask = false;
                                }
                                else if (idSelected >= 1)
                                {
                                    writeMem<float>(Info.Object.X + GetOffsetId(idSelected), 0f);
                                    writeMem<float>(Info.Object.Y + GetOffsetId(idSelected), 0f);
                                    writeMem<float>(Info.Object.Z + GetOffsetId(idSelected), 0f);
                                }
                                else if (idSelected == -1)
                                {
                                    for (id = 1; id != 40; id++)
                                    {
                                        writeMem<float>(Info.Object.X + GetOffsetId(id), 0f);
                                        writeMem<float>(Info.Object.Y + GetOffsetId(id), 0f);
                                        writeMem<float>(Info.Object.Z + GetOffsetId(id), 0f);
                                    }
                                }
                                else if ((idSelected == -2) && (nearest != null))
                                {
                                    foreach (int num14 in nearest)
                                    {
                                        writeMem<float>(Info.Object.X + GetOffsetId(num14), 0f);
                                        writeMem<float>(Info.Object.Y + GetOffsetId(num14), 0f);
                                        writeMem<float>(Info.Object.Z + GetOffsetId(num14), 0f);
                                    }
                                }
                            }
                            if (task == 6)
                            {
                                if (idSelected >= 0)
                                {
                                    writeMem<float>(Info.Object.Vel2 + GetOffsetId(idSelected), -100f);
                                }
                                else if (idSelected == -1)
                                {
                                    for (id = 1; id != 40; id++)
                                    {
                                        writeMem<float>(Info.Object.Vel2 + GetOffsetId(id), -100f);
                                    }
                                }
                                else if ((idSelected == -2) && (nearest != null))
                                {
                                    foreach (int num15 in nearest)
                                    {
                                        writeMem<float>(Info.Object.Vel2 + GetOffsetId(num15), -100f);
                                    }
                                }
                            }
                            if (task == 3)
                            {
                                float num16 = 200f;
                                float num17 = -10000f;
                                if (idSelected >= 0)
                                {
                                    writeMem<float>(Info.Object.Z + GetOffsetId(idSelected), readMem<float>(Info.Object.Z + GetOffsetId(idSelected)) + num16);
                                    writeMem<float>(Info.Object.Vel2 + GetOffsetId(idSelected), num17);
                                }
                                else if (idSelected == -1)
                                {
                                    for (id = 1; id != 40; id++)
                                    {
                                        writeMem<float>(Info.Object.Z + GetOffsetId(id), readMem<float>(Info.Object.Z + GetOffsetId(id)) + num16);
                                        writeMem<float>(Info.Object.Vel2 + GetOffsetId(id), num17);
                                    }
                                }
                                else if ((idSelected == -2) && (nearest != null))
                                {
                                    foreach (int num18 in nearest)
                                    {
                                        writeMem<float>(Info.Object.Z + GetOffsetId(num18), readMem<float>(Info.Object.Z + GetOffsetId(num18)) + num16);
                                        writeMem<float>(Info.Object.Vel2 + GetOffsetId(num18), num17);
                                    }
                                }
                                doTask = false;
                            }
                            if (task == 7)
                            {
                                m[((IntPtr)0x60aac0) - 0x400000, true].Execute<int>();
                                doTask = false;
                                SetLog("Starting pursuit");
                            }
                            if (task == 8)
                            {
                                m[((IntPtr)0x6052b0) - 0x400000, true].Execute<int>();
                                doTask = false;
                                SetLog("Jumping to safehouse");
                            }
                            if (task == 10)
                            {
                                m[((IntPtr)0x56c5b0) - 0x400000, true].Execute<int>();
                                doTask = false;
                                SetLog("Starting free roam");
                            }
                            if (task == 11)
                            {
                                mWnd.Dispatcher.BeginInvoke(new Action(delegate { mWnd.CallMemBtn_Click(null, null); }));
                                doTask = false;
                            }
                            if (task == 20)
                            {
                                Replays.AddInfo(new ReplayInfo(readMem<float>(Info.Object.X), readMem<float>(Info.Object.Y), readMem<float>(Info.Object.Z), readMem<float>(Info.Object.RotX), readMem<float>(Info.Object.RotY), readMem<float>(Info.Object.RotZ), readMem<float>(Info.Object.RotW)));
                                DX9Overlay.TextSetString(idGameSpeed, "{FF0000}" + Replays.rInfo.Count + " FRAMES RECORDED, PRESS INSERT TO STOP");
                            }
                            if (task == 0x19)
                            {
                                if (idSelected <= 0)
                                {
                                    m.WriteString((IntPtr)0x894c20, "CDActionDrive", false);
                                    m[((IntPtr)0x47cc50) - 0x400000, true].Execute();
                                }
                                else
                                {
                                    writeMem<byte>((IntPtr)0x911038, 1);
                                    writeMem<byte>((IntPtr)0x911058, (byte)idSelected);
                                    var allocation = m.Memory.Allocate("CDActionDebugWatchCar".Length);
                                    allocation.WriteString("CDActionDebugWatchCar");
                                    writeMem<int>((IntPtr)0x47cc79, allocation.BaseAddress.ToInt32());
                                    writeMem<int>((IntPtr)0x47cc9b, allocation.BaseAddress.ToInt32());
                                    writeMem<int>((IntPtr)0x47cc28, allocation.BaseAddress.ToInt32());
                                    m[((IntPtr)0x47cc50) - 0x400000, true].Execute();
                                    writeMem<int>((IntPtr)0x47cc79, 0x894c20);
                                    writeMem<int>((IntPtr)0x47cc9b, 0x894c20);
                                    writeMem<int>((IntPtr)0x47cc28, 0x894c20);
                                }
                                SetLog("Done");
                                doTask = false;
                            }
                            if (task == 0x15)
                            {
                                SetLog(string.Concat(new object[] { "Replay frame: ", CurrentReplayFrame, "/", Replays.rInfo.Count, " [+", Replays.Double, "]" }));
                                try
                                {
                                    writeMem<float>(Info.Object.X + GetOffsetId(idSelected), Replays.rInfo[CurrentReplayFrame].X);
                                    writeMem<float>(Info.Object.Y + GetOffsetId(idSelected), Replays.rInfo[CurrentReplayFrame].Y);
                                    writeMem<float>(Info.Object.Z + GetOffsetId(idSelected), Replays.rInfo[CurrentReplayFrame].Z);
                                    writeMem<float>(Info.Object.RotX + GetOffsetId(idSelected), Replays.rInfo[CurrentReplayFrame].RotationX);
                                    writeMem<float>(Info.Object.RotZ + GetOffsetId(idSelected), Replays.rInfo[CurrentReplayFrame].RotationZ);
                                    writeMem<float>(Info.Object.RotW + GetOffsetId(idSelected), Replays.rInfo[CurrentReplayFrame].RotationW);
                                }
                                catch
                                {
                                }
                                CurrentReplayFrame += Replays.Double;
                                if (CurrentReplayFrame >= Replays.rInfo.Count)
                                {
                                    CurrentReplayFrame = 0;
                                }
                            }
                            if (task == 12)
                            {
                                CordsSaver.SaveToFile("cords.txt");
                                SetLog("{00FF00}SAVED TO cords.txt");
                                doTask = false;
                            }
                            if (task == 13)
                            {
                                CordsSaver.AddCheckpointInfo(idSelected);
                                SetLog("Cord added, total: " + CordsSaver.checkpoints.Count);
                                doTask = false;
                            }
                            if (task == 9)
                            {
                                if (readMem<int>(Info.GameState) == 5)
                                {
                                    writeMem<int>(Info.GameState, 6);
                                }
                                m[((IntPtr)0x6057e0) - 0x400000, true].Execute<int>();
                                doTask = false;
                                SetLog("Reloading world");
                            }


                        } 
                        fps = DX9Overlay.GetFrameRate();
                        try
                        {
                            int millisecondsTimeout = (0x3e8 / fps) + thrSleep;
                            Thread.Sleep(millisecondsTimeout);
                        }
                        catch
                        {
                            Thread.Sleep(500);
                            if (!CheckMW()) return;
                        }

                    }
                    else
                    {
                        Thread.Sleep(1000);
                        if (!CheckMW()) return;
                    }

                }
            }
        }
        public static bool CheckMW()
        {
            if (Process.GetProcessesByName("speed").Length == 0)
            {
                mWnd.ErrorT("CRITICAL ERROR", "NFS MW is not running\r\nRIP speed.exe 2005-2018", true);
                return false;
            }
            return true;
        }
        public static void CRB(IntPtr v, float p)
        {
            if (idSelected >= 0)
            {
                writeMem<float>(v + GetOffsetId(idSelected), p);
            }
            else if (idSelected == -1)
            {
                for (int i = 1; i != 40; i++)
                {
                    writeMem<float>(v + GetOffsetId(i), p);
                }
            }
            else if (idSelected == -2)
            {
                SetLog("{FF0000}ERROR: Can't use ID -2, sry plz ok");
            }
        }

        public void DropToWorldBug()
        {
            if (readMem<int>(Info.GameState) == 6)
            {
                m[((IntPtr)0x6052b0) - 0x400000, true].Execute<int>();
            }
            while (readMem<int>(Info.GameState) != 3)
            {
                Thread.Sleep(500);
            }
            writeMem<int>(Info.GameState, 1);
            Thread.Sleep(200);
            m[((IntPtr)0x56c5b0) - 0x400000, true].Execute<int>();
            while (readMem<int>(Info.GameState) != 6)
            {
                Thread.Sleep(500);
            }
        }


        public static void KeyUp(KeyboardHookEventArgs e)
        {
            if(e.Key == Keys.F1 && e.isLAltPressed)
            {
                mWnd.PlakImage.Visibility = Visibility.Visible;
                mWnd.GridTopColor.Opacity = 0.2;
                mWnd.GridButtons.Opacity = 0.5;
            }
            if (working && m.Windows.MainWindow.IsActivated)
            {
                if(e.Key == Keys.Tab)
                {
                    IntPtr adr = (IntPtr)0x911020;
                    if (readMem<int>(adr) != 1)
                    {
                        writeMem<int>(adr, 1);
                        SetLog("{00FF00}Camera frozen");
                    }
                    else
                    {
                        writeMem<int>(adr, 0);
                        SetLog("{FFFF00}Camera unfrozen");
                    }
                }
                if ((e.Key == Keys.Q) && e.isCtrlPressed)
                {
                    m.WriteString((IntPtr)0x894c20, "CDActionDrive", false);
                    m[((IntPtr)0x632190) - 0x400000, true].Execute();
                    m[((IntPtr)0x47cc50) - 0x400000, true].Execute();
                    writeMem<float>((IntPtr)0x8933d4, 0.5f);
                    SetLog("Unpaused");
                }
                if ((e.Key == Keys.D1) && e.isCtrlPressed)
                {
                    writeMem<int>(Info.GameState, 1);
                }
                if ((e.Key == Keys.D3) && e.isCtrlPressed)
                {
                    writeMem<int>(Info.GameState, 3);
                }
                if ((e.Key == Keys.D6) && e.isCtrlPressed)
                {
                    writeMem<int>(Info.GameState, 6);
                }
                if (((e.Key == Keys.Oemplus) && !e.isRAltPressed) && !e.isRCtrlPressed)
                {
                    GameSlider.Increase(1);
                }
                else if (((e.Key == Keys.OemMinus) && !e.isRAltPressed) && !e.isRCtrlPressed)
                {
                    GameSlider.Decrease(1);
                }
                else if ((e.Key == Keys.Oemplus) && e.isRAltPressed)
                {
                    GameSlider.Increase(200);
                }
                else if ((e.Key == Keys.OemMinus) && e.isRAltPressed)
                {
                    GameSlider.Decrease(200);
                }
                else if ((e.Key == Keys.Oemplus) && e.isRCtrlPressed)
                {
                    GameSpeed.Increase(m);
                    SetLog("Increase game speed");
                }
                else if ((e.Key == Keys.OemMinus) && e.isRCtrlPressed)
                {
                    GameSpeed.Decrease(m);
                    SetLog("Decrease game speed");
                }
                else if ((e.Key == Keys.Oem4) && e.isRCtrlPressed)
                {
                    GameSpeed.Set(m, 0f);
                    SetLog("Set game speed to 0");
                }
                else if ((e.Key == Keys.Oem6) && e.isRCtrlPressed)
                {
                    GameSpeed.Set(m, 0.1f);
                    SetLog("Slowmo");
                }
                else if ((e.Key == Keys.P) && e.isRCtrlPressed)
                {
                    GameSpeed.Set(m, 1f);
                    SetLog("Normal game speed");
                }
                else if (e.isRAltPressed && ((((e.Key == Keys.U) || (e.Key == Keys.J)) || ((e.Key == Keys.H) || (e.Key == Keys.I))) || (((e.Key == Keys.K) || (e.Key == Keys.Y)) || (e.Key == Keys.N))))
                {
                    RBForce(e);
                }
                else if ((e.Key == Keys.V) && e.isAltPressed)
                {
                    string str = m.ReadString(((IntPtr)0x894c20) - 0x400000, true, 0x200);
                    if (str.StartsWith("CDActionDrive"))
                    {
                        SetLog("[P] Drive > Showcase");
                        m.WriteString(((IntPtr)0x894c20) - 0x400000, "CDActionShowcase", true);
                        m[((IntPtr)0x47cc50) - 0x400000, true].Execute();
                    }
                    else if (str.StartsWith("CDActionShowcase"))
                    {
                        SetLog("[P] Showcase > Drive");
                        m.WriteString(((IntPtr)0x894c20) - 0x400000, "CDActionDrive", true);
                        m[((IntPtr)0x47cc50) - 0x400000, true].Execute();
                    }
                    else
                    {
                        SetLog("{FF0000}Error: {00FF00}Not compatible with other mods");
                    }
                }
                else if ((e.Key == Keys.V) && !e.isAltPressed)
                {
                    if (visibility == 0)
                    {
                        DX9Overlay.HideAllVisual();
                        visibility++;
                    }
                    else if (visibility == 1)
                    {
                        hudX = readMem<float>((IntPtr)0x6e6f55);
                        hudY = readMem<float>((IntPtr)0x6e6f5d);
                        writeMem<float>((IntPtr)0x6e6f55, 0f);
                        writeMem<float>((IntPtr)0x6e6f5d, 0f);
                        visibility++;
                    }
                    else if (visibility == 2)
                    {
                        DX9Overlay.ShowAllVisual();
                        writeMem<float>((IntPtr)0x6e6f55, hudX);
                        writeMem<float>((IntPtr)0x6e6f5d, hudY);
                        visibility = 0;
                    }
                }
                else if (e.Key == Keys.Delete)
                {
                    showCoords = !showCoords;
                    SetLog("Expanded cordlog: " + showCoords.ToString());
                }
                else if (e.Key == Keys.Home)
                {
                    TaskChanged(task, task + 1);
                    task++;
                    doTask = false;
                }
                else if (e.Key == Keys.End)
                {
                    TaskChanged(task, task - 1);
                    task--;
                    doTask = false;
                }
                else if (e.Key == Keys.PageUp)
                {
                    if (idSelected <= 40)
                    {
                        idSelected++;
                    }
                    SelectedIdChanged();
                }
                else if (e.Key == Keys.Next)
                {
                    if (idSelected >= -1)
                    {
                        idSelected--;
                    }
                    SelectedIdChanged();
                }
                else if (e.Key == Keys.Insert)
                {
                    doTask = !doTask;
                    SetLog("Perform the task: " + doTask.ToString());
                    TaskStateChanged();
                }
            }
        }



 
        public static void RBForce(KeyboardHookEventArgs e)
        {
            if (e.Key == Keys.N)
            {
                SetLog("Rigidbody Stop");
                CRB(Info.Object.Vel1, 0f);
                CRB(Info.Object.Vel2, 0f);
                CRB(Info.Object.Vel3, 0f);
            }
            if (e.Key == Keys.K)
            {
                SetLog("RigidBody AddFroce +" + PscPower);
                CRB(Info.Object.Vel1, (float)PscPower);
            }
            if (e.Key == Keys.Y)
            {
                SetLog("RigidBody AddFroce -" + PscPower);
                CRB(Info.Object.Vel1, PscPower * -1f);
            }
            if (e.Key == Keys.U)
            {
                SetLog("RigidBody AddFroce +" + PscPower);
                CRB(Info.Object.Vel2, (float)PscPower);
            }
            if (e.Key == Keys.J)
            {
                SetLog("RigidBody AddFroce -" + PscPower);
                CRB(Info.Object.Vel2, PscPower * -1f);
            }
            if (e.Key == Keys.I)
            {
                SetLog("RigidBody AddFroce +" + PscPower);
                CRB(Info.Object.Vel3, (float)PscPower);
            }
            if (e.Key == Keys.H)
            {
                SetLog("RigidBody AddFroce -" + PscPower);
                CRB(Info.Object.Vel3, PscPower * -1f);
            }
        }



        public static string GetWD(string fn)
        {
            string result = "";
            string[] fs = fn.Split('\\');
            for (int i = 0; i < fs.Length - 1; i++)
            {
                if (i != 0)
                {
                    result += "\\";
                }
                result += fs[i];
            }
            return result;
        }
    }
}
