using Il2CppTLD.IntBackedUnit;

namespace FuelManager
{
	public class ConsoleCommands
	{
		private static void UpdateAllGearItems()
		{
			Inventory inv = GameManager.GetInventoryComponent();
			for (int i = 0; i < inv.m_Items.Count; i++)
			{
				if (inv.m_Items[i] == null) continue;
				GearItem gi = inv.m_Items[i];

				if (gi == null) continue;
				if (string.IsNullOrWhiteSpace(gi.name)) continue;

				if (CommonUtilities.NormalizeName(gi.name) == "GEAR_JerrycanRusty")
				{
					FuelItemAPI.RefreshRepairComponent(gi);
					FuelItemAPI.RefreshHarvestComponent(gi);
					continue;
				}
				if (CommonUtilities.NormalizeName(gi.name) == "GEAR_LampFuel")
				{
					FuelItemAPI.RefreshRepairComponent(gi);
					FuelItemAPI.RefreshHarvestComponent(gi);
					continue;
				}
				if (CommonUtilities.NormalizeName(gi.name) == "GEAR_LampFuelFull")
				{
					FuelItemAPI.RefreshRepairComponent(gi);
					FuelItemAPI.RefreshHarvestComponent(gi);
					continue;
				}
			}
		}

		public static void TestStrings()
		{
			Main.Logger.Log("Test Strings Called", FlaggedLoggingLevel.Always, LoggingSubType.uConsole);
			for (int i = 0; i < GameManager.GetInventoryComponent().m_Items.Count; i++)
			{
				GearItem gi = GameManager.GetInventoryComponent().m_Items[i];
				if (gi == null) continue;
				if (string.IsNullOrWhiteSpace(gi.name)) continue;
				string name = CommonUtilities.NormalizeName(gi.name);
				if (name == "GEAR_JerrycanRusty")
				{
					ItemLiquidVolume thing = gi.gameObject.GetComponent<LiquidItem>().m_Liquid;

					StringBuilder sb = new();
					sb.AppendLine("Current Value: ");
					sb.Append(Fuel.GetLiquidQuantityString(thing));
					sb.AppendLine("New Value Metric: ");
					sb.Append(thing.ToStringMetric());
					sb.AppendLine("New Value Gallons: ");
					sb.Append(thing.ToStringImperialGallons());
					sb.AppendLine("New Value Ounces: ");
					sb.Append(thing.ToStringImperialOunces());

					Main.Logger.Log("Testing Strings", FlaggedLoggingLevel.Always, LoggingSubType.IntraSeparator);
					Main.Logger.Log(sb.ToString(), FlaggedLoggingLevel.Always);
					Main.Logger.Log(FlaggedLoggingLevel.Always, LoggingSubType.Separator);
				}
			}
		}

		public static void RegisterCommands()
		{
			uConsole.RegisterCommand("FM_PrintChangeLogs", new Action(PatchNotes.PrintChangeLog));
			uConsole.RegisterCommand("UpdateAllGearItems", new Action(UpdateAllGearItems));
			uConsole.RegisterCommand("FM_TESTSTRINGS", new Action(TestStrings));
		}
	}
}