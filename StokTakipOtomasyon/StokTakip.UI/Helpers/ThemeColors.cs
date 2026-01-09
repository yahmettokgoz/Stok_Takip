using System.Drawing;

namespace StokTakip.UI.Helpers
{
    /// <summary>
    /// Modern Dark Theme renk paleti
    /// </summary>
    public static class ThemeColors
    {
        // Dark Background
        public static Color Background = Color.FromArgb(15, 23, 42);        // Slate-900
        public static Color PrimaryDark = Color.FromArgb(15, 23, 42);       // Slate-900
        public static Color SecondaryDark = Color.FromArgb(30, 41, 59);     // Slate-800
        public static Color TertiaryDark = Color.FromArgb(51, 65, 85);      // Slate-700
        public static Color Slate700 = Color.FromArgb(51, 65, 85);          // Slate-700
        
        // Accent Colors (Neon/Vibrant)
        public static Color Primary = Color.FromArgb(59, 130, 246);         // Blue-500
        public static Color AccentBlue = Color.FromArgb(59, 130, 246);      // Blue-500
        public static Color AccentGreen = Color.FromArgb(34, 197, 94);      // Green-500
        public static Color AccentOrange = Color.FromArgb(249, 115, 22);    // Orange-500
        public static Color AccentRed = Color.FromArgb(239, 68, 68);        // Red-500
        public static Color AccentPurple = Color.FromArgb(168, 85, 247);    // Purple-500
        public static Color AccentCyan = Color.FromArgb(6, 182, 212);       // Cyan-600
        public static Color AccentYellow = Color.FromArgb(234, 179, 8);     // Yellow-500
        
        // Text Colors
        public static Color TextPrimary = Color.FromArgb(248, 250, 252);    // Slate-50
        public static Color TextSecondary = Color.FromArgb(148, 163, 184);  // Slate-400
        public static Color TextDisabled = Color.FromArgb(100, 116, 139);   // Slate-500
        
        // Card & Components
        public static Color CardBackground = Color.FromArgb(30, 41, 59);
        public static Color CardHover = Color.FromArgb(51, 65, 85);
        public static Color BorderColor = Color.FromArgb(71, 85, 105);      // Slate-600
        public static Color BorderLight = Color.FromArgb(100, 116, 139);
        
        // Status Colors
        public static Color Success = Color.FromArgb(34, 197, 94);
        public static Color Warning = Color.FromArgb(249, 115, 22);
        public static Color Danger = Color.FromArgb(239, 68, 68);
        public static Color Info = Color.FromArgb(59, 130, 246);
        
        // Sidebar
        public static Color SidebarBackground = Color.FromArgb(15, 23, 42);
        public static Color SidebarHover = Color.FromArgb(30, 41, 59);
        public static Color SidebarActive = Color.FromArgb(51, 65, 85);
    }
}
