using StardewModdingAPI;
using StardewModdingAPI.Utilities;

namespace RainTuner
{
    public class ModConfig
    {
        // Seasonal precipitation chance (any precip at all)
        public int SpringPercent { get; set; } = 25;
        public int SummerPercent { get; set; } = 15;
        public int FallPercent { get; set; } = 50;
        public int WinterPercent { get; set; } = 80; // snow

        // Precipitation type distribution when precipitation happens (non-winter)
        public bool EnableStorms { get; set; } = true;
        public int StormWeight { get; set; } = 25;  // %
        public bool EnableGreenRain { get; set; } = true;
        public int GreenRainWeight { get; set; } = 10; // %
        public int RainWeight { get; set; } = 65;  // %

        // Overrides
        public bool AllowForcedGreenRain { get; set; } = false;
        public bool AllowForcedSpringYearOneOverride { get; set; } = false;

        // Hotkeys
        public KeybindList ForcePrecipHotkey { get; set; } = KeybindList.ForSingle(SButton.F9);
        public KeybindList ForceSunnyHotkey { get; set; } = KeybindList.ForSingle(SButton.F10);

        // Behavior
        public bool ForceSunnyOnMiss { get; set; } = true;
        public bool RespectFestivalsAndWeddings { get; set; } = true;
    }
}
