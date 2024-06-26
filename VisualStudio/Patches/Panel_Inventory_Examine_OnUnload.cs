﻿namespace FuelManager
{
    [HarmonyPatch(typeof(Panel_Inventory_Examine), nameof(Panel_Inventory_Examine.OnUnload))]
    internal class Panel_Inventory_Examine_OnUnload
    {
        private static bool Prefix(Panel_Inventory_Examine __instance)
        {
            if (__instance != null && Fuel.IsFuelItem(__instance.m_GearItem))
            {
                Fuel.Drain(__instance.m_GearItem, __instance);
            }
            return true;
        }
    }
}
