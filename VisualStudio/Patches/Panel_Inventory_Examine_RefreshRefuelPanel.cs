using Il2CppTLD.IntBackedUnit;

namespace FuelManager
{
	[HarmonyPatch(typeof(Panel_Inventory_Examine), nameof(Panel_Inventory_Examine.RefreshRefuelPanel))]
	internal class Panel_Inventory_Examine_RefreshRefuelPanel
	{
		private static bool Prefix(Panel_Inventory_Examine __instance)
		{
			if (!__instance.IsPanelPatchable()) return true;

			var gi = __instance.m_GearItem;

			if (gi == null) return true;
			if (!Fuel.IsFuelItem(gi)) return true;

			Main.Logger.Log($"Current GearItem: {gi.name} is a FuelItem and is not null", FlaggedLoggingLevel.Debug);

			__instance.m_RefuelPanel.SetActive(false);
			__instance.m_Button_Refuel.gameObject.SetActive(true);
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

			bool fuelIsAvailable    = totalCurrent > ItemLiquidVolume.Zero;
			bool flag               = fuelIsAvailable && !(currentLiters.m_Units == capacityLiters.m_Units);

			__instance.m_Refuel_X.gameObject.SetActive(!flag);
			__instance.m_Button_Refuel.gameObject.GetComponent<Panel_Inventory_Examine_MenuItem>().SetDisabled(!flag);

			try
			{
				if (gi != null && !Fuel.IsKeroseneLamp(gi)) __instance.m_Button_RefuelBackground.SetActive(true);
			}
			catch (Exception e)
			{
				Main.Logger.Log($"Attempting to set the m_Button_RefuelBackground active failed", FlaggedLoggingLevel.Exception, e);
			}

			InterfaceManager.GetPanel<Panel_Inventory_Examine>().m_MouseRefuelButton.SetActive(flag);
			__instance.m_RequiresFuelMessage.SetActive(false);

			__instance.m_LanternFuelAmountLabel.text = $"{Fuel.GetLiquidQuantityStringNoOunces(currentLiters)} / {Fuel.GetLiquidQuantityStringNoOunces(capacityLiters)}";
			__instance.m_FuelSupplyAmountLabel.text = $"{Fuel.GetLiquidQuantityStringNoOunces(totalCurrent)} / {Fuel.GetLiquidQuantityStringNoOunces(totalCapacity)}";

			__instance.UpdateWeightAndConditionLabels();

			return false; // MUST BE FALSE
		}
	}
}
