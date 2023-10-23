using Il2CppTLD.Gear;

namespace FuelManager
{
    [HarmonyPatch(typeof(PlayerManager), nameof(PlayerManager.AddLiquidToInventory), new Type[] { typeof(float), typeof(LiquidType) })]
    internal class PlayerManager_AddLiquidToInventory
    {
        private static void PostFix(PlayerManager __instance, float litersToAdd, LiquidType liquidType, ref float __result)
        {
            if (__instance != null && liquidType == Main.GetKerosene() && __result != litersToAdd)
            {
                MessageUtils.SendLostMessageDelayed(litersToAdd - __result);

                // just pretend we added everything, so the original method will not generate new containers
                __result = litersToAdd;
            }
        }
    }
}
