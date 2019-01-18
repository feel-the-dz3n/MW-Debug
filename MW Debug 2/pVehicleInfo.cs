using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MW_Debug_2
{
    public class pVehicleInfo
    {
        public string name = "unknown";
        public List<IntPtr> addresses = new List<IntPtr>();
        public string tip = "";
        public string type = "";

        public static void Init()
        {
            Info.pVehicle.Clear();

            Info.pVehicle.Add(new pVehicleInfo() { name = "911turbo", type = "racers" });
            Info.pVehicle.Add(new pVehicleInfo() { name = "911gt2", type = "racers" });
            Info.pVehicle.Add(new pVehicleInfo() { name = "997s", type = "racers" });
            Info.pVehicle.Add(new pVehicleInfo() { name = "a3", type = "racers" });
            Info.pVehicle.Add(new pVehicleInfo() { name = "bmwm3gtr", type = "racers" });
            Info.pVehicle.Add(new pVehicleInfo() { name = "bmwm3gtre46", type = "racers" });
            Info.pVehicle.Add(new pVehicleInfo() { name = "m3gtre46careerstart", type = "racers" });
            Info.pVehicle.Add(new pVehicleInfo() { name = "mustang_demo", type = "racers" });
            Info.pVehicle.Add(new pVehicleInfo() { name = "wrx_demo", type = "racers" });
            Info.pVehicle.Add(new pVehicleInfo() { name = "camaro", type = "racers" });
            Info.pVehicle.Add(new pVehicleInfo() { name = "carreragt", type = "racers" });
            Info.pVehicle.Add(new pVehicleInfo() { name = "clio", type = "racers" });
            Info.pVehicle.Add(new pVehicleInfo() { name = "clk500", type = "racers" });
            Info.pVehicle.Add(new pVehicleInfo() { name = "cobaltss", type = "racers" });
            Info.pVehicle.Add(new pVehicleInfo() { name = "corvette", type = "racers" });
            Info.pVehicle.Add(new pVehicleInfo() { name = "corvettec6r", type = "racers" });
            Info.pVehicle.Add(new pVehicleInfo() { name = "cts", type = "racers" });
            Info.pVehicle.Add(new pVehicleInfo() { name = "db9", type = "racers" });
            Info.pVehicle.Add(new pVehicleInfo() { name = "eclipsegt", type = "racers" });
            Info.pVehicle.Add(new pVehicleInfo() { name = "elise", type = "racers" });
            Info.pVehicle.Add(new pVehicleInfo() { name = "fordgt", type = "racers" });
            Info.pVehicle.Add(new pVehicleInfo() { name = "gallardo", type = "racers" });
            Info.pVehicle.Add(new pVehicleInfo() { name = "gti", type = "racers" });
            Info.pVehicle.Add(new pVehicleInfo() { name = "gto", type = "racers" });
            Info.pVehicle.Add(new pVehicleInfo() { name = "imprezawrx", type = "racers" });
            Info.pVehicle.Add(new pVehicleInfo() { name = "is300", type = "racers" });
            Info.pVehicle.Add(new pVehicleInfo() { name = "lancerevo8", type = "racers" });
            Info.pVehicle.Add(new pVehicleInfo() { name = "monaro", type = "racers" });
            Info.pVehicle.Add(new pVehicleInfo() { name = "murcielago", type = "racers" });
            Info.pVehicle.Add(new pVehicleInfo() { name = "mustanggt", type = "racers" });
            Info.pVehicle.Add(new pVehicleInfo() { name = "punto", type = "racers" });
            Info.pVehicle.Add(new pVehicleInfo() { name = "rx7", type = "racers" });
            Info.pVehicle.Add(new pVehicleInfo() { name = "rx8", type = "racers" });
            Info.pVehicle.Add(new pVehicleInfo() { name = "rx8speedt", type = "racers" });
            Info.pVehicle.Add(new pVehicleInfo() { name = "sl500", type = "racers" });
            Info.pVehicle.Add(new pVehicleInfo() { name = "sl65", type = "racers" });
            Info.pVehicle.Add(new pVehicleInfo() { name = "slr", type = "racers" });
            Info.pVehicle.Add(new pVehicleInfo() { name = "speedtest", type = "racers" });
            Info.pVehicle.Add(new pVehicleInfo() { name = "supra", type = "racers" });
            Info.pVehicle.Add(new pVehicleInfo() { name = "tt", type = "racers" });
            Info.pVehicle.Add(new pVehicleInfo() { name = "viper", type = "racers" });
            Info.pVehicle.Add(new pVehicleInfo() { name = "slr", type = "racers" });

           // List<IntPtr> cms = Form1.searchStringMem("copmidsize", 0x07BCC953, 0x08FCC953);

            Info.pVehicle.Add(new pVehicleInfo() { name = "copmidsize", type = "cops", tip = "heat 1", addresses = { (IntPtr)0x07BCC83D, (IntPtr)0x07BCCB8A, (IntPtr)0x07BCCAFF, (IntPtr)0x07BCEA3B, (IntPtr)0x07DBEF29, (IntPtr)0x07BCCC29 } });
            Info.pVehicle.Add(new pVehicleInfo() { name = "copghost", type = "cops", tip = "heat 2", addresses = { (IntPtr)0x07BCCC00 } });
            Info.pVehicle.Add(new pVehicleInfo() { name = "copgto", type = "cops", tip = "heat 3", addresses = { (IntPtr)0x07BCC987, (IntPtr)0x07BCC80D, (IntPtr)0x07BCCBA3 } });
            Info.pVehicle.Add(new pVehicleInfo() { name = "copsporthench", type = "cops", tip = "heat 5", addresses = { (IntPtr)0x07BCCA2E } });
            Info.pVehicle.Add(new pVehicleInfo() { name = "copsuv", type = "cops", tip = "", addresses = { (IntPtr)0x07BCC4EA, (IntPtr)0x07BCCC00 } });
            Info.pVehicle.Add(new pVehicleInfo() { name = "copsuvpatrol", type = "cops", tip = "", addresses = { (IntPtr)0x07BCC4EA, (IntPtr)0x07BCCC00 } });
            Info.pVehicle.Add(new pVehicleInfo() { name = "copgtoghost", type = "cops", tip = "" });
            Info.pVehicle.Add(new pVehicleInfo() { name = "copmidsize_nis", type = "cops", tip = "" });
            Info.pVehicle.Add(new pVehicleInfo() { name = "copmidsize_nis_ld", type = "cops", tip = "" });
            Info.pVehicle.Add(new pVehicleInfo() { name = "copcross", type = "cops", tip = "" });
            Info.pVehicle.Add(new pVehicleInfo() { name = "copsportghost", type = "cops", tip = "" });
            Info.pVehicle.Add(new pVehicleInfo() { name = "copheli", type = "cops", tip = "choppers", addresses = { (IntPtr)0x07BCC4EA, (IntPtr)0x07BCCC00 } });

            Info.pVehicle.Add(new pVehicleInfo() { name = "cs_semi", type = "tractors|semi", tip = "" });
            Info.pVehicle.Add(new pVehicleInfo() { name = "semia", type = "tractors|semi", tip = "" });
            Info.pVehicle.Add(new pVehicleInfo() { name = "semib", type = "tractors|semi", tip = "" });
            Info.pVehicle.Add(new pVehicleInfo() { name = "semicmt", type = "tractors|semi", tip = "" });
            Info.pVehicle.Add(new pVehicleInfo() { name = "semicon", type = "tractors|semi", tip = "" });
            Info.pVehicle.Add(new pVehicleInfo() { name = "semicrate", type = "tractors|semi", tip = "" });
            Info.pVehicle.Add(new pVehicleInfo() { name = "semilog", type = "tractors|semi", tip = "" });

            Info.pVehicle.Add(new pVehicleInfo() { name = "cs_clio_trafpizza", type = "traffic", tip = "" });
            Info.pVehicle.Add(new pVehicleInfo() { name = "cs_clio_traftaxi", type = "traffic", tip = "" });
            Info.pVehicle.Add(new pVehicleInfo() { name = "cs_cts_traf_minivan", type = "traffic", tip = "" });
            Info.pVehicle.Add(new pVehicleInfo() { name = "cs_cts_traffictruck", type = "traffic", tip = "" });
            Info.pVehicle.Add(new pVehicleInfo() { name = "cs_trafcement", type = "traffic", tip = "" });
            Info.pVehicle.Add(new pVehicleInfo() { name = "cs_trafgarb", type = "traffic", tip = "" });
            Info.pVehicle.Add(new pVehicleInfo() { name = "traf4dseda", type = "traffic", tip = "" });
            Info.pVehicle.Add(new pVehicleInfo() { name = "traf4dsedb", type = "traffic", tip = "" });
            Info.pVehicle.Add(new pVehicleInfo() { name = "traf4dsedc", type = "traffic", tip = "" });
            Info.pVehicle.Add(new pVehicleInfo() { name = "trafcourt", type = "traffic", tip = "" });
            Info.pVehicle.Add(new pVehicleInfo() { name = "trafficcoup", type = "traffic", tip = "" });
            Info.pVehicle.Add(new pVehicleInfo() { name = "trafha", type = "traffic", tip = "" });
            Info.pVehicle.Add(new pVehicleInfo() { name = "trafpizza", type = "traffic", tip = "" });
            Info.pVehicle.Add(new pVehicleInfo() { name = "trafstwag", type = "traffic", tip = "" });
            Info.pVehicle.Add(new pVehicleInfo() { name = "traftaxi", type = "traffic", tip = "" });
            Info.pVehicle.Add(new pVehicleInfo() { name = "trafamb", type = "traffic", tip = "" });
            Info.pVehicle.Add(new pVehicleInfo() { name = "trafcemtr", type = "traffic", tip = "" });
            Info.pVehicle.Add(new pVehicleInfo() { name = "trafdmptr", type = "traffic", tip = "" });
            Info.pVehicle.Add(new pVehicleInfo() { name = "traffire", type = "traffic", tip = "" });
            Info.pVehicle.Add(new pVehicleInfo() { name = "trafgarb", type = "traffic", tip = "" });
            Info.pVehicle.Add(new pVehicleInfo() { name = "trafcamper", type = "traffic", tip = "" });
            Info.pVehicle.Add(new pVehicleInfo() { name = "trafminivan", type = "traffic", tip = "" });
            Info.pVehicle.Add(new pVehicleInfo() { name = "trafnews", type = "traffic", tip = "" });
            Info.pVehicle.Add(new pVehicleInfo() { name = "trafpickupa", type = "traffic", tip = "" });
            Info.pVehicle.Add(new pVehicleInfo() { name = "trafsuva", type = "traffic", tip = "" });
            Info.pVehicle.Add(new pVehicleInfo() { name = "trafvanb", type = "traffic", tip = "" });

            Info.pVehicle.Add(new pVehicleInfo() { name = "copheli", type = "choppers", tip = "" });

            Info.pVehicle.Add(new pVehicleInfo() { name = "trailera", type = "trailers", tip = "" });
            Info.pVehicle.Add(new pVehicleInfo() { name = "trailerb", type = "trailers", tip = "" });
            Info.pVehicle.Add(new pVehicleInfo() { name = "trailercmt", type = "trailers", tip = "" });
            Info.pVehicle.Add(new pVehicleInfo() { name = "trailercon", type = "trailers", tip = "" });
            Info.pVehicle.Add(new pVehicleInfo() { name = "trailercrate", type = "trailers", tip = "" });
            Info.pVehicle.Add(new pVehicleInfo() { name = "trailerlog", type = "trailers", tip = "" });

        }

    }
}
