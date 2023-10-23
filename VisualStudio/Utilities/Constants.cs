namespace FuelManager
{
    internal class Constants
    {
        internal static string[] FuelContainerWhiteList { get; } = { "GEAR_GasCan", "GEAR_GasCanFull", "GEAR_JerrycanRusty", "GEAR_LampFuel", "GEAR_LampFuelFull" };
        internal static string[] REPAIR_HARVEST_GEAR { get; } = { "GEAR_ScrapMetal" };
        internal static string[] HARVEST_TOOLS { get; } = { "GEAR_Hacksaw" };
        internal static string[] REPAIR_TOOLS { get; } = { "GEAR_SimpleTools", "GEAR_HighQualityTools" };
    }
}
