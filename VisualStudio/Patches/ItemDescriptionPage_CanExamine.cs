namespace FuelManager
{
	[HarmonyPatch(typeof(ItemDescriptionPage), nameof(ItemDescriptionPage.CanExamine), [typeof(GearItem)])]
	internal class ItemDescriptionPage_CanExamine
	{
		private static bool Prefix(ref GearItem gi, ref bool __result)
		{
			if (gi == null) return true;

			Main.Logger.Log($"ItemDescriptionPage.CanExamine:: Result before change is: {__result}, GearItem: {gi.name}", FlaggedLoggingLevel.Debug);

			if (Fuel.IsFuelItem(gi))
			{
				__result = true;
				return false;
			}
			return true;
		}
	}
}
