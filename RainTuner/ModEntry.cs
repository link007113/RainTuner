using System;
using System.Collections.Generic;
using System.Linq;
using StardewModdingAPI;
using StardewModdingAPI.Events;
using StardewValley;
using GenericModConfigMenu;

namespace RainTuner
{

    public sealed class ModEntry : Mod
    {
        private ModConfig Config = null!;
        private bool _forcePrecipTomorrow;

        public override void Entry(IModHelper helper)
        {
            Config = Helper.ReadConfig<ModConfig>();

            helper.Events.GameLoop.GameLaunched += OnGameLaunched;
            helper.Events.GameLoop.DayEnding += OnDayEnding;
            helper.Events.Input.ButtonsChanged += OnButtonsChanged;
        }

        // ---------------- GMCM ----------------
        private void OnGameLaunched(object? sender, GameLaunchedEventArgs e)
        {
            var gmcm = Helper.ModRegistry.GetApi<IGenericModConfigMenuApi>("spacechase0.GenericModConfigMenu");
            if (gmcm is null)
            {
                Monitor.Log("GMCM not found; skipping config menu.", LogLevel.Info);
                return;
            }

            gmcm.Register(
                mod: ModManifest,
                reset: () => { Config = new ModConfig(); Helper.WriteConfig(Config); },
                save: () => Helper.WriteConfig(Config),
                titleScreenOnly: false
            );

            gmcm.AddSectionTitle(ModManifest, () => "Seasonal precipitation chance", null);
            gmcm.AddNumberOption(ModManifest, () => Config.SpringPercent, v => Config.SpringPercent = Clamp01(v),
                () => "Spring chance", () => "Chance that tomorrow has any precipitation", 0, 100, 1, v => $"{v}%", "springChance");
            gmcm.AddNumberOption(ModManifest, () => Config.SummerPercent, v => Config.SummerPercent = Clamp01(v),
                () => "Summer chance", () => "Chance that tomorrow has any precipitation", 0, 100, 1, v => $"{v}%", "summerChance");
            gmcm.AddNumberOption(ModManifest, () => Config.FallPercent, v => Config.FallPercent = Clamp01(v),
                () => "Fall chance", () => "Chance that tomorrow has any precipitation", 0, 100, 1, v => $"{v}%", "fallChance");
            gmcm.AddNumberOption(ModManifest, () => Config.WinterPercent, v => Config.WinterPercent = Clamp01(v),
                () => "Winter chance (snow)", () => "Chance that tomorrow snows", 0, 100, 1, v => $"{v}%", "winterChance");

            gmcm.AddSectionTitle(ModManifest, () => "Precipitation type (non-winter)", null);
            gmcm.AddBoolOption(ModManifest, () => Config.EnableStorms, v => Config.EnableStorms = v,
                () => "Enable Storm", () => "Allow storms as a possible precipitation type", "enableStorms");
            gmcm.AddNumberOption(ModManifest, () => Config.StormWeight, v => Config.StormWeight = Clamp01(v),
                () => "Storm weight", () => "Weight used to pick Storm", 0, 100, 1, v => $"{v}", "stormWeight");
            gmcm.AddBoolOption(ModManifest, () => Config.EnableGreenRain, v => Config.EnableGreenRain = v,
                () => "Enable Green Rain", () => "Allow Green Rain on eligible summer dates (unless override enabled)", "enableGreenRain");
            gmcm.AddNumberOption(ModManifest, () => Config.GreenRainWeight, v => Config.GreenRainWeight = Clamp01(v),
                () => "Green Rain weight", () => "Weight used to pick Green Rain", 0, 100, 1, v => $"{v}", "greenRainWeight");
            gmcm.AddNumberOption(ModManifest, () => Config.RainWeight, v => Config.RainWeight = Clamp01(v),
                () => "Rain weight", () => "Weight used to pick normal Rain", 0, 100, 1, v => $"{v}", "rainWeight");

            gmcm.AddSectionTitle(ModManifest, () => "Overrides", null);
            gmcm.AddBoolOption(ModManifest, () => Config.AllowForcedGreenRain, v => Config.AllowForcedGreenRain = v,
                () => "Allow Green Rain override", () => "Let Green Rain happen on any summer day", "allowForcedGreenRain");
            gmcm.AddBoolOption(ModManifest, () => Config.AllowForcedSpringYearOneOverride, v => Config.AllowForcedSpringYearOneOverride = v,
                () => "Allow Spring Year 1 override", () => "Ignore vanilla fixed weather on Spring 1–5 (Year 1)", "allowForcedSpringOverride");

            gmcm.AddSectionTitle(ModManifest, () => "Hotkeys & behavior", null);
            gmcm.AddKeybindList(ModManifest, () => Config.ForcePrecipHotkey, v => Config.ForcePrecipHotkey = v,
                () => "Force precipitation tomorrow", () => "Press to force rain/snow (type picked by weights)", "forcePrecipHotkey");
            gmcm.AddKeybindList(ModManifest, () => Config.ForceSunnyHotkey, v => Config.ForceSunnyHotkey = v,
                () => "Force vanilla sunny (valley)", () => "Set tomorrow to sunny (no special effects)", "forceSunnyHotkey");
            gmcm.AddBoolOption(ModManifest, () => Config.RespectFestivalsAndWeddings, v => Config.RespectFestivalsAndWeddings = v,
                () => "Respect festivals and weddings", () => "Do not override weather on those days", "respectSpecialDays");
            gmcm.AddBoolOption(ModManifest, () => Config.ForceSunnyOnMiss, v => Config.ForceSunnyOnMiss = v,
                () => "Force sunny on failed roll", () => "If chance roll fails, explicitly set sunny", "forceSunnyOnMiss");

            Monitor.Log("Rain Tuner: GMCM page registered.", LogLevel.Info);
        }

        // --------------- Input ---------------
        private void OnButtonsChanged(object? sender, ButtonsChangedEventArgs e)
        {
            if (!Context.IsWorldReady)
                return;

            // Force precip (valley)
            if (Config.ForcePrecipHotkey.JustPressed())
            {
                _forcePrecipTomorrow = true;
                var seasonTomorrow = GetSeasonTomorrow();

                if (seasonTomorrow == Season.Winter)
                {
                    Game1.weatherForTomorrow = Game1.weather_snow;
                    Game1.addHUDMessage(new HUDMessage("Snow forced for tomorrow"));
                    Monitor.Log("Hotkey: set Snow for tomorrow", LogLevel.Info);
                    return;
                }

                string picked = PickPrecipTypeForTomorrow();
                Game1.weatherForTomorrow = picked;
                Game1.addHUDMessage(new HUDMessage($"{DisplayNameForWeather(picked)} forced for tomorrow"));
                Monitor.Log($"Hotkey: set {DisplayNameForWeather(picked)} for tomorrow", LogLevel.Info);
            }

            // Force vanilla sunny (valley)
            if (Config.ForceSunnyHotkey.JustPressed())
            {
                Game1.weatherForTomorrow = Game1.weather_sunny;
                Game1.addHUDMessage(new HUDMessage("Sunny forced for tomorrow"));
                Monitor.Log("Hotkey: forced Sunny for tomorrow (valley).", LogLevel.Info);
            }
        }

        // --------------- End of day ---------------
        private void OnDayEnding(object? sender, DayEndingEventArgs e)
        {
            if (!Context.IsWorldReady)
                return;

            int tomorrowDay = Game1.dayOfMonth == 28 ? 1 : Game1.dayOfMonth + 1;
            Season seasonTomorrow = GetSeasonTomorrow();

            if (Config.RespectFestivalsAndWeddings)
            {
                if (Utility.isFestivalDay(tomorrowDay, seasonTomorrow))
                {
                    Monitor.Log("Tomorrow is a festival. Weather not changed.", LogLevel.Trace);
                    _forcePrecipTomorrow = false;
                    return;
                }
                if (Game1.weatherForTomorrow == Game1.weather_wedding)
                {
                    Monitor.Log("Tomorrow is a wedding. Weather not changed.", LogLevel.Trace);
                    _forcePrecipTomorrow = false;
                    return;
                }
            }

            // Year 1 Spring hard rules (unless override)
            if (!Config.AllowForcedSpringYearOneOverride &&
                TryApplyYearOneSpringRule(tomorrowDay, seasonTomorrow))
                return;

            if (_forcePrecipTomorrow)
            {
                ApplyPrecipitation(seasonTomorrow);
                _forcePrecipTomorrow = false;
                return;
            }

            // Valley roll
            int valleyChance = seasonTomorrow switch
            {
                Season.Spring => Config.SpringPercent,
                Season.Summer => Config.SummerPercent,
                Season.Fall => Config.FallPercent,
                Season.Winter => Config.WinterPercent,
                _ => 0
            };

            var rng = new Random(unchecked((int)(Game1.uniqueIDForThisGame + Game1.stats.DaysPlayed * 9973)));
            bool precipValley = rng.NextDouble() < (valleyChance / 100.0);

            if (precipValley)
                ApplyPrecipitation(seasonTomorrow);
            else if (Config.ForceSunnyOnMiss)
                Game1.weatherForTomorrow = Game1.weather_sunny;
        }

        // --------------- Core logic ---------------
        private void ApplyPrecipitation(Season seasonTomorrow)
        {
            if (seasonTomorrow == Season.Winter)
            {
                Game1.weatherForTomorrow = Game1.weather_snow;
                Monitor.Log("Set Snow for tomorrow.", LogLevel.Info);
                return;
            }

            string picked = PickPrecipTypeForTomorrow();
            Game1.weatherForTomorrow = picked;
            Monitor.Log($"Set {DisplayNameForWeather(picked)} for tomorrow.", LogLevel.Info);
        }

        /// Pick Rain/Storm/GreenRain (strings per 1.6), honoring eligibility for Green Rain unless override is enabled.
        private string PickPrecipTypeForTomorrow()
        {
            var choices = new List<(string id, int weight)>();

            // Rain
            int rainW = Math.Max(0, Config.RainWeight);
            if (rainW > 0) choices.Add((Game1.weather_rain, rainW));

            // Storms
            if (Config.EnableStorms)
            {
                int w = Math.Max(0, Config.StormWeight);
                if (w > 0) choices.Add((Game1.weather_lightning, w));
            }

            // Green Rain: eligible only on certain summer dates unless override
            if (Config.EnableGreenRain && IsGreenRainEligible())
            {
                int w = Math.Max(0, Config.GreenRainWeight);
                if (w > 0) choices.Add((Game1.weather_green_rain, w));
            }

            int total = choices.Sum(c => c.weight);
            if (total <= 0)
                return Game1.weather_rain;

            int roll = new Random(unchecked((int)(Game1.uniqueIDForThisGame + Game1.stats.DaysPlayed * 7919))).Next(1, total + 1);
            int acc = 0;
            foreach (var c in choices)
            {
                acc += c.weight;
                if (roll <= acc)
                    return c.id;
            }
            return Game1.weather_rain;
        }

        /// Green Rain can only happen in Summer on days 5, 6, 7, 14, 16, 18, 23 — unless override is enabled.
        private bool IsGreenRainEligible()
        {
            if (Config.AllowForcedGreenRain)
                return true;

            if (GetSeasonTomorrow() != Season.Summer)
                return false;

            int tomorrow = Game1.dayOfMonth == 28 ? 1 : Game1.dayOfMonth + 1;
            return tomorrow is 5 or 6 or 7 or 14 or 16 or 18 or 23;
        }

        private bool TryApplyYearOneSpringRule(int tomorrowDay, Season seasonTomorrow)
        {
            if (Game1.year != 1 || seasonTomorrow != Season.Spring)
                return false;

            // if override enabled, do not enforce vanilla rule
            if (Config.AllowForcedSpringYearOneOverride)
                return false;

            switch (tomorrowDay)
            {
                case 1:
                case 2:
                case 4:
                case 5:
                    Game1.weatherForTomorrow = Game1.weather_sunny;
                    Game1.addHUDMessage(new HUDMessage("Year 1 Spring rule: tomorrow must be Sunny.", HUDMessage.error_type));
                    Monitor.Log("Year 1 Spring rule: forcing Sunny.", LogLevel.Warn);
                    return true;
                case 3:
                    Game1.weatherForTomorrow = Game1.weather_rain;
                    Game1.addHUDMessage(new HUDMessage("Year 1 Spring rule: tomorrow must be Rain.", HUDMessage.error_type));
                    Monitor.Log("Year 1 Spring rule: forcing Rain.", LogLevel.Warn);
                    return true;
                default:
                    return false;
            }
        }

        private static int Clamp01(int v) => Math.Max(0, Math.Min(100, v));

        private static Season GetSeasonTomorrow()
        {
            Season curr = Game1.currentSeason switch
            {
                "spring" => Season.Spring,
                "summer" => Season.Summer,
                "fall" => Season.Fall,
                _ => Season.Winter
            };

            if (Game1.dayOfMonth >= 28)
            {
                curr = curr switch
                {
                    Season.Spring => Season.Summer,
                    Season.Summer => Season.Fall,
                    Season.Fall => Season.Winter,
                    _ => Season.Spring
                };
            }

            return curr;
        }

        private static string DisplayNameForWeather(string id)
        {
            if (id == Game1.weather_lightning) return "Storm";
            if (id == Game1.weather_green_rain) return "Green Rain";
            if (id == Game1.weather_snow) return "Snow";
            if (id == Game1.weather_rain) return "Rain";
            if (id == Game1.weather_debris) return "Wind";
            if (id == Game1.weather_sunny) return "Sunny";
            return id;
        }
    }
}
