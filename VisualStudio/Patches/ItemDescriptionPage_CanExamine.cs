namespace FuelManager
{
	[HarmonyPatch(typeof(ItemDescriptionPage), nameof(ItemDescriptionPage.CanExamine), [typeof(GearItem)])]
	internal class ItemDescriptionPage_CanExamine
	{
		private static bool Prefix(ref GearItem gi, ref bool __result)
		{
			if (gi == null) return true;
			if (Fuel.IsFuelItem(gi))
			{
				__result = true;
				return false;
			}
			return true;
		}
	}
}
