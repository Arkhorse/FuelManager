using Il2CppTLD.IntBackedUnit;

namespace FuelManager
{
	[HarmonyPatch(typeof(Panel_Inventory_Examine), nameof(Panel_Inventory_Examine.RefreshMainWindow))]
	internal class Panel_Inventory_Examine_RefreshMainWindow
	{
		private static void Postfix(Panel_Inventory_Examine __instance)
		{
			if (!__instance.IsPanelPatchable()) return;
			if (__instance.m_GearItem == null) return;

			Main.Logger.Log($"Panel is patchable, gearitem is not null", FlaggedLoggingLevel.Debug);
			
			GearItem gi = __instance.m_GearItem;

			// I want all of these to be logged if they null, then exit the method if they are
			StringBuilder NullRecord = new();
			if (string.IsNullOrWhiteSpace(gi.name)) NullRecord.AppendLine("Attempting to work on a gear item without a name will not work");
			if (__instance.m_Button_Harvest == null) NullRecord.AppendLine("__instance.m_Button_Harvest is null");
			if (__instance.m_Button_Refuel == null) NullRecord.AppendLine("__instance.m_Button_Refuel is null");
			if (__instance.m_Button_Repair == null) NullRecord.AppendLine("__instance.m_Button_Repair is null");

			if (string.IsNullOrWhiteSpace(gi.name) || __instance.m_Button_Harvest == null || __instance.m_Button_Refuel == null || __instance.m_Button_Repair == null)
			{
				Main.Logger.Log(NullRecord.ToString(), FlaggedLoggingLevel.Warning);
				NullRecord.Clear();
				return;
			}

			try
			{
				if (Fuel.IsFuelItem(gi))
				{
					Main.Logger.Log($"Panel_Inventory_Examine_RefreshMainWindow: {gi.name}", FlaggedLoggingLevel.Debug);
					// maintain all possible buttons, maintain order used in the panel, verify order in Unity Explorer
					/* VERIFICATION
						Panel Code: true
						UE: false
					*/
					Vector3 position = Buttons.GetBottomPosition(
													__instance.m_Button_Harvest,
													__instance.m_Button_Repair,
													__instance.m_Button_SafehouseCustomizationRepair,
													__instance.m_Button_Refuel,
													__instance.m_Button_Unload,
													__instance.m_Button_Clean,
													__instance.m_Button_Sharpen
													);
					position.y += __instance.m_ButtonSpacing;
					__instance.m_Button_Unload.transform.localPosition = position;
					__instance.m_Button_Unload.gameObject.SetActive(true);

					ItemLiquidVolume litersToDrain = Fuel.GetLitersToDrain(gi);

					bool thing = litersToDrain < ItemLiquidVolume.Zero; // must be <, not <=

					__instance.m_Button_Unload.GetComponent<Panel_Inventory_Examine_MenuItem>().SetDisabled(thing);
					Main.Logger.Log($"DEBUG DATA:\n\tPosition: {position}\n\tDrained: {litersToDrain}\n\tSetDisabled: {thing}", FlaggedLoggingLevel.Debug);
				}
			}
			catch (Exception e)
			{
				Main.Logger.Log($"RefreshMainWindow failed\n", FlaggedLoggingLevel.Exception, e);
			}
		}
	}
}
