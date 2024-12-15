namespace FuelManager
{
	[HarmonyPatch(typeof(Panel_Inventory_Examine), nameof(Panel_Inventory_Examine.OnRefuel))]
	internal class Panel_Inventory_Examine_OnRefuel
	{
		private static bool Prefix(Panel_Inventory_Examine __instance)
		{
			if (!__instance.IsPanelPatchable()) return true;
			if (__instance.m_GearItem == null) return true;

			GearItem gi = __instance.m_GearItem;
			bool drain = false;

			try
			{
				drain = Buttons.IsSelected(__instance.m_Button_Unload);
			}
			catch (Exception e)
			{
				Main.Logger.Log($"Attempting to check if the unload button is selected failed\n", FlaggedLoggingLevel.Exception, e);
				return true;
			}
			try
			{
				if (Fuel.IsFuelItem(gi))
				{
					Main.Logger.Log($"Begin OnRefuel patch for {gi.name}", FlaggedLoggingLevel.Debug);
					if (drain)
					{
						Fuel.Drain(gi, __instance);
					}
					else
					{
						Fuel.Refuel(gi, true, __instance);
					}
					return false;
				}
			}
			catch (Exception e)
			{
				Main.Logger.Log($"Attempting to ", FlaggedLoggingLevel.Exception, e);
			}
			return true;
		}
	}
}
