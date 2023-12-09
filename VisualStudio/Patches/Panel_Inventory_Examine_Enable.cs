namespace FuelManager
{
    [HarmonyPatch(typeof(Panel_Inventory_Examine), nameof(Panel_Inventory_Examine.Enable), new Type[] { typeof(bool), typeof(ComingFromScreenCategory) })]
    internal class Panel_Inventory_Examine_Enable
    {
        private static void Prefix(Panel_Inventory_Examine __instance, bool enable)
        {
            if (!enable) return;

            if (Fuel.IsFuelItem(__instance.m_GearItem))
            {
                // repurpose the left "Unload" button to "Drain"
                Buttons.SetButtonLocalizationKey(__instance.m_Button_Unload, "GAMEPLAY_BFM_Drain");
                Buttons.SetButtonSprite(__instance.m_Button_Unload, "ico_lightSource_lantern");
                // rename the bottom right "Unload" button to "Drain"
                Buttons.SetUnloadButtonLabel(__instance, "GAMEPLAY_BFM_Drain");

                Transform lanternTexture = __instance.m_RefuelPanel.transform.Find("FuelDisplay/Lantern_Texture");
                Buttons.SetTexture(lanternTexture, Il2Cpp.Utils.GetInventoryIconTexture(__instance.m_GearItem));
            }
            else
            {
                Buttons.SetButtonLocalizationKey(__instance.m_Button_Unload, "GAMEPLAY_Unload");
                Buttons.SetButtonSprite(__instance.m_Button_Unload, "ico_ammo_rifle");
                Buttons.SetUnloadButtonLabel(__instance, "GAMEPLAY_Unload");
            }
        }
    }
}
