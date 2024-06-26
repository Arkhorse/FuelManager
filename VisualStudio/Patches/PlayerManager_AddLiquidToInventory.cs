using Il2CppTLD.Gear;
using Il2CppTLD.IntBackedUnit;

namespace FuelManager
{
    [HarmonyPatch(typeof(PlayerManager), nameof(PlayerManager.AddLiquidToInventory), new Type[] { typeof(ItemLiquidVolume), typeof(LiquidType) })]
    internal class PlayerManager_AddLiquidToInventory
    {
        private static void PostFix(PlayerManager __instance, ItemLiquidVolume litersToAdd, LiquidType liquidType, ref ItemLiquidVolume __result)
        {
            if (__instance != null && liquidType == Main.GetKerosene() && __result != litersToAdd)
            {
                Message.SendLostMessageDelayed(litersToAdd - __result);

                // just pretend we added everything, so the original method will not generate new containers
                __result = litersToAdd;
            }
        }
    }
}
