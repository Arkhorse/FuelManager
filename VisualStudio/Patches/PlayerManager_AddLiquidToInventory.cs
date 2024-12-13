using Il2CppTLD.Gear;
using Il2CppTLD.IntBackedUnit;

namespace FuelManager
{
    [HarmonyPatch(typeof(PlayerManager), nameof(PlayerManager.AddLiquidToInventory), [ typeof(ItemLiquidVolume), typeof(LiquidType) ])]
    internal class PlayerManager_AddLiquidToInventory
    {
        private static void PostFix(PlayerManager __instance, ItemLiquidVolume litersToAdd, LiquidType liquidType, ref ItemLiquidVolume ___result)
        {
            if (__instance == null) return;
            if (liquidType == null) return;

            Main.Logger.Log($"PlayerManager is not null, liquidType: {liquidType.ToString()}, litersToAdd: {litersToAdd.m_Units}L", FlaggedLoggingLevel.Debug);

            if (!(liquidType == Main.GetKerosene())) return;

            if (___result != litersToAdd)
            {
                Message.SendLostMessageDelayed(litersToAdd - ___result);

				// just pretend we added everything, so the original method will not generate new containers
				___result = litersToAdd;
            }
        }
    }
}
