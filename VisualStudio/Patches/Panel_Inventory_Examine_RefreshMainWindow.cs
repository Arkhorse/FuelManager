using Il2CppTLD.IntBackedUnit;

namespace FuelManager
{
    [HarmonyPatch(typeof(Panel_Inventory_Examine), nameof(Panel_Inventory_Examine.RefreshMainWindow))]
    internal class Panel_Inventory_Examine_RefreshMainWindow
    {
        private static void Postfix(Panel_Inventory_Examine __instance)
        {
			if (!__instance.IsPanelPatchable()) return;
			if (__instance.m_GearItem == null) return;

            Main.Logger.Log($"Panel is patchable, gearitem is not null", FlaggedLoggingLevel.Verbose);

            GearItem gi = __instance.m_GearItem;

			try
			{
				if (Fuel.IsFuelItem(gi))
				{
					//Main.Logger.Log($"Panel_Inventory_Examine_RefreshMainWindow: {gi.name}", FlaggedLoggingLevel.Debug);
					Vector3 position = Buttons.GetBottomPosition(
													__instance.m_Button_Harvest,
													__instance.m_Button_Refuel,
													__instance.m_Button_Repair
													);
					position.y += __instance.m_ButtonSpacing;
					__instance.m_Button_Unload.transform.localPosition = position;

					__instance.m_Button_Unload.gameObject.SetActive(true);

					ItemLiquidVolume litersToDrain = Fuel.GetLitersToDrain(gi);
					__instance.m_Button_Unload.GetComponent<Panel_Inventory_Examine_MenuItem>().SetDisabled(litersToDrain <= ItemLiquidVolume.Zero);
				}
			}
			catch (Exception e)
			{
				Main.Logger.Log($"RefreshMainWindow failed\n", FlaggedLoggingLevel.Exception, e);
			}
		}
	}
}
