using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MW_Debug_2
{
    public class GameSlider
    {
        public static bool IsVisible = false;
        public static int Max = 100;
        public static int Min = 0;
        public static int Value = 50;
        public static int TextID = -1;
        public static int Line = -1;

        public static void Init(int screen_height, int screen_width)
        {
            TextID = DX9OverlayAPI.DX9Overlay.TextCreate("Arial", 7, true, false, 230, 28, 0xFFFFFF00, " ", true, true);
            Line = DX9OverlayAPI.DX9Overlay.LineCreate(230, 26, 530, 26, 1, 0x80FFFFFF, false);
        }

        public static void Update()
        {
            if (IsVisible)
    
            {
                DX9OverlayAPI.DX9Overlay.TextSetString(TextID, Value.ToString() + "/" + Max.ToString() + " {FFFFFF}- use + or - keys to change");
                DX9OverlayAPI.DX9Overlay.LineSetShown(Line, true);
            }
            else
            {
                DX9OverlayAPI.DX9Overlay.TextSetString(TextID, " ");
                DX9OverlayAPI.DX9Overlay.LineSetShown(Line, false);
            }
        }

        public static void Increase(int v)
        {
            if (Value < Max) Value += v;
            else Value = Max;
        }

        public static void Decrease(int v)
        {
            if (Value > Min) Value -= v;
            else Value = Min;
        }
    }
}
