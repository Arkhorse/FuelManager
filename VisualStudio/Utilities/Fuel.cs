using Il2CppNodeCanvas.Tasks.Actions;
using Il2CppTLD.Gear;
using Il2CppTLD.IntBackedUnit;

namespace FuelManager
{
	internal class Fuel
	{
		//public static ItemLiquidVolume MIN_LITERS                           = ItemLiquidVolume.FromLiters(0.001f);
		private const string REFUEL_AUDIO                                   = "Play_SndActionRefuelLantern";
		internal static readonly float REFUEL_TIME                          = Settings.Instance.refuelTime;
		//private const float REFUEL_TIME                                     = 3f;

		#region Add
		/// <summary>
		/// 
		/// </summary>
		/// <param name="gearItem"></param>
		/// <param name="liters"></param>
		public static void AddLiters(GearItem gearItem, ItemLiquidVolume liters)
		{
			if (gearItem == null) return;
			if (liters == ItemLiquidVolume.Zero) return;
			ItemLiquidVolume volume = liters;

			if (IsKeroseneLamp(gearItem))
			{
				gearItem.m_KeroseneLampItem.m_CurrentFuelLiters += volume;
				gearItem.m_KeroseneLampItem.m_CurrentFuelLiters = ItemLiquidVolume.Clamp(gearItem.m_KeroseneLampItem.m_CurrentFuelLiters, ItemLiquidVolume.Zero, gearItem.m_KeroseneLampItem.m_MaxFuel);
			}
			else if (IsFuelContainer(gearItem))
			{
				gearItem.m_LiquidItem.m_Liquid += volume;
				gearItem.m_LiquidItem.m_Liquid = ItemLiquidVolume.Clamp(gearItem.m_LiquidItem.m_Liquid, ItemLiquidVolume.Zero, gearItem.m_LiquidItem.m_LiquidCapacity);
			}
		}

		public static void AddTotalCurrentLiters(ItemLiquidVolume liters, GearItem excludeItem)
		{
			ItemLiquidVolume remaining = liters;

			foreach (GameObject eachItem in GameManager.GetInventoryComponent().m_Items)
			{
				GearItem gearItem = eachItem.GetComponent<GearItem>();
				if (gearItem == null || gearItem == excludeItem) continue;

				LiquidItem liquidItem = gearItem.m_LiquidItem;
				if (liquidItem == null || liquidItem.m_LiquidType != Main.GetKerosene()) continue;

				ItemLiquidVolume previousLiters = liquidItem.m_Liquid;
				liquidItem.m_Liquid = ItemLiquidVolume.Clamp(liquidItem.m_Liquid + remaining, ItemLiquidVolume.Zero, liquidItem.m_LiquidCapacity);
				ItemLiquidVolume transferred = liquidItem.m_Liquid - previousLiters;

				remaining -= transferred;

				if (remaining == ItemLiquidVolume.Zero) break;
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
			if (gearItem.m_LiquidItem == null) return false;

			bool kerosene = false;

			try
			{
				kerosene = gearItem.m_LiquidItem.m_LiquidType == Main.GetKerosene();
			}
			catch (Exception e)
			{
				Main.Logger.Log($"Attempting to get kerosene failed\n", FlaggedLoggingLevel.Exception, e);
			}
			return kerosene;
		}

		/// <summary>
		/// Is the gear item a kerosene lamp?
		/// </summary>
		/// <returns>True if gearItem.m_KeroseneLampItem is not null.</returns>
		internal static bool IsKeroseneLamp(GearItem gearItem)
		{
			bool lamp = false;

			try
			{
				lamp = gearItem.m_KeroseneLampItem != null;
			}
			catch (Exception e)
			{
				Main.Logger.Log($"Attempting to get m_KeroseneLampItem failed\n", FlaggedLoggingLevel.Exception, e);
			}

			if (!lamp)
			{
				try
				{
					lamp = gearItem.gameObject.GetComponent<KeroseneLampItem>() != null;
				}
				catch (Exception e)
				{
					Main.Logger.Log($"Attempting to get lamp failed\n", FlaggedLoggingLevel.Exception, e);
				}
			}

			return lamp;
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
		internal static string GetLiquidQuantityString(ItemLiquidVolume quantityLiters)
		{
			return quantityLiters.ToStringMetric();
		}

		/// <summary>
		/// Returns a liquid quantity string with respect to the game units;
		/// </summary>
		internal static string GetLiquidQuantityStringNoOunces(ItemLiquidVolume quantityLiters)
		{
			return quantityLiters.ToStringMetric();
		}

		/// <summary>
		/// Returns a liquid quantity string with respect to the game units;
		/// </summary>
		internal static string GetLiquidQuantityStringWithUnits(ItemLiquidVolume quantityLiters)
		{
			return quantityLiters.ToStringMetric();
		}

		/// <summary>
		/// Returns a liquid quantity string with respect to the game units;
		/// </summary>
		internal static string GetLiquidQuantityStringWithUnitsNoOunces(ItemLiquidVolume quantityLiters)
		{
			return quantityLiters.ToStringMetric();
		}

		/// <summary>
		/// Returns a liquid quantity string with respect to the game units;
		/// </summary>
		internal static ItemLiquidVolume GetLitersToDrain(GearItem gearItem)
		{
			return ItemLiquidVolume.Min(
						GetIndividualCurrentLiters(gearItem),  //available fuel
						GetTotalSpaceLiters(gearItem)          //available capacity
						);
		}

		internal static ItemLiquidVolume GetLitersToRefuel(GearItem gearItem)
		{
			return ItemLiquidVolume.Min(
						GetIndividualSpaceLiters(gearItem),     //amount of space in the fuel container
						GetTotalCurrentLiters(gearItem)         //current amount of kerosene in other containers
						);
		}

		/// <summary>
		/// Returns the current capacity (in liters) of kerosene for the gear item.
		/// </summary>
		internal static ItemLiquidVolume GetIndividualCapacityLiters(GearItem gearItem)
		{
			if (IsFuelContainer(gearItem)) return gearItem.m_LiquidItem.m_LiquidCapacity;
			else if (IsKeroseneLamp(gearItem)) return gearItem.m_KeroseneLampItem.m_MaxFuel;
			else return ItemLiquidVolume.Zero;
		}

		/// <summary>
		/// Returns the current amount (in liters) of kerosene in the gear item.
		/// </summary>
		internal static ItemLiquidVolume GetIndividualCurrentLiters(GearItem gearItem)
		{
			if (IsFuelContainer(gearItem)) return gearItem.m_LiquidItem.m_Liquid;
			else if (IsKeroseneLamp(gearItem)) return gearItem.m_KeroseneLampItem.m_CurrentFuelLiters;
			else return ItemLiquidVolume.Zero;
		}

		/// <summary>
		/// Get the amount of space in the fuel container.
		/// </summary>
		/// <param name="gearItem">The fuel container being investigated.</param>
		/// <returns>The amount (in liters) of empty space in the fuel container.</returns>
		internal static ItemLiquidVolume GetIndividualSpaceLiters(GearItem gearItem)
		{
			return GetIndividualCapacityLiters(gearItem) - GetIndividualCurrentLiters(gearItem);
		}

		/// <summary>
		/// Get the total capacity of all other fuel containers in the inventory.
		/// </summary>
		/// <param name="excludeItem">The gear item to be excluded from the calculations.</param>
		/// <returns>The total capacity (in liters) from inventory fuel containers.</returns>
		internal static ItemLiquidVolume GetTotalCapacityLiters(GearItem excludeItem)
		{
			ItemLiquidVolume result = ItemLiquidVolume.Zero;

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
		internal static ItemLiquidVolume GetTotalCurrentLiters(GearItem excludeItem)
		{
			ItemLiquidVolume result = ItemLiquidVolume.Zero;

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
		internal static ItemLiquidVolume GetTotalSpaceLiters(GearItem excludeItem)
		{
			ItemLiquidVolume result = ItemLiquidVolume.Zero;

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
			ItemLiquidVolume currentLiters     = GetIndividualCurrentLiters(gearItem);
			ItemLiquidVolume totalCapacity     = GetTotalCapacityLiters(gearItem);
			ItemLiquidVolume totalCurrent      = GetTotalCurrentLiters(gearItem);

			Main.Logger.Log($"item is {gearItem.name}, currentLiters: {currentLiters}, totalCurrent {totalCurrent}, totalCapacity: {totalCapacity}", FlaggedLoggingLevel.Debug);

			if (currentLiters == ItemLiquidVolume.Zero)
			{
				HUDMessage.AddMessage(Localization.Get("GAMEPLAY_BFM_AlreadyEmpty"));
				GameAudioManager.PlayGUIError();
				return;
			}

			if (totalCapacity == totalCurrent)
			//if (Il2Cpp.Utils.Approximately((float)totalCapacity.m_Units, (float)totalCurrent.m_Units))
			{
				HUDMessage.AddMessage(Localization.Get("GAMEPLAY_BFM_NoFuelCapacityAvailable"));
				GameAudioManager.PlayGUIError();
				return;
			}

			Main.Target = gearItem;

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

			Buttons.SetButtonLocalizationKey(InterfaceManager.GetPanel<Panel_Inventory_Examine>().m_RefuelPanel.GetComponentInChildren<UIButton>(), "GAMEPLAY_Refuel");
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

			ItemLiquidVolume currentLiters             = GetIndividualCurrentLiters(gearItem);
			ItemLiquidVolume capacityLiters            = GetIndividualCapacityLiters(gearItem);
			ItemLiquidVolume totalCurrent              = GetTotalCurrentLiters(gearItem);

			ItemLiquidVolume totalInventoryFuel        = GameManager.GetPlayerManagerComponent().GetCapacityLiters(Main.GetKerosene());
			ItemLiquidVolume totalInventoryCapacity    = GameManager.GetPlayerManagerComponent().GetTotalLiters(Main.GetKerosene());

			Main.Logger.Log($"item is {gearItem.name}, currentLiters: {currentLiters}, capacityLiters: {capacityLiters}, totalCurrent: {totalCurrent}", FlaggedLoggingLevel.Debug);

			if (currentLiters == capacityLiters)
			//if (Il2Cpp.Utils.Approximately((float)currentLiters.m_Units, (float)capacityLiters.m_Units))
			{
				GameAudioManager.PlayGUIError();
				HUDMessage.AddMessage(Localization.Get("GAMEPLAY_BFM_AlreadyFilled"), true, true);
				return;
			}

			if (totalCurrent == ItemLiquidVolume.Zero)
			{
				GameAudioManager.PlayGUIError();
				HUDMessage.AddMessage(Localization.Get("GAMEPLAY_NoKeroseneavailable"), false);
				return;
			}

			Main.Target = gearItem;

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
			GearItem? Target = Main.Target;

			if (Target != null && IsFuelItem(Target))
			{
				ItemLiquidVolume litersToDrain = GetLitersToDrain(Target) * progress;

				AddTotalCurrentLiters(litersToDrain, Target);
				AddLiters(Target, -litersToDrain);

				DoRefreshPanel();
			}
		}

		private static void OnRefuelFinished(bool success, bool playerCancel, float progress)
		{
			GearItem? Target = Main.Target;

			if (Target != null && IsFuelItem(Target))
			{
				ItemLiquidVolume litersToTransfer = GetLitersToRefuel(Target) * progress;

				AddTotalCurrentLiters(-litersToTransfer, Target);
				AddLiters(Target, litersToTransfer);

				DoRefreshPanel();
			}
		}
		#endregion
	}
}
