namespace FuelManager
{
    [HarmonyPatch(typeof(Panel_Inventory_Examine), nameof(Panel_Inventory_Examine.OnRefuel))]
    internal class Panel_Inventory_Examine_OnRefuel
    {
        private static bool Prefix(Panel_Inventory_Examine __instance)
        {
			if (!__instance.IsPanelPatchable()) return true;
			if (__instance.m_GearItem == null) return true;
			if (Fuel.IsFuelItem(__instance.m_GearItem))
            {
                if (Buttons.IsSelected(__instance.m_Button_Unload))
                {
                    Fuel.Drain(__instance.m_GearItem, __instance);
                }
                else
                {
                    Fuel.Refuel(__instance.m_GearItem, true, __instance);
                }
                return false;
            }
            else return true;
        }
    }
}
