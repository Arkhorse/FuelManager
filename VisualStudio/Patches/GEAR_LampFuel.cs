
//using System.Text;

//namespace FuelManager
//{
//	//[HarmonyPatch(typeof(GearItem), nameof(GearItem.Deserialize), new Type[] { typeof(GearItemSaveDataProxy) })]
//	[HarmonyPatch(typeof(GearItem), nameof(GearItem.Awake))]
//	public static class AddComponents_LampFuel
//	{
//		// "GEAR_LampFuel"
//		public static void Postfix(ref GearItem __instance)
//		{
//			if (__instance == null) return;
//			if (string.IsNullOrWhiteSpace(__instance.name)) return;
//			if (CommonUtilities.NormalizeName(__instance.name) == "GEAR_LampFuel")
//			{
//				Main.Logger.Log("Begin patch for GEAR_LampFuel", FlaggedLoggingLevel.Debug, LoggingSubType.IntraSeparator);
//				StringBuilder sb = new StringBuilder();

//				//FuelItemAPI.AddRepair(__instance, Constants.REPAIR_HARVEST_GEAR, new int[] { 1 }, Constants.REPAIR_TOOLS, "Play_RepairingMetal");

//				bool harvest = __instance.gameObject.GetComponent<Harvest>() == null;

//				sb.AppendLine($"Before attempting to add harvest component, harvest is null: {harvest}");

//				try
//				{
//					FuelItemAPI.AddHarvest(ref __instance, Constants.REPAIR_HARVEST_GEAR, [2], Constants.HARVEST_TOOLS, "Play_HarvestingMetalSaw");
//					sb.AppendLine($"Was able to add harvest");
//				}
//				catch (Exception e)
//				{
//					harvest = __instance.gameObject.GetComponent<Harvest>() == null;

//					sb.AppendLine($"Was not able to add harvest");
//					sb.AppendLine(e.Message);
//				}
//				sb.AppendLine($"Finally, the harvest component is null: {harvest}");
//				Main.Logger.Log(sb.ToString(), FlaggedLoggingLevel.Debug);
//			}
//		}
//	}
//}
