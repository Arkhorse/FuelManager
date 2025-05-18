using Il2CppTLD.IntBackedUnit;

namespace FuelManager
{
	[HarmonyPatch(typeof(Panel_Inventory_Examine), nameof(Panel_Inventory_Examine.RefreshRefuelPanel))]
	internal class Panel_Inventory_Examine_RefreshRefuelPanel
	{
		private static bool Prefix(ref Panel_Inventory_Examine __instance)
		{
			if (!__instance.IsPanelPatchable(out string reason))
			{
				Main.Logger.Log($"Attempting to patch {nameof(Panel_Inventory_Examine.RefreshRefuelPanel)} failed. Reason: {reason}", FlaggedLoggingLevel.Verbose);
				return true;
			}
			GearItem gi = __instance.m_GearItem;

			if (gi == null) return true;
			if (!Fuel.IsFuelItem(gi)) return true;

			Main.Logger.Log($"Current GearItem: {gi.name} is a FuelItem and is not null", FlaggedLoggingLevel.Debug);

			try
			{
				__instance.m_RefuelPanel.SetActive(false);
			}
			catch (Exception e)
			{
				Main.Logger.Log($"Attempting to set __instance.m_RefuelPanel not active failed", FlaggedLoggingLevel.Exception, e);
			}
			try
			{
				__instance.m_Button_Refuel.gameObject.SetActive(true);
			}
			catch (Exception e)
			{
				Main.Logger.Log($"Attempting to set __instance.m_Button_Refuel active failed", FlaggedLoggingLevel.Exception, e);
			}
			//bool GamePad = Utils.IsGamepadActive();
			//__instance.m_RequiresFuelMessage.SetActive(false && GamePad);
			//__instance.m_MouseRefuelButton.SetActive(true);
			//__instance.m_RequiresFuelMessage.SetActive(!GamePad);

			//__instance.m_Button_RefuelBackground.SetActive(true); // DONT ENABLE. BREAKS EVERYTHING

			// __instance.m_GearItem.GetComponent<GearItem>()
			ItemLiquidVolume currentLiters     = Fuel.GetIndividualCurrentLiters(gi);
			ItemLiquidVolume capacityLiters    = Fuel.GetIndividualCapacityLiters(gi);
			ItemLiquidVolume totalCurrent      = Fuel.GetTotalCurrentLiters(gi);
			ItemLiquidVolume totalCapacity     = Fuel.GetTotalCapacityLiters(gi);

			bool fuelIsAvailable    = !Constants.Empty(totalCurrent);
			bool flag               = fuelIsAvailable && !(currentLiters.m_Units == capacityLiters.m_Units);

			try
			{
				__instance.m_Refuel_X.gameObject.SetActive(!flag);
			}
			catch (Exception e)
			{
				Main.Logger.Log($"Attempting to set __instance.m_Refuel_X {(!flag == true ? "active" : "not active")} failed", FlaggedLoggingLevel.Exception, e);
			}
			try
			{
				__instance.m_Button_Refuel.gameObject.GetComponent<Panel_Inventory_Examine_MenuItem>().SetDisabled(!flag);
			}
			catch (Exception e)
			{
				Main.Logger.Log($"Attempting to set __instance.m_Button_Refuel {(!flag == true ? "active" : "not active")} failed", FlaggedLoggingLevel.Exception, e);
			}
			try
			{
				if (gi != null && !Fuel.IsKeroseneLamp(gi)) __instance.m_Button_RefuelBackground.SetActive(true);
			}
			catch (Exception e)
			{
				Main.Logger.Log($"Attempting to set the m_Button_RefuelBackground active failed", FlaggedLoggingLevel.Exception, e);
			}
			try
			{
				__instance.m_MouseRefuelButton.SetActive(flag);
			}
			catch (Exception e)
			{
				Main.Logger.Log($"Attempting to set __instance.m_Button_Refuel {(flag == true ? "active" : "not active")} failed", FlaggedLoggingLevel.Exception, e);
			}
			try
			{
				__instance.m_RequiresFuelMessage.SetActive(!flag);
			}
			catch (Exception e)
			{
				Main.Logger.Log($"Attempting to set __instance.m_Button_Refuel not active failed", FlaggedLoggingLevel.Exception, e);
			}

			__instance.m_LanternFuelAmountLabel.text = $"{Fuel.GetLiquidQuantityString(currentLiters)} / {Fuel.GetLiquidQuantityString(capacityLiters)}";
			__instance.m_FuelSupplyAmountLabel.text = $"{Fuel.GetLiquidQuantityString(totalCurrent)} / {Fuel.GetLiquidQuantityString(totalCapacity)}";

			try
			{
				__instance.UpdateWeightAndConditionLabels();
			}
			catch (Exception e)
			{
				Main.Logger.Log($"Attempting to __instance.UpdateWeightAndConditionLabels() failed", FlaggedLoggingLevel.Exception, e);
			}
			
			return false; // MUST BE FALSE
		}
	}
}
