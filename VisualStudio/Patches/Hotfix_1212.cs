//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using Il2CppTLD.IntBackedUnit;
//namespace FuelManager.Patches
//{
//	[HarmonyPatch(typeof(GearItem), nameof(GearItem.Awake))]
//	internal class Hotfix_1212
//	{
//		[HarmonyPostfix]
//		public static void Apply(GearItem __instance)
//		{
//			if (__instance == null) return;
//			if (!string.IsNullOrWhiteSpace(__instance.name) && __instance.name.Contains("gascan".ToLowerInvariant()))
//			{
//				Main.Logger.Log("Hotfix 1.2.12: Temp fix to fix incorrect LiquidItem values from MC", FlaggedLoggingLevel.Debug);
//				LiquidItem liquidItem = __instance.GetComponent<LiquidItem>();
//				if (liquidItem != null)
//				{
//					liquidItem.m_Minimum = ItemLiquidVolume.Zero;
//					liquidItem.m_Maximum = ItemLiquidVolume.FromLiters(6);
//				}
//			}
//		}
//	}
//}
