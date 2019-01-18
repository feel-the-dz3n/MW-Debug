using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace MW_Debug_2
{
    /// <summary>
    /// Логика взаимодействия для App.xaml
    /// </summary>
    public partial class App : Application
    {
        private void Application_Startup(object sender, StartupEventArgs e)
        {
            Info.WriteResourceToFile("MW_Debug_2.MemorySharp.dll", "MemorySharp.dll");
            try
            {
                Info.WriteResourceToFile("MW_Debug_2.dx9_overlay.dll", "dx9_overlay.dll"); // this file maybe used by game if it's running, so catch exceptuion
            }
            catch (System.IO.IOException) { }
            catch (Exception ex) { throw ex; }
            Info.WriteResourceToFile("MW_Debug_2.ExternalUtilsCSharp.dll", "ExternalUtilsCSharp.dll");
            Info.WriteResourceToFile("MW_Debug_2.Fasm.NET.dll", "Fasm.NET.dll");
            Info.WriteResourceToFile("MW_Debug_2.FMUtils.KeyboardHook.dll", "FMUtils.KeyboardHook.dll");
            Info.WriteResourceToFile("MW_Debug_2.Memcall.exe", "Memcall.exe");
        }
    }
}
