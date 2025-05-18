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
			StringBuilder sb = new();

			//for (int i = 0; i < GameManager.GetInventoryComponent().m_Items.Count; i++)
			//{
			//	GearItem gi = GameManager.GetInventoryComponent().m_Items[i];
			//	if (gi == null) continue;
			//	if (string.IsNullOrWhiteSpace(gi.name)) continue;
			//	string name = CommonUtilities.NormalizeName(gi.name);

			//	if (Fuel.IsFuelItem(gi))
			//	{
			//		sb.AppendLine($"Current Object: {name}");


			//		break; // only want this to happen once
			//	}
			//}

			int result = Fuel.GetUserMetric();

			sb.AppendLine("Get Used Metric Key:\n");
			sb.Append("\t\t1: Metric");
			sb.Append("\t\t2: Imperial");
			sb.Append("\t\t-1: Panel_OptionsMenu is null");
			sb.Append("\t\t-2: DisplayMenuItems is null");
			sb.Append("\t\t-3: Units GameObject is null");
			sb.Append("\t\t-4: ComboBox is null");

			string thing = "";

			switch (result)
			{
				case 1:
					thing = "\t\tX";
					break;
				case 2:
					thing = "\t\t\t\tX";
					break;
				case -1:
					thing = "\t\t\t\t\t\tX";
					break;
				case -2:
					thing = "\t\t\t\t\t\t\t\tX";
					break;
				case -3:
					thing = "\t\t\t\t\t\t\t\t\t\tX";
					break;
				case -4:
					thing = "\t\t\t\t\t\t\t\t\t\t\t\tX";
					break;
			}

			sb.AppendLine(thing);

			Main.Logger.Log("Testing Strings", FlaggedLoggingLevel.Always, LoggingSubType.IntraSeparator);
			Main.Logger.Log(sb.ToString(), FlaggedLoggingLevel.Always);
			Main.Logger.Log(FlaggedLoggingLevel.Always, LoggingSubType.Separator);
		}

		public static void RegisterCommands()
		{
			uConsole.RegisterCommand("FM_PrintChangeLogs", new Action(PatchNotes.PrintChangeLog));
			uConsole.RegisterCommand("FM_DEBUG_UpdateAllGearItems", new Action(UpdateAllGearItems));
			uConsole.RegisterCommand("FM_TESTSTRINGS", new Action(TestStrings));
		}
	}
}