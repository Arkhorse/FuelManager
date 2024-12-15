namespace FuelManager
{
	[HarmonyPatch(typeof(Panel_Inventory_Examine), nameof(Panel_Inventory_Examine.Enable), [typeof(bool), typeof(ComingFromScreenCategory)])]
	internal class Panel_Inventory_Examine_Enable
	{
		private static void Prefix(Panel_Inventory_Examine __instance, bool enable)
		{
			if (!__instance.IsPanelPatchable()) return;
			if (__instance.m_GearItem == null) return;
			if (!enable) return;

			GearItem gi = __instance.m_GearItem;
			bool fuel = false;

			try
			{
				fuel = Fuel.IsFuelItem(gi);
			}
			catch (Exception e)
			{
				Main.Logger.Log($"Enable Attempting to get IsFuelItem failed\n", FlaggedLoggingLevel.Exception, e);
			}

			if (fuel)
			{
				try
				{
					// repurpose the left "Unload" button to "Drain"
					Buttons.SetButtonLocalizationKey(__instance.m_Button_Unload, "GAMEPLAY_BFM_Drain");
					Buttons.SetButtonSprite(__instance.m_Button_Unload, "ico_lightSource_lantern");
					// rename the bottom right "Unload" button to "Drain"
					Buttons.SetUnloadButtonLabel(__instance, "GAMEPLAY_BFM_Drain");

					Transform lanternTexture = __instance.m_RefuelPanel.transform.Find("FuelDisplay/Lantern_Texture");
					Buttons.SetTexture(lanternTexture, Il2Cpp.Utils.GetInventoryIconTexture(__instance.m_GearItem));
					Main.Logger.Log($"{__instance.m_GearItem.name}: IsFuelItem", FlaggedLoggingLevel.Debug);
				}
				catch (Exception e)
				{
					Main.Logger.Log($"Attempting set buttons for item {gi.name} failed\n", FlaggedLoggingLevel.Exception, e);
				}
			}
			else
			{
				try
				{
					Buttons.SetButtonLocalizationKey(__instance.m_Button_Unload, "GAMEPLAY_Unload");
					Buttons.SetButtonSprite(__instance.m_Button_Unload, "ico_ammo_rifle");
					Buttons.SetUnloadButtonLabel(__instance, "GAMEPLAY_Unload");
				}
				catch (Exception e)
				{
					Main.Logger.Log($"Attempting set buttons back for item {gi.name} failed\n", FlaggedLoggingLevel.Exception, e);
				}
			}
		}
	}
}
