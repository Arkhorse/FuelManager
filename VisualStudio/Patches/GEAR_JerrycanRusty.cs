namespace FuelManager
{
	//[HarmonyPatch(typeof(GearItem), nameof(GearItem.Deserialize), new Type[] { typeof(GearItemSaveDataProxy) })]
	[HarmonyPatch(typeof(GearItem), nameof(GearItem.Awake))]
	public static class AddComponents
	{
		public static void Prefix(ref GearItem __instance)
		{
			if (__instance == null) return;
			//if (__instance.gameObject.scene.name == null) return; // prefab
			if (string.IsNullOrWhiteSpace(__instance.name)) return;

			string name = string.Empty;

			try
			{
				name = CommonUtilities.NormalizeName(__instance.name);
			}
			catch (Exception e)
			{
				Main.Logger.Log($"Attempting to set the name and normalize it failed\n", FlaggedLoggingLevel.Exception, e);
				return;
			}
			if (name == "GEAR_LampFuel")
			{
				Main.Logger.Log("Begin patch for GEAR_LampFuel", FlaggedLoggingLevel.Debug, LoggingSubType.IntraSeparator);

				FuelItemAPI.ApplyAllComponents(ref __instance, 1, 1, 1, 1);
			}
			else if (name == "GEAR_LampFuelFull")
			{
				Main.Logger.Log("Begin patch for GEAR_LampFuelFull", FlaggedLoggingLevel.Debug, LoggingSubType.IntraSeparator);

				FuelItemAPI.ApplyAllComponents(ref __instance, 1, 1, 1, 1);
			}
			else if (name == "GEAR_JerrycanRusty")
			{
				Main.Logger.Log($"Patching \'GEAR_JerrycanRusty\'", FlaggedLoggingLevel.Debug);

				FuelItemAPI.ApplyAllComponents(ref __instance, 2, 2, 1, 2);
			}
		}
	}
}
