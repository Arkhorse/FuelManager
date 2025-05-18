namespace FuelManager
{
	public sealed class Constants
	{
#pragma warning disable IDE0300
		public static string[] FuelContainerWhiteList { get; } = { "GEAR_GasCan", "GEAR_GasCanFull", "GEAR_JerrycanRusty", "GEAR_LampFuel", "GEAR_LampFuelFull" };
		public static string[] REPAIR_HARVEST_GEAR { get; } = { "GEAR_ScrapMetal" };
		public static string[] HARVEST_TOOLS { get; } = { "GEAR_Hacksaw" };
		public static string[] REPAIR_TOOLS { get; } = { "GEAR_SimpleTools", "GEAR_HighQualityTools" };
		/// <summary>
		/// This checks using known methods to see if the current liquid container is empty
		/// </summary>
		/// <param name="a">This is the container liquid volume instance</param>
		/// <returns><see langword="true"/> if the give <paramref name="a"/> is less than <see cref="ItemLiquidVolume.Zero"/>, otherwise <see langword="false"/></returns>
		/// <remarks>This must be <c>&lt;</c> as otherwise this thing wont work properly</remarks>
		public static bool Empty(ItemLiquidVolume a) => a < ItemLiquidVolume.Zero; // must be <, not <=

#pragma warning restore IDE0300
	}
}
