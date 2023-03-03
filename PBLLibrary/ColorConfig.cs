using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PBLLibrary
{
    public static class ColorConfig
    {
        public static Theme CurrentTheme { get; private set; }

        public enum Theme
        {
            DarkPurple,
        }
        public static void ChangeTheme(Theme theme)
        {
            CurrentTheme = theme;
        }

        public static Color BaseColor { get; private set; } = Color.FromArgb(62, 60, 102);
        public static Color FrameColor { get; private set; } = Color.FromArgb(48, 48, 87);
        public static Color DarkFrameColor { get; private set; } = Color.FromArgb(44, 41, 74);
        public static Color LightFrameColor { get; private set; } = Color.FromArgb(60, 50, 99);
        public static Color TextFrameColor { get; private set; } = Color.FromArgb(75, 65, 114);
        public static Color TextColor { get; private set; } = Color.FromArgb(240, 237, 254);
        public static Color LightTextColor { get; private set; } = Color.FromArgb(119, 112, 156);
        public static Color DarkTextColor { get; private set; } = Color.FromArgb(44, 41, 74);

    }
}
