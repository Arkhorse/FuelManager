namespace FuelManager
{
    [HarmonyPatch(typeof(Panel_Inventory_Examine), nameof(Panel_Inventory_Examine.OnUnload))]
    internal class Panel_Inventory_Examine_OnUnload
    {
        private static bool Prefix(Panel_Inventory_Examine __instance)
        {
            if (__instance != null && FuelUtils.IsFuelItem(__instance.m_GearItem))
            {
                FuelUtils.Drain(__instance.m_GearItem, __instance);
            }
            return true;
        }
    }
}
