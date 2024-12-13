namespace FuelManager
{
    [HarmonyPatch(typeof(Panel_Inventory_Examine), nameof(Panel_Inventory_Examine.UpdateButtonLegend))]
    internal class Panel_Inventory_Examine_UpdateButtonLegend
    {
        private static void Postfix(Panel_Inventory_Examine __instance)
        {
            if (!__instance.IsPanelPatchable()) return;
			if (__instance.m_GearItem == null) return;
			if (Fuel.IsFuelItem(__instance.m_GearItem) && Buttons.IsSelected(__instance.m_Button_Unload))
            {
                __instance.m_ButtonLegendContainer.UpdateButton("Continue", "GAMEPLAY_BFM_Drain", true, 1, true);
            }
        }
    }
}
