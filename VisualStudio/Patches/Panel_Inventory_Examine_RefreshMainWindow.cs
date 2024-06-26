using Il2CppTLD.IntBackedUnit;

namespace FuelManager
{
    [HarmonyPatch(typeof(Panel_Inventory_Examine), nameof(Panel_Inventory_Examine.RefreshMainWindow))]
    internal class Panel_Inventory_Examine_RefreshMainWindow
    {
        private static void Postfix(Panel_Inventory_Examine __instance)
        {
            if (Fuel.IsFuelItem(__instance.m_GearItem.GetComponent<GearItem>()))
            {
                Vector3 position = Buttons.GetBottomPosition(
                                                __instance.m_Button_Harvest,
                                                __instance.m_Button_Refuel,
                                                __instance.m_Button_Repair
                                                );
                position.y += __instance.m_ButtonSpacing;
                __instance.m_Button_Unload.transform.localPosition = position;

                __instance.m_Button_Unload.gameObject.SetActive(true);

				ItemLiquidVolume litersToDrain = Fuel.GetLitersToDrain(__instance.m_GearItem);
                __instance.m_Button_Unload.GetComponent<Panel_Inventory_Examine_MenuItem>().SetDisabled(litersToDrain < Fuel.MIN_LITERS);
            }
        }
    }
}
