namespace FuelManager
{
    using System;
    using Il2Cpp;
    using HarmonyLib;
    using UnityEngine;
    using Il2CppTLD.Gear;

    internal static class PreventLiquidItemDestruction
    {
        internal static int deductLiquidFromInventoryCallDepth = 0;

        [HarmonyPatch(typeof(PlayerManager), nameof(PlayerManager.DeductLiquidFromInventory), new Type[] { typeof(float), typeof(LiquidType) })]
        internal static class PlayerManager_DeductLiquidFromInventory
        {
            private static void Prefix()
            {
                deductLiquidFromInventoryCallDepth++;
            }
            private static void Postfix()
            {
                deductLiquidFromInventoryCallDepth--;
            }
        }

        [HarmonyPatch(typeof(Inventory), nameof(Inventory.DestroyGear), new Type[] { typeof(GameObject) })]
        internal static class Inventory_DestroyGear
        {
            private static bool Prefix(GameObject go)
            {
                if (deductLiquidFromInventoryCallDepth > 0)
                {
                    LiquidItem liquidItem = go.GetComponent<LiquidItem>();

                    if (liquidItem != null && liquidItem.LiquidType == Main.GetKerosene())
                    {
                        return false;
                    }
                }
                return true;
            }
        }
    }
}
