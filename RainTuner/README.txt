# **Rain Tuner**

Customize the chance of rain per season and force tomorrow’s weather with a single hotkey. Optional GMCM config. Compatible with SMAPI 4.3.2. Vortex-friendly.

---

## **Features**

* **Seasonal rain/snow chance:**

  * Spring: 25%
  * Summer: 15%
  * Fall: 50%
  * Winter: 80% (snow)
  * All configurable via GMCM

* **Force precipitation tomorrow with a hotkey:**

  * Default: F9
  * Customizable via GMCM

* **Options:**

  * Respect festivals and weddings
  * Force sunny weather if the chance roll fails (for tighter distribution)

* Safe for existing saves

---

## **Requirements**

* Stardew Valley 1.6.x
* SMAPI 4.3.2 or higher
* *Generic Mod Config Menu* by spacechase0 (recommended, not required)

---

## **Installation**

### **With Vortex**

1. Click “Mod Manager Download” or add the zip manually to Vortex
2. Enable the mod
3. Launch the game via SMAPI

### **Manual**

1. Extract the `RainTuner` folder into `Stardew Valley/Mods`
2. Launch the game via SMAPI

---

## **Configuration**

Use the **in-game GMCM menu** to change:

* Spring chance
* Summer chance
* Fall chance
* Winter chance (snow)
* Force rain tomorrow hotkey
* Respect festivals & weddings
* Force sunny if roll fails

### **Default values:**

* Spring: 25, Summer: 15, Fall: 50, Winter: 80
* Hotkey: F9
* Respect special days: On
* Force sunny: On

---

## **Usage**

* Want precipitation tomorrow? Press the hotkey. In winter, this means snow.
* Want random seasonal weather? Just sleep. The mod rolls the chance at the end of each day.
* Want strict percentages? Keep *Force sunny on failure* enabled.
* Want other weather mods to have influence? Disable that option.

---

## **How It Works**

* The mod sets the weather for tomorrow **at the end of the day**.
* The hotkey sets `weatherForTomorrow` directly to rain or snow.
* If *Respect festivals and weddings* is enabled, weather on those days won’t be overridden.

---

## **Multiplayer**

* **Weather is host-determined.** Only the host needs this mod for effects to apply.
* Non-hosts can use the mod safely, but it won’t affect the session.

---

## **Compatibility**

* Compatible with SMAPI 4.3.2 and Stardew Valley 1.6.x
* Works alongside most content mods
* Mods that set tomorrow’s weather may conflict – **last mod wins**
* For best predictability, **enable “Force sunny”** and **load this mod last**

---

## **Troubleshooting**

**Nothing changes?**

* Check if tomorrow is a festival or wedding
* Try pressing F9 and sleeping
* Check `SMAPI-latest.txt` for “Rain Tuner” messages

**GMCM not showing?**

* Install or update `spacechase0.GenericModConfigMenu`

**Rain appears in winter?**

* Nope. This mod always uses **snow** during winter if triggered

---

## **Known Issues**

* Conflicts possible with full overhaul mods that override weather
* SMAPI logs help identify load order conflicts in your setup

---

## **Changelog**

**1.0.0 – Initial Release**

* Seasonal precipitation chances
* Force tomorrow’s weather via hotkey
* GMCM integration
* Festival and wedding protection
* Vortex package ready

---

## **Credits**

* Code: **Link007**
* GMCM by **spacechase0**
* Thanks to the SMAPI team and modding community

---
