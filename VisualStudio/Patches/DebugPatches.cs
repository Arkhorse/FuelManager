namespace FuelManager.Patches
{
	[HarmonyPatch(typeof(LiquidItem), nameof(LiquidItem.AddLiquid), argumentTypes: [typeof(LiquidType), typeof(ItemLiquidVolume), typeof(float)])]
	public class LiquidItem_AddLiquid
	{
		public static void Prefix(ref LiquidItem __instance, ref LiquidType type, ref ItemLiquidVolume volumeLitres, ref float _)
		{
			if (__instance != null)
			{
				GearItem gi = __instance.gameObject.GetComponent<GearItem>();

				if (gi != null)
				{
					string GearName = gi.DisplayName;
					StringBuilder sb = new();

					sb.AppendLine($"GearItem:\t{GearName}");
					sb.AppendLine($"LiquidType:\t{Localization.Get(type.m_LiquidName.m_LocalizationID)}");
					sb.AppendLine($"ItemLiquidVolume:\t{volumeLitres.m_Units}L"); // this is a long
					sb.AppendLine($"Unknown float _:\t{_}"); // unknown

					Main.Logger.Log(sb.ToString(), FlaggedLoggingLevel.Debug);
				}
			}
		}
	}
	[HarmonyPatch(typeof(LiquidItem), nameof(LiquidItem.RemoveLiquid), argumentTypes: [typeof(ItemLiquidVolume), typeof(float)], argumentVariations: [ArgumentType.Normal, ArgumentType.Out])]
	public class LiquidItem_RemoveLiquid
	{
		public static void Prefix(ref LiquidItem __instance, ref ItemLiquidVolume volumeLitres)
		{
			if (__instance != null)
			{
				GearItem gi = __instance.gameObject.GetComponent<GearItem>();
				if (gi != null)
				{
					bool IsEmpty = Fuel.IsEmpty(gi);

					string GearName = gi.DisplayName;
					StringBuilder sb = new();

					sb.AppendLine($"GearItem:\t{GearName}");
					sb.AppendLine($"LiquidType:\t{Localization.Get(__instance.LiquidType.m_LiquidName.m_LocalizationID)}");
					sb.AppendLine($"Will Be Destroyed?:\t{IsEmpty}");
					sb.AppendLine($"ItemLiquidVolume:\t{volumeLitres.m_Units}L");

					Main.Logger.Log(sb.ToString(), FlaggedLoggingLevel.Debug);
				}
			}
		}
	}
}
