namespace FuelManager.Patches
{
	[HarmonyPatch(typeof(Panel_Harvest), nameof(Panel_Harvest.OnHarvest))]
	internal class Panel_Harvest_OnHarvest
	{
		public static void Postfix(ref Panel_Harvest __instance)
		{
			GearItem gi = __instance.GetSelectedHarvestItem();
			if (gi != null)
			{
				if (Fuel.IsFuelItem(gi))
				{
					string name = CommonUtilities.NormalizeName(gi.name);
					ItemLiquidVolume liquid = Fuel.GetIndividualCurrentLiters(gi);

					if (!Constants.Empty(liquid))
					{
						// tell the player that they lost some kerosene
						Message.SendLostMessageDelayed(liquid);
					}

					StringBuilder sb = new($"Panel_Harvest.OnHarvest::{name}");
					sb.AppendLine($"\tFuel Remaining? {!Constants.Empty(liquid)}");
					sb.AppendLine($"\tAmount remaining: {(float)liquid.m_Units}");

					Main.Logger.Log(sb.ToString(), FlaggedLoggingLevel.Debug);
				}
			}
		}
	}
}
