# FuelManager
| Version | Downloads | License |
| :---: | :---: | :---: |
| ![GitHub release (with filter)](https://img.shields.io/github/v/release/Arkhorse/FuelManager) | ![GitHub all releases](https://img.shields.io/github/downloads/Arkhorse/FuelManager/total) | ![GitHub](https://img.shields.io/github/license/Arkhorse/FuelManager) |

This mod completely replaces [Better-Fuel-Management](https://github.com/ds5678/Better-Fuel-Management) by [ds5678](https://github.com/ds5678).

## Features
* Fuel containers do not automatically vanish when they are empty.
* Fuel containers do not automatically appear when they are needed.
* If an action produces (e.g. cooking fish) or retrieves (e.g. harvesting a Kerosene Lamp) fuel and there is no appropriate container with sufficient capacity in the inventory, that fuel will be lost. You will need to manage your empty fuel containers!
* The fuel in the Lantern's can be drained.
* Transfer fuel between fuel containers.
* Ability to harvest the Jerry Can and the Lamp Fuel items.
* Adds a new `GearItem`, the Gas Can. Can hold up to 6L.
* Ability to refuel Kerosene lamps using a hotkey (https://github.com/Arkhorse/FuelManager/pull/7)

### Disabled Features:
* A custom radial menu has been added to make placing fuel containers easier. You can change the key or disable the menu in the Mod Settings.
  * This is disabled until I can fully update `RadialMenuUtilities`

## Special Thanks

[WulfMarius](https://github.com/WulfMarius) is the original creator of Better Fuel Management and [ds5678](https://github.com/ds5678) for updating BFM.

## Installation

### Requirements
* [KeyboardUtilities](https://github.com/ds5678/KeyboardUtilities/releases/latest)
* [LocalizationUtilities](https://github.com/dommrogers/LocalizationUtilities/releases/latest)
* [CraftingRevisions](https://github.com/dommrogers/CraftingRevisions/release/latest)
* [ModSettings](https://github.com/zeobviouslyfakeacc/ModSettings/releases/latest)
* [GearSpawner](https://github.com/dommrogers/GearSpawner/releases/latest)
* [ModComponent](https://github.com/dommrogers/ModComponent/releases/latest)
<!--* [RadialMenuUtilities](https://github.com/Arkhorse/RadialMenuUtilities/releases/latest)-->

### Install Steps
* Download `FuelManager.dll`, `FuelManager.Shared.modcomponent` and `FuelManager.modcomponent` from the [releases page](https://github.com/Arkhorse/FuelManager/releases)
* Move `FuelManager.dll`, `FuelManager.Shared.modcomponent` and `FuelManager.modcomponent` into your mods folder.

## Optional Addons
### Modders Gear Toolbox
#### Additional Installation Instructions:
 * Download [Modders Gear Toolbox](https://github.com/Jods-Its/Modders-Gear-Toolbox)
 * Download the Fuel Manager [`.modcomponent`](https://github.com/Arkhorse/FuelManager/releases/latest/download/FuelManager.ModdersGearToolbox.modcomponent) for Modders Gear Toolbox
 * **Remove the default modcomponent (`FuelManager.modcomponent`)**

## Localizations

You can help make Better-Fuel-Management even better! Better-Fuel-Management has localized text that can be translated into other languages. You can find a list of compatible languages [here](https://github.com/dommrogers/ModComponent/blob/master/docs/Localizations.md) and the localized text file [here](https://github.com/Arkhorse/FuelManager/blob/main/Unity/Assets/Localization.json). You can contribute your translations by doing the following:
* Make a Github account
* [Fork](https://docs.github.com/en/github/collaborating-with-pull-requests/working-with-forks/about-forks) this project
* Edit the localization file on your fork
* Make a [pull request](https://docs.github.com/en/github/collaborating-with-pull-requests/proposing-changes-to-your-work-with-pull-requests/about-pull-requests) to this repository

## Contributers

[Xpazeman](https://github.com/Xpazeman) contributed the Spanish localization.

Santana contributed the Japanese localization.

Encinal contributed the French localization.

Fanerkin contributed to the Russian localization.

Oguzhan contributed the Dutch and Turkish localizations.

finnjvl contributed the Swedish and Finnish localizations.

[ttr](https://github.com/ttr) and Sovereign contributed the Polish localization.

