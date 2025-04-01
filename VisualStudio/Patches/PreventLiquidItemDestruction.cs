using Il2CppTLD.Gear;
using Il2CppTLD.IntBackedUnit;

namespace FuelManager
{
	public static class PreventLiquidItemDestruction
	{
		internal static int deductLiquidFromInventoryCallDepth = 0;

		[HarmonyPatch(typeof(PlayerManager), nameof(PlayerManager.DeductLiquidFromInventory), [typeof(ItemLiquidVolume), typeof(LiquidType)])]
		public static class PlayerManager_DeductLiquidFromInventory
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

		[HarmonyPatch(typeof(Inventory), nameof(Inventory.DestroyGear), [typeof(GameObject)])]
		public static class Inventory_DestroyGear
		{
			// this prevents the patch from occuring if the var is not greater than 0
			//public static bool Prepare()
			//{
			//	return deductLiquidFromInventoryCallDepth > 0;
			//}

			public static bool Prefix(Inventory __instance, ref GameObject go)
			{
				if (__instance == null) return true;
				if (go == null) return true;
				if (string.IsNullOrWhiteSpace(go.name)) return true;

				Main.Logger.Log($"Inventory.DestroyGear(GameObject go): Name: {go.name}", FlaggedLoggingLevel.Debug);
				LiquidItem liquidItem = go.GetComponent<LiquidItem>();

				if (liquidItem != null && liquidItem.LiquidType == Main.GetKerosene())
				{
					return false;
				}

				return true;
			}
		}

		[HarmonyPatch(typeof(Inventory), nameof(Inventory.DestroyGear), [typeof(GearItem)])]
		public class Inventory_DestroyGear_GearItem
		{
			public static void Prefix(Inventory __instance, ref GearItem gi)
			{
				if (__instance == null) return;
				if (gi == null) return;
				if (string.IsNullOrWhiteSpace(gi.name)) return;

				Main.Logger.Log($"Inventory.DestroyGear(GearItem gi): Name: {gi.name}", FlaggedLoggingLevel.Debug);
			}
		}
	}
}
