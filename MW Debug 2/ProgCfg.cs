using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace MW_Debug_2.Funcs
{
    public class ProgCfg
    {
        public static Dictionary<string, string> Settings = new Dictionary<string, string>();
        private static string FileName = "mwdebug_settings.ini";
        public static void Handle()
        {
            CheckFile();
            StreamReader r = new StreamReader(FileName);
            string[] SplitCfg = { "\r\n", "\n" }; 
            string[] file = r.ReadToEnd().Split(SplitCfg, StringSplitOptions.RemoveEmptyEntries);
            r.Dispose();
            try
            {
                foreach (string line in file)
                {
                    string[] p = line.Split('=');
                    Settings.Add(p[0], p[1]);
                }
            }
            catch (Exception ex) { MessageBox.Show(ex.ToString(), "MW Debug Config Handler"); }
            foreach (var s in Settings)
            {
                DC.WriteLine("[setting] " + s.Key + " = " + s.Value);
            }
            Version v = System.Reflection.Assembly.GetExecutingAssembly().GetName().Version;
            string version = String.Format("{0}.{1}.{2}", v.Major, v.Minor, v.Build, v.Revision);
            if (!Settings["version"].StartsWith(version))
            {
                DC.WriteLine("VERSION ERROR, REMOVING SETTINGS");
                MessageBox.Show("The settings file is deprecated and will be deleted", "MW Debug");
                File.Delete(FileName);
                Settings.Clear();
                Handle();
            }

        }

        public static void CheckFile()
        {
            if (!File.Exists(FileName))
            {
                Version v = System.Reflection.Assembly.GetExecutingAssembly().GetName().Version;
                using (StreamWriter w = new StreamWriter(FileName))
                {
                    w.WriteLine("gamePath=");
                    w.WriteLine("gameDir=");
                    w.WriteLine("startGame=0");
                    w.WriteLine("skipfe=0");
                    w.WriteLine("skipfePlayerCar=speedtest");
                    w.WriteLine("splashFNG=MW_LS_Splash.fng");
                    w.WriteLine("loading=1");
                    w.WriteLine("firstRun=1");
                    w.WriteLine("windowX=0");
                    w.WriteLine("windowY=0");
                    w.WriteLine("animations=1");
                    w.WriteLine(String.Format("version={0}.{1}.{2}", v.Major, v.Minor, v.Build, v.Revision));
                }
                DC.WriteLine("settings file created");
            }
        }

        public static void Save()
        {
            using (StreamWriter w = new StreamWriter(FileName))
            {
                foreach (var s in Settings)
                {
                    w.WriteLine(s.Key + "=" + s.Value);
                }
            }
            DC.WriteLine("settings file updated"); 
            foreach (var s in Settings)
            {
                DC.WriteLine("[setting] " + s.Key + " = " + s.Value);
            }
        }
    }
}
