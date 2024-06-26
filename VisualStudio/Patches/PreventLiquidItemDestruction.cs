namespace FuelManager
{
	using System;
	using Il2Cpp;
	using HarmonyLib;
	using UnityEngine;
	using Il2CppTLD.Gear;
	using Il2CppTLD.IntBackedUnit;

	public static class PreventLiquidItemDestruction
	{
		internal static int deductLiquidFromInventoryCallDepth = 0;

		[HarmonyPatch(typeof(PlayerManager), nameof(PlayerManager.DeductLiquidFromInventory), new Type[] { typeof(ItemLiquidVolume), typeof(LiquidType) })]
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

		[HarmonyPatch(typeof(Inventory), nameof(Inventory.DestroyGear), new Type[] { typeof(GameObject) })]
		public static class Inventory_DestroyGear
		{
			// this prevents the patch from occuring if the var is not greater than 0
			public static bool Prepare()
			{
				return deductLiquidFromInventoryCallDepth > 0;
			}

			public static bool Prefix(GameObject go)
			{
				LiquidItem liquidItem = go.GetComponent<LiquidItem>();

				if (liquidItem != null && liquidItem.LiquidType == Main.GetKerosene())
				{
					return false;
				}

				return true;
			}
		}
	}
}
