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

- **Precipitation type selection (non-winter)**  
  - Normal Rain  
  - Storms (lightning)  
  - Green Rain (by default only on eligible summer days: 5, 6, 7, 14, 16, 18, 23)  
  - All weighted and configurable

- **Force precipitation tomorrow with a hotkey**  
  - Default: **F9**

- **Options**
  - Respect festivals and weddings
  - Force sunny weather if the chance roll fails (for tighter distribution)
  - **Override settings**
    - *Allow Green Rain override*: ignore vanilla restrictions and allow Green Rain any summer day  
    - *Allow Spring Year 1 override*: ignore vanilla’s fixed weather on Spring 1–5 Year 1

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

Available through **GMCM**:

- Seasonal chances (Spring, Summer, Fall, Winter Snow)  
- Weights for Rain, Storms, and Green Rain  
- Hotkey for forcing precipitation (default: F9)  
- Respect festivals & weddings  
- Force sunny if roll fails  
- Overrides:
  - Allow Green Rain override  
  - Allow Spring Year 1 override  

---

## 🎮 Usage

- Want precipitation tomorrow? → Press the hotkey.  
- In winter → always snow.  
- In other seasons → type is chosen based on configured weights (Rain, Storm, Green Rain).  
- Want strict percentages? → Keep *Force sunny on failure* enabled.  
- Want to bypass vanilla restrictions (Green Rain / Year 1 Spring)? → Enable the override options.

---

## 🛠️ How It Works

- At day’s end, Rain Tuner rolls your configured chance.  
- On success → picks a type (Rain, Storm, Green Rain, or Snow in winter).  
- On failure (with *Force sunny* enabled) → sets Sunny.  
- Hotkey sets tomorrow’s weather directly using the same logic.  
- Festival/wedding days are protected if the option is enabled.

---

## 👥 Multiplayer

- Weather is **host-determined**.  
- Only the **host** needs this mod for effects to apply.  
- Non-hosts can install safely, but it won’t affect the session.

---

## 🔄 Compatibility

- Compatible with **SMAPI 4.3.2+** and Stardew Valley **1.6.x**  
- Works with **Cloudy Skies** – Rain Tuner sets rain/snow flags, Cloudy Skies picks the variant.  
- Mods that also set tomorrow’s weather may conflict → last mod wins  

---

## ❓ Troubleshooting

- Nothing changes?  
  - Check if tomorrow is a festival or wedding  
  - Try pressing **F9** and sleeping  
  - Check `SMAPI-latest.txt` for `Rain Tuner` messages  

- Why no override?  
  - In Year 1 Spring, days 1–5 are hardcoded by vanilla.  
  - Enable the *Spring Year 1 override* option to bypass.  
  - Green Rain is only valid on certain days unless you enable the override.

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
