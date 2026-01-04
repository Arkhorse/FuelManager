namespace FuelManager
{
	/* PURPOSE: This patch is to affect changes when the examine action window is refreshed. This adds the drain button for now. Examine Actions API will be used soonish to replace this to decrease breakable stuff
	
	*/
	[HarmonyPatch(typeof(Panel_Inventory_Examine), nameof(Panel_Inventory_Examine.RefreshMainWindow))]
	internal class Panel_Inventory_Examine_RefreshMainWindow
	{
		private static void Postfix(ref Panel_Inventory_Examine __instance)
		{
			string reason = string.Empty;
			if (!__instance.IsPanelPatchable(out reason))
			{
				Main.Logger.Log($"Panel is not currently patchable. Reason: {reason}", FlaggedLoggingLevel.Trace);
				return;
			}
			if (__instance.m_GearItem == null)
			{
				Main.Logger.Log($"The panel's gearitem is null", FlaggedLoggingLevel.Trace);
				return;
			}

			Main.Logger.Log($"Panel is patchable, gearitem is not null", FlaggedLoggingLevel.Debug);
			
			GearItem gi = __instance.m_GearItem;

			// I want all of these to be logged if they null, then exit the method if they are
			StringBuilder NullRecord = new();
			if (string.IsNullOrWhiteSpace(gi.name)) NullRecord.AppendLine("Attempting to work on a gear item without a name will not work");
			if (__instance.m_Button_Harvest == null) NullRecord.AppendLine("__instance.m_Button_Harvest is null");
			if (__instance.m_Button_Repair == null) NullRecord.AppendLine("__instance.m_Button_Repair is null");
			if (__instance.m_Button_SafehouseCustomizationRepair == null) NullRecord.AppendLine("__instance.m_Button_SafehouseCustomizationRepair is null");
			if (__instance.m_Button_Refuel == null) NullRecord.AppendLine("__instance.m_Button_Refuel is null");
			if (__instance.m_Button_Unload == null) NullRecord.AppendLine("__instance.m_Button_Unload is null");
			if (__instance.m_Button_Clean == null) NullRecord.AppendLine("__instance.m_Button_Clean is null");
			if (__instance.m_Button_Sharpen == null) NullRecord.AppendLine("__instance.m_Button_Sharpen is null");

			if (NullRecord.Length > 0)
			{
				Main.Logger.Log(NullRecord.ToString(), FlaggedLoggingLevel.Warning);
				NullRecord.Clear();
				return;
			}
			Main.Logger.Log($"Panel is patchable, gearitem is not null and all buttons are not null", FlaggedLoggingLevel.Verbose);

			try
			{
				if (Fuel.IsFuelItem(gi))
				{
					Main.Logger.Log($"Panel_Inventory_Examine_RefreshMainWindow: {gi.name}", FlaggedLoggingLevel.Debug);

#pragma warning disable CS8600 // Converting null literal or possible null value to non-nullable type.
					UIButton ButtonUnload = __instance.m_Button_Unload;
#pragma warning restore CS8600 // Converting null literal or possible null value to non-nullable type.
					if (ButtonUnload == null)
					{
						Main.Logger.Log("__instance.m_Button_Unload is null", FlaggedLoggingLevel.Debug);
						return;
					}
					Panel_Inventory_Examine_MenuItem UnloadButton = ButtonUnload.GetComponent<Panel_Inventory_Examine_MenuItem>();
					if (UnloadButton == null)
					{
						Main.Logger.Log($"__instance.m_Button_Unload.GetComponent<Panel_Inventory_Examine_MenuItem>() is currently null, unable to patch", FlaggedLoggingLevel.Debug, memberName: nameof(Panel_Inventory_Examine.RefreshMainWindow));
						return;
					}

					// maintain all possible buttons, maintain order used in the panel, verify order in Unity Explorer
					/* VERIFICATION
						Panel Code: true
						UE: false
					*/
					// obviously this shouldnt including the unload button

#pragma warning disable CS8604 // Possible null reference argument.
					Vector3 position = Buttons.GetBottomPosition(
						__instance.m_ButtonSpacing,
						__instance.m_Button_Harvest,
						__instance.m_Button_Repair,
						__instance.m_Button_SafehouseCustomizationRepair,
						__instance.m_Button_Refuel,
						__instance.m_Button_Clean,
						__instance.m_Button_Sharpen);
#pragma warning restore CS8604 // Possible null reference argument.

					UnloadButton.transform.localPosition = position;
					UnloadButton.gameObject.SetActive(true);

					UnloadButton.SetDisabled(Constants.Empty(Fuel.GetLitersToDrain(gi)));
					Main.Logger.Log($"DEBUG DATA:\n\tPosition: {position}\n\tDrained: {Fuel.GetLitersToDrain(gi)}\n\tSetDisabled: {Constants.Empty(Fuel.GetLitersToDrain(gi))}", FlaggedLoggingLevel.Debug);
				}
			}
			catch (Exception e)
			{
				Main.Logger.Log($"RefreshMainWindow patch failed\n", FlaggedLoggingLevel.Exception, e);
			}
		}
	}
}
