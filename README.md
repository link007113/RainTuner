# Rain Tuner

**Rain Tuner**: Customize the chance of rain per season and force tomorrow’s weather with a single hotkey.  
Compatible with SMAPI 4.3.2 and Stardew Valley 1.6.x.

---

## ✨ Features

- **Seasonal precipitation chances**  
  - Spring: 25%  
  - Summer: 15%  
  - Fall: 50%  
  - Winter: 80% (snow)  
  - All values configurable via **Generic Mod Config Menu (GMCM)**

- **Force precipitation tomorrow with a hotkey**  
  - Default: **F9**

- **Options**
  - Respect festivals and weddings
  - Force sunny weather if the chance roll fails (for tighter distribution)

- **Safe for existing saves**

---

## 📦 Requirements

- Stardew Valley **1.6.x**  
- SMAPI **4.3.2+**  
- (Optional) [Generic Mod Config Menu](https://www.nexusmods.com/stardewvalley/mods/5098) for in-game configuration

---

## 🔧 Installation

### Vortex
1. Click **“Mod Manager Download”** or add the zip manually to Vortex  
2. Enable the mod  
3. Launch the game via **SMAPI**

### Manual
1. Extract the `RainTuner` folder into `Stardew Valley/Mods`  
2. Launch the game via **SMAPI**

---

## ⚙️ Configuration

Available through **GMCM** (in the title screen or in-game menu):

- Spring chance (default 25%)  
- Summer chance (default 15%)  
- Fall chance (default 50%)  
- Winter chance – snow (default 80%)  
- Force rain tomorrow hotkey (default: F9)  
- Respect festivals & weddings (default: On)  
- Force sunny if roll fails (default: On)

---

## 🎮 Usage

- Want precipitation tomorrow? → Press the hotkey. (In winter this means snow.)  
- Want random seasonal weather? → Just sleep. The mod rolls at the end of each day.  
- Want strict percentages? → Keep *Force sunny on failure* enabled.  
- Want other weather mods to have influence? → Disable *Force sunny*.  

---

## 🛠️ How It Works

- The mod sets the weather for tomorrow at the end of the day.  
- The hotkey sets `Game1.weatherForTomorrow` directly to rain or snow.  
- If *Respect festivals and weddings* is enabled, those days are never overridden.  

---

## 👥 Multiplayer

- Weather is **host-determined**.  
- Only the **host** needs this mod for effects to apply.  
- Non-hosts can install safely, but it won’t affect the session.

---

## 🔄 Compatibility

- Compatible with **SMAPI 4.3.2+** and Stardew Valley **1.6.x**  
- Works alongside most content mods  
- Mods that also set tomorrow’s weather may conflict → last mod wins  
- For best predictability: enable *Force sunny* and load this mod last  

---

## ❓ Troubleshooting

- Nothing changes?  
  - Check if tomorrow is a festival or wedding  
  - Try pressing **F9** and sleeping  
  - Check `SMAPI-latest.txt` for `Rain Tuner` messages  

- Rain appears in winter?  
  - Nope. In winter this mod always sets **snow** if precipitation is triggered.  

---

## ⚠️ Known Issues

- Conflicts possible with overhaul mods that fully replace weather logic.  
- SMAPI logs help identify load order issues.

---

## 📜 Changelog

See [CHANGELOG.md](CHANGELOG.md)

---

## 🙏 Credits

- Code: **Link007**  
- Thanks to the **SMAPI team** and the **modding community**
