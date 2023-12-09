namespace FuelManager
{
    [HarmonyPatch(typeof(Panel_Inventory_Examine), nameof(Panel_Inventory_Examine.RefreshRefuelPanel))]
    internal class Panel_Inventory_Examine_RefreshRefuelPanel
    {
        private static bool Prefix(Panel_Inventory_Examine __instance)
        {
            if (!Fuel.IsFuelItem(__instance.m_GearItem.GetComponent<GearItem>())) return true;

            __instance.m_RefuelPanel.SetActive(false);
            __instance.m_Button_Refuel.gameObject.SetActive(true);
            //__instance.m_Button_RefuelBackground.SetActive(true); // DONT ENABLE. BREAKS EVERYTHING

            float currentLiters     = Fuel.GetIndividualCurrentLiters(__instance.m_GearItem.GetComponent<GearItem>());
            float capacityLiters    = Fuel.GetIndividualCapacityLiters(__instance.m_GearItem.GetComponent<GearItem>());
            float totalCurrent      = Fuel.GetTotalCurrentLiters(__instance.m_GearItem.GetComponent<GearItem>());
            float totalCapacity     = Fuel.GetTotalCapacityLiters(__instance.m_GearItem.GetComponent<GearItem>());

            bool fuelIsAvailable    = totalCurrent > Fuel.MIN_LITERS;
            bool flag               = fuelIsAvailable && !Il2Cpp.Utils.Approximately(currentLiters, capacityLiters, Fuel.MIN_LITERS);

            __instance.m_Refuel_X.gameObject.SetActive(!flag);
            __instance.m_Button_Refuel.gameObject.GetComponent<Panel_Inventory_Examine_MenuItem>().SetDisabled(!flag);

            try
            {
                if (__instance.m_GearItem != null && !Fuel.IsKeroseneLamp(__instance.m_GearItem)) __instance.m_Button_RefuelBackground.SetActive(true);
            }
            catch (NullReferenceException)
            {

            }

            InterfaceManager.GetPanel<Panel_Inventory_Examine>().m_MouseRefuelButton.SetActive(flag);
            __instance.m_RequiresFuelMessage.SetActive(false);

            __instance.m_LanternFuelAmountLabel.text = $"{Fuel.GetLiquidQuantityStringNoOunces(currentLiters)} / {Fuel.GetLiquidQuantityStringNoOunces(capacityLiters)}";
            __instance.m_FuelSupplyAmountLabel.text = $"{Fuel.GetLiquidQuantityStringNoOunces(totalCurrent)} / {Fuel.GetLiquidQuantityStringNoOunces(totalCapacity)}";

            __instance.UpdateWeightAndConditionLabels();

            return false; // MUST BE FALSE
        }
    }
}
