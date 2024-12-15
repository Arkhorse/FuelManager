using Il2CppTLD.Gear;
using Il2CppTLD.IntBackedUnit;

namespace FuelManager
{
	[HarmonyPatch(typeof(PlayerManager), nameof(PlayerManager.AddLiquidToInventory), [ typeof(ItemLiquidVolume), typeof(LiquidType) ])]
	internal class PlayerManager_AddLiquidToInventory
	{
		private static void PostFix(PlayerManager __instance, ItemLiquidVolume litersToAdd, LiquidType liquidType, ref ItemLiquidVolume __result)
		{
			if (__instance == null) return;
			if (liquidType == null) return;

			Main.Logger.Log($"PlayerManager is not null, liquidType: {Localization.Get(liquidType.LiquidName.m_LocalizationID)}, litersToAdd: {litersToAdd.m_Units}L, Result: {__result.m_Units}L", FlaggedLoggingLevel.Debug);

			if (!(liquidType == Main.GetKerosene())) return;

			if (__result != litersToAdd)
			{
				Message.SendLostMessageDelayed(litersToAdd - __result);

				// just pretend we added everything, so the original method will not generate new containers
				__result = litersToAdd;
			}
		}
	}
}
