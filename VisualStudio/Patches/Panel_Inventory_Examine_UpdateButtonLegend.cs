namespace FuelManager
{
    [HarmonyPatch(typeof(Panel_Inventory_Examine), nameof(Panel_Inventory_Examine.UpdateButtonLegend))]
    internal class Panel_Inventory_Examine_UpdateButtonLegend
    {
        private static void Postfix(Panel_Inventory_Examine __instance)
        {
            if (!__instance.IsPanelPatchable()) return;
			if (__instance.m_GearItem == null) return;

            GearItem gi = __instance.m_GearItem;

            try
            {
                if (Fuel.IsFuelItem(gi) && Buttons.IsSelected(__instance.m_Button_Unload))
                {
                    bool active = __instance.m_ActionInProgressWindow.activeInHierarchy || InterfaceManager.GetPanel<Panel_GenericProgressBar>().IsEnabled();

                    __instance.m_ButtonLegendContainer.UpdateButton("Continue", "GAMEPLAY_BFM_Drain", !active, 1, true);
                }
            }
            catch (Exception e)
            {
				Main.Logger.Log($"UpdateButtonLegend failed\n", FlaggedLoggingLevel.Exception, e);
			}
        }
    }
}
