namespace FuelManager
{
	[HarmonyPatch(typeof(Panel_Inventory_Examine), nameof(Panel_Inventory_Examine.OnUnload))]
	internal class Panel_Inventory_Examine_OnUnload
	{
		private static bool Prefix(Panel_Inventory_Examine __instance)
		{
			if (!__instance.IsPanelPatchable()) return true;
			if (__instance.m_GearItem == null) return true;

			GearItem gi = __instance.m_GearItem;

			try
			{
				if (Fuel.IsFuelItem(gi))
				{
					Fuel.Drain(gi, __instance);
				}
			}
			catch (Exception e)
			{
				Main.Logger.Log($"OnUnload failed\n", FlaggedLoggingLevel.Exception, e);
			}
			return true;
		}
	}
}
