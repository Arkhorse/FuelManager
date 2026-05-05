//using Il2CppTLD.Gear;

//namespace FuelManager.Patches
//{
//	[HarmonyPatch(typeof(LiquidItem), nameof(LiquidItem.AddLiquid), argumentTypes: [typeof(LiquidType), typeof(ItemLiquidVolume), typeof(float)])]
//	public class LiquidItem_AddLiquid
//	{
//		public static void Postfix(ref LiquidItem __instance, ref LiquidType type, ref ItemLiquidVolume volumeLitres, ref float _)
//		{
//			if (__instance != null)
//			{
//				if (type != Main.GetKerosene()) return;
//				GearItem gi = __instance.gameObject.GetComponent<GearItem>();

//				if (gi != null)
//				{
//					string GearName = gi.DisplayName;
//					if (Main.CheckGearNameValid(gi))
//					{
//						StringBuilder sb = new();

//						sb.AppendLine($"GearItem:\t{GearName}");
//						sb.AppendLine($"LiquidType:\t{Localization.Get(type.m_LiquidName.m_LocalizationID)}");
//						sb.AppendLine($"ItemLiquidVolume:\t{volumeLitres.m_Units}L"); // this is a long
//						sb.AppendLine($"Unknown float _:\t{_}"); // unknown

//						Main.Logger.Log(sb.ToString(), FlaggedLoggingLevel.Debug);
//					}
//				}
//			}
//		}
//	}
//	[HarmonyPatch(typeof(LiquidItem), nameof(LiquidItem.RemoveLiquid))]
//	public class LiquidItem_RemoveLiquid
//	{
//		public static void Postfix(ref LiquidItem __instance, ref ItemLiquidVolume volumeLitres)
//		{
//			try
//			{
//				if (__instance != null)
//				{
//					if (__instance != Main.GetKerosene()) return;
//					GearItem gi = __instance.gameObject.GetComponent<GearItem>();
//					if (gi != null)
//					{
//						bool IsEmpty = Fuel.IsEmpty(gi);

//						string GearName = gi.DisplayName;
//						if (Main.CheckGearNameValid(gi))
//						{
//							StringBuilder sb = new();

//							sb.AppendLine($"GearItem:\t{GearName}");
//							sb.AppendLine($"LiquidType:\t{Localization.Get(__instance.LiquidType.m_LiquidName.m_LocalizationID)}");
//							sb.AppendLine($"Will Be Destroyed?:\t{IsEmpty}");
//							sb.AppendLine($"ItemLiquidVolume:\t{volumeLitres.m_Units}L");

//							Main.Logger.Log(sb.ToString(), FlaggedLoggingLevel.Debug);
//						}
//					}
//				}
//			}
//			catch (NullReferenceException)
//			{
//				Main.Logger.Log($"Null exception occured for above item", FlaggedLoggingLevel.Debug);
//				// do nothing
//				return;
//			}
//			catch (Exception e)
//			{
//				Main.Logger.Log($"Attempting to deal with the removal of liquid failed", FlaggedLoggingLevel.Exception, e);
//			}
//		}
//	}
//}
