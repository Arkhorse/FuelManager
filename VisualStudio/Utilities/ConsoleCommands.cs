namespace FuelManager
{
    public class ConsoleCommands
    {
        private static void UpdateAllGearItems()
        {
            var inv = GameManager.GetInventoryComponent();
            for (int i = 0; i < inv.m_Items.Count; i++)
            {
                if (inv.m_Items[i] is null) continue;
                GearItem gi = inv.m_Items[i];

                if (gi is null) continue;

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

        public static void RegisterCommands()
        {
            uConsole.RegisterCommand("FM_PrintChangeLogs", new Action(PatchNotes.PrintChangeLog));
            uConsole.RegisterCommand("UpdateAllGearItems", new Action(UpdateAllGearItems));
        }
    }
}