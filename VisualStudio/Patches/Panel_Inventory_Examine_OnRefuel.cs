namespace FuelManager
{
    [HarmonyPatch(typeof(Panel_Inventory_Examine), nameof(Panel_Inventory_Examine.OnRefuel))]
    internal class Panel_Inventory_Examine_OnRefuel
    {
        private static bool Prefix(Panel_Inventory_Examine __instance)
        {
            if (__instance != null && FuelUtils.IsFuelItem(__instance.m_GearItem))
            {
                if (ButtonUtils.IsSelected(__instance.m_Button_Unload))
                {
                    FuelUtils.Drain(__instance.m_GearItem, __instance);
                }
                else
                {
                    FuelUtils.Refuel(__instance.m_GearItem, true, __instance);
                }
                return false;
            }
            else return true;
        }
    }
}
