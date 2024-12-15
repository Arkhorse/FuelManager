//namespace FuelManager
//{
//	//[HarmonyPatch(typeof(GearItem), nameof(GearItem.Deserialize), new Type[] { typeof(GearItemSaveDataProxy) })]
//	[HarmonyPatch(typeof(GearItem), nameof(GearItem.Awake))]
//	public static class AddComponents_JerrycanRusty
//	{
//		// "GEAR_JerrycanRusty"
//		public static void Prefix(ref GearItem __instance)
//		{
//			if (__instance == null) return;
//			if (__instance.gameObject.scene.name == null) return; // prefab
//			if (string.IsNullOrWhiteSpace(__instance.name)) return;

//			string name = string.Empty;

//			try
//			{
//				name = CommonUtilities.NormalizeName(__instance.name);
//			}
//			catch (Exception e)
//			{
//				Main.Logger.Log($"Attempting to set the name and normalize it failed\n", FlaggedLoggingLevel.Exception, e);
//				return;
//			}

//			System.Text.StringBuilder sb = new();
//			bool harvest = __instance.gameObject.GetComponent<Harvest>() == null;

//			if (name == "GEAR_LampFuel")
//			{
//				Main.Logger.Log("Begin patch for GEAR_LampFuel", FlaggedLoggingLevel.Debug, LoggingSubType.IntraSeparator);

//				//FuelItemAPI.AddRepair(__instance, Constants.REPAIR_HARVEST_GEAR, new int[] { 1 }, Constants.REPAIR_TOOLS, "Play_RepairingMetal");

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
//			else if (name == "GEAR_LampFuelFull")
//			{
//				//FuelItemAPI.AddRepair(__instance, Constants.REPAIR_HARVEST_GEAR, new int[] { 1 }, Constants.REPAIR_TOOLS, "Play_RepairingMetal");
//				Main.Logger.Log("Begin patch for GEAR_LampFuelFull", FlaggedLoggingLevel.Debug, LoggingSubType.IntraSeparator);

//				//FuelItemAPI.AddRepair(__instance, Constants.REPAIR_HARVEST_GEAR, new int[] { 1 }, Constants.REPAIR_TOOLS, "Play_RepairingMetal");

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
//			else if (name == "GEAR_JerrycanRusty")
//			{
//				Main.Logger.Log($"Patching \'GEAR_JerrycanRusty\'", FlaggedLoggingLevel.Debug);

//				//FuelItemAPI.AddRepair(__instance, Constants.REPAIR_HARVEST_GEAR, new int[] { 1 }, Constants.REPAIR_TOOLS, "Play_RepairingMetal");
//				//FuelItemAPI.AddHarvest(ref __instance, Constants.REPAIR_HARVEST_GEAR, [2], Constants.HARVEST_TOOLS, "Play_HarvestingMetalSaw");
//				//FuelItemAPI.AddRepair(__instance, Constants.REPAIR_HARVEST_GEAR, new int[] { 1 }, Constants.REPAIR_TOOLS, "Play_RepairingMetal");

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

//				FuelItemAPI.AddMillable(ref __instance, Constants.REPAIR_HARVEST_GEAR, [1], Constants.REPAIR_HARVEST_GEAR, [2], true, 30, 60, SkillType.ToolRepair);
//			}
//		}
//	}
//}
