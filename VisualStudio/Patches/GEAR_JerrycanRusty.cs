namespace FuelManager
{
    //[HarmonyPatch(typeof(GearItem), nameof(GearItem.Deserialize), new Type[] { typeof(GearItemSaveDataProxy) })]
    [HarmonyPatch(typeof(GearItem), nameof(GearItem.Awake))]
    public static class AddComponents_JerrycanRusty
    {
        // "GEAR_JerrycanRusty"
        public static void Prefix(GearItem __instance)
        {
            if (__instance == null) return;

            if (__instance.name != null && CommonUtilities.NormalizeName(__instance.name) == "GEAR_JerrycanRusty")
            {
                //FuelItemAPI.AddRepair(__instance, Constants.REPAIR_HARVEST_GEAR, new int[] { 1 }, Constants.REPAIR_TOOLS, "Play_RepairingMetal");
                FuelItemAPI.AddHarvest(__instance, Constants.REPAIR_HARVEST_GEAR, new int[] { 2 }, Constants.HARVEST_TOOLS, "Play_HarvestingMetalSaw");
                FuelItemAPI.AddMillable(__instance, 30, Constants.REPAIR_HARVEST_GEAR, [1], Constants.REPAIR_HARVEST_GEAR, [2], true, 30, 60, SkillType.ToolRepair);
            }
        }
    }
}
