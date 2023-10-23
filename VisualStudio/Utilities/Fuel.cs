using Il2CppNodeCanvas.Tasks.Actions;
using Il2CppTLD.Gear;

namespace FuelManager
{
    internal class Fuel
    {
        // Panels
        internal static Panel_Inventory_Examine _Panel_Inventory_Examine    = new();
        internal static Panel_OptionsMenu _Panel_OptionsMenu                = new();
        internal static Panel_GenericProgressBar _Panel_GenericProgressBar  = new();

        internal static LiquidItem _LiquidItem                              = new();
        //KeroseneLampItem _KeroseneLampItem                                  = _Panel_Inventory_Examine.m_GearItem.GetComponent<KeroseneLampItem>();

        public const float MIN_LITERS                                       = 0.001f;
        private const string REFUEL_AUDIO                                   = "Play_SndActionRefuelLantern";
        internal static readonly float REFUEL_TIME                          = Settings.Instance.refuelTime;
        //private const float REFUEL_TIME                                     = 3f;

        #region Add

        /// <summary>
        /// 
        /// </summary>
        /// <param name="gearItem"></param>
        /// <param name="liters"></param>
        public static void AddLiters(GearItem gearItem, float liters)
        {
            if (gearItem == null) return;
            if (liters == 0) return;

            if (IsKeroseneLamp(gearItem))
            {
                gearItem.m_KeroseneLampItem.m_CurrentFuelLiters += liters;
                gearItem.m_KeroseneLampItem.m_CurrentFuelLiters = Mathf.Clamp(gearItem.m_KeroseneLampItem.m_CurrentFuelLiters, MIN_LITERS, gearItem.m_KeroseneLampItem.m_MaxFuelLiters);
            }
            else if (IsFuelContainer(gearItem))
            {
                gearItem.m_LiquidItem.m_LiquidLiters += liters;
            }
        }

        public static void AddTotalCurrentLiters(float liters, GearItem excludeItem)
        {
            float remaining = liters;

            foreach (GameObject eachItem in GameManager.GetInventoryComponent().m_Items)
            {
                GearItem gearItem = eachItem.GetComponent<GearItem>();
                if (gearItem == null || gearItem == excludeItem) continue;

                LiquidItem liquidItem = gearItem.m_LiquidItem;
                if (liquidItem == null || liquidItem.m_LiquidType != FuelManager.GetKerosene()) continue;

                float previousLiters = liquidItem.m_LiquidLiters;
                liquidItem.m_LiquidLiters = Mathf.Clamp(liquidItem.m_LiquidLiters + remaining, 0f, liquidItem.m_LiquidCapacityLiters);
                float transferred = liquidItem.m_LiquidLiters - previousLiters;

                remaining -= transferred;

                if (Mathf.Abs(remaining) < MIN_LITERS) break;
            }
        }

        #endregion

        #region Is

        /// <summary>
        /// Is the gear item purely a container for kerosene?
        /// </summary>
        /// <returns>True if gearItem.m_LiquidItem is not null and is for kerosene.</returns>
        internal static bool IsFuelContainer(GearItem gearItem)
        {
            if (gearItem == null) return false;
            if (!gearItem.m_LiquidItem) return false;
            return gearItem.m_LiquidItem.m_LiquidType == FuelManager.GetKerosene();
        }

        /// <summary>
        /// Is the gear item a kerosene lamp?
        /// </summary>
        /// <returns>True if gearItem.m_KeroseneLampItem is not null.</returns>
        internal static bool IsKeroseneLamp(GearItem gearItem)
        {
            return gearItem.m_KeroseneLampItem != null;
        }

        /// <summary>
        /// Can the gear item hold kerosene?
        /// </summary>
        /// <returns>True if the gear item is a fuel container or is a kerosene lamp.</returns>
        internal static bool IsFuelItem(GearItem gearItem)
        {
            return IsFuelContainer(gearItem) || IsKeroseneLamp(gearItem);
        }
        #endregion

        #region Get

        /// <summary>
        /// Returns a liquid quantity string with respect to the game units;
        /// </summary>
        internal static string GetLiquidQuantityString(float quantityLiters)
        {
            return Il2Cpp.Utils.GetLiquidQuantityString(_Panel_OptionsMenu.State.m_Units, quantityLiters);
        }

        /// <summary>
        /// Returns a liquid quantity string with respect to the game units;
        /// </summary>
        internal static string GetLiquidQuantityStringNoOunces(float quantityLiters)
        {
            return Il2Cpp.Utils.GetLiquidQuantityStringNoOunces(_Panel_OptionsMenu.State.m_Units, quantityLiters);
        }

        /// <summary>
        /// Returns a liquid quantity string with respect to the game units;
        /// </summary>
        internal static string GetLiquidQuantityStringWithUnits(float quantityLiters)
        {
            return Il2Cpp.Utils.GetLiquidQuantityStringWithUnits(_Panel_OptionsMenu.State.m_Units, quantityLiters);
        }

        /// <summary>
        /// Returns a liquid quantity string with respect to the game units;
        /// </summary>
        internal static string GetLiquidQuantityStringWithUnitsNoOunces(float quantityLiters)
        {
            return Il2Cpp.Utils.GetLiquidQuantityStringWithUnitsNoOunces(_Panel_OptionsMenu.State.m_Units, quantityLiters);
        }

        /// <summary>
        /// Returns a liquid quantity string with respect to the game units;
        /// </summary>
        internal static float GetLitersToDrain(GearItem gearItem)
        {
            return Mathf.Min(
                        GetIndividualCurrentLiters(gearItem),  //available fuel
                        GetTotalSpaceLiters(gearItem)          //available capacity
                        );
        }

        internal static float GetLitersToRefuel(GearItem gearItem)
        {
            return Mathf.Min(
                        GetIndividualSpaceLiters(gearItem),     //amount of space in the fuel container
                        GetTotalCurrentLiters(gearItem)         //current amount of kerosene in other containers
                        );
        }

        /// <summary>
        /// Returns the current capacity (in liters) of kerosene for the gear item.
        /// </summary>
        internal static float GetIndividualCapacityLiters(GearItem gearItem)
        {
            if (IsFuelContainer(gearItem)) return gearItem.m_LiquidItem.m_LiquidCapacityLiters;
            else if (IsKeroseneLamp(gearItem)) return gearItem.m_KeroseneLampItem.m_MaxFuelLiters;
            else return 0;
        }

        /// <summary>
        /// Returns the current amount (in liters) of kerosene in the gear item.
        /// </summary>
        internal static float GetIndividualCurrentLiters(GearItem gearItem)
        {
            if (IsFuelContainer(gearItem)) return gearItem.m_LiquidItem.m_LiquidLiters;
            else if (IsKeroseneLamp(gearItem)) return gearItem.m_KeroseneLampItem.m_CurrentFuelLiters;
            else return 0;
        }

        /// <summary>
        /// Get the amount of space in the fuel container.
        /// </summary>
        /// <param name="gearItem">The fuel container being investigated.</param>
        /// <returns>The amount (in liters) of empty space in the fuel container.</returns>
        internal static float GetIndividualSpaceLiters(GearItem gearItem)
        {
            return GetIndividualCapacityLiters(gearItem) - GetIndividualCurrentLiters(gearItem);
        }

        /// <summary>
        /// Get the total capacity of all other fuel containers in the inventory.
        /// </summary>
        /// <param name="excludeItem">The gear item to be excluded from the calculations.</param>
        /// <returns>The total capacity (in liters) from inventory fuel containers.</returns>
        internal static float GetTotalCapacityLiters(GearItem excludeItem)
        {
            float result = 0;

            foreach (GameObject eachItem in GameManager.GetInventoryComponent().m_Items)
            {
                GearItem? gearItem = eachItem?.GetComponent<GearItem>();
                if (gearItem == null || gearItem == excludeItem || !IsFuelContainer(gearItem))
                {
                    continue;
                }

                result += GetIndividualCapacityLiters(gearItem);
            }

            return result;
        }

        /// <summary>
        /// Get the total kerosene quantity of all other fuel containers in the inventory.
        /// </summary>
        /// <param name="excludeItem">The gear item to be excluded from the calculations.</param>
        /// <returns>The total kerosene quantity (in liters) from other inventory fuel containers.</returns>
        internal static float GetTotalCurrentLiters(GearItem excludeItem)
        {
            float result = 0;

            foreach (GameObject eachItem in GameManager.GetInventoryComponent().m_Items)
            {
                GearItem gearItem = eachItem.GetComponent<GearItem>();
                if (gearItem == null || gearItem == excludeItem || !IsFuelContainer(gearItem)) continue;

                result += GetIndividualCurrentLiters(gearItem);
            }

            return result;
        }

        /// <summary>
        /// Get the total empty space of all other fuel containers in the inventory.
        /// </summary>
        /// <param name="excludeItem">The gear item to be excluded from the calculations.</param>
        /// <returns>The total empty space (in liters) from other inventory fuel containers.</returns>
        internal static float GetTotalSpaceLiters(GearItem excludeItem)
        {
            float result = 0;

            foreach (GameObject eachItem in GameManager.GetInventoryComponent().m_Items)
            {
                GearItem gearItem = eachItem.GetComponent<GearItem>();
                if (gearItem != null && gearItem != excludeItem && IsFuelContainer(gearItem))
                {
                    result += GetIndividualSpaceLiters(gearItem);
                }
            }

            return result;
        }

        public static void DoRefreshPanel()
        {
            Panel_Inventory_Examine panel = InterfaceManager.GetPanel<Panel_Inventory_Examine>();

            if (panel.IsEnabled())
            {
                panel.RefreshMainWindow();
            }
        }

        #endregion
        #region Actions

        public static void Drain(GearItem gi, bool RestoreInHands, Panel_Inventory_Examine? panel = null)
        {
            GearItem? Target;
            if (panel == null)
            {
                Target = gi;
            }
            else
            {
                Target = panel.m_GearItem;
            }

            if (Target == null) return;
            Drain(Target, RestoreInHands);
        }

        internal static void Drain(GearItem gearItem, bool RestoreInHands)
        {
            float currentLiters     = GetIndividualCurrentLiters(gearItem);
            float totalCapacity     = GetTotalCapacityLiters(gearItem);
            float totalCurrent      = GetTotalCurrentLiters(gearItem);

            if (Settings.Instance.ExtraLogging)
            {
                Logging.Log($"currentLiters: {currentLiters}, totalCurrent {totalCurrent}, totalCapacity: {totalCapacity}");
                Logging.Log($"item is {gearItem.name}");
            }

            if (currentLiters < MIN_LITERS)
            {
                HUDMessage.AddMessage(Localization.Get("GAMEPLAY_BFM_AlreadyEmpty"));
                GameAudioManager.PlayGUIError();
                return;
            }

            if (Il2Cpp.Utils.Approximately(totalCapacity, totalCurrent, MIN_LITERS))
            {
                HUDMessage.AddMessage(Localization.Get("GAMEPLAY_BFM_NoFuelCapacityAvailable"));
                GameAudioManager.PlayGUIError();
                return;
            }

            GameAudioManager.PlayGuiConfirm();

            InterfaceManager.GetPanel<Panel_GenericProgressBar>().Launch(
                name:                       Localization.Get("GAMEPLAY_BFM_DrainingProgress"),
                seconds:                    REFUEL_TIME,
                minutes:                    0f,
                randomFailureThreshold:     0f,
                audioName:                  REFUEL_AUDIO,
                voiceName:                  null,
                supressHeavyBreathing:      false,
                skipRestoreInHands:         RestoreInHands,
                del:                        new Action<bool, bool, float>(OnDrainFinished)
            );

            ButtonUtils.SetButtonLocalizationKey(InterfaceManager.GetPanel<Panel_Inventory_Examine>().m_RefuelPanel.GetComponentInChildren<UIButton>(), "GAMEPLAY_Refuel");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="gi"></param>
        /// <param name="panel"></param>
        public static void Refuel(GearItem gi, bool RestoreInHands, Panel_Inventory_Examine? panel = null)
        {
            GearItem? Target;
            if (panel == null)
            {
                Target = gi;
            }
            else
            {
                Target = panel.m_GearItem;
            }

            if (Target == null) return;
            Refuel(Target, RestoreInHands);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="gearItem"></param>
        internal static void Refuel(GearItem gearItem, bool RestoreInHands)
        {

            float currentLiters             = GetIndividualCurrentLiters(gearItem);
            float capacityLiters            = GetIndividualCapacityLiters(gearItem);
            float totalCurrent              = GetTotalCurrentLiters(gearItem);

            float totalInventoryFuel        = GameManager.GetPlayerManagerComponent().GetCapacityLiters(FuelManager.GetKerosene());
            float totalInventoryCapacity    = GameManager.GetPlayerManagerComponent().GetTotalLiters(FuelManager.GetKerosene());

            if (Settings.Instance.ExtraLogging)
            {
                Logging.Log($"currentLiters: {currentLiters}, capacityLiters: {capacityLiters}, totalCurrent: {totalCurrent}");
                Logging.Log($"item is {gearItem.name}, GetComponent is {gearItem.name}");
                Logging.Log($"panel item is {gearItem.name}, panel item GetComponent is {gearItem.name}");
            }

            if (Utils.Approximately(currentLiters, capacityLiters, MIN_LITERS))
            {
                GameAudioManager.PlayGUIError();
                HUDMessage.AddMessage(Localization.Get("GAMEPLAY_BFM_AlreadyFilled"), true, true);
                return;
            }

            if (totalCurrent < MIN_LITERS)
            {
                GameAudioManager.PlayGUIError();
                HUDMessage.AddMessage(Localization.Get("GAMEPLAY_NoKeroseneavailable"), false);
                return;
            }

            FuelManager.Target = gearItem;

            GameAudioManager.PlayGuiConfirm();
            InterfaceManager.GetPanel<Panel_GenericProgressBar>().Launch(
                name:                       Localization.Get("GAMEPLAY_RefuelingProgress"),
                seconds:                    REFUEL_TIME,
                minutes:                    0f,
                randomFailureThreshold:     0f,
                audioName:                  REFUEL_AUDIO,
                voiceName:                  null,
                supressHeavyBreathing:      false,
                skipRestoreInHands:         RestoreInHands,
                del:                        new Action<bool, bool, float>(OnRefuelFinished)
            );
        }

        #endregion
        #region OnActions

        private static void OnDrainFinished(bool success, bool playerCancel, float progress)
        {
            GearItem? Target = FuelManager.Target;

            if (Target != null && IsFuelItem(Target))
            {
                float litersToDrain = GetLitersToDrain(Target) * progress;

                AddTotalCurrentLiters(litersToDrain, Target);
                AddLiters(Target, -litersToDrain);

                DoRefreshPanel();
            }
        }

        private static void OnRefuelFinished(bool success, bool playerCancel, float progress)
        {
            GearItem? Target = FuelManager.Target;

            if (Target != null && IsFuelItem(Target))
            {
                float litersToTransfer = GetLitersToRefuel(Target) * progress;

                AddTotalCurrentLiters(-litersToTransfer, Target);
                AddLiters(Target, litersToTransfer);

                DoRefreshPanel();
            }
        }
        #endregion
    }   
}
