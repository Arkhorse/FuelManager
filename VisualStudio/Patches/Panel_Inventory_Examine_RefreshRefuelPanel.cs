﻿namespace FuelManager
{
    [HarmonyPatch(typeof(Panel_Inventory_Examine), nameof(Panel_Inventory_Examine.RefreshRefuelPanel))]
    internal class Panel_Inventory_Examine_RefreshRefuelPanel
    {
        private static bool Prefix(Panel_Inventory_Examine __instance)
        {
            if (!FuelUtils.IsFuelItem(__instance.m_GearItem.GetComponent<GearItem>())) return true;

            __instance.m_RefuelPanel.SetActive(false);
            __instance.m_Button_Refuel.gameObject.SetActive(true);
            //__instance.m_Button_RefuelBackground.SetActive(true); // DONT ENABLE. BREAKS EVERYTHING

            float currentLiters     = FuelUtils.GetIndividualCurrentLiters(__instance.m_GearItem.GetComponent<GearItem>());
            float capacityLiters    = FuelUtils.GetIndividualCapacityLiters(__instance.m_GearItem.GetComponent<GearItem>());
            float totalCurrent      = FuelUtils.GetTotalCurrentLiters(__instance.m_GearItem.GetComponent<GearItem>());
            float totalCapacity     = FuelUtils.GetTotalCapacityLiters(__instance.m_GearItem.GetComponent<GearItem>());

            bool fuelIsAvailable    = totalCurrent > FuelUtils.MIN_LITERS;
            bool flag               = fuelIsAvailable && !Il2Cpp.Utils.Approximately(currentLiters, capacityLiters, FuelUtils.MIN_LITERS);

            __instance.m_Refuel_X.gameObject.SetActive(!flag);
            __instance.m_Button_Refuel.gameObject.GetComponent<Panel_Inventory_Examine_MenuItem>().SetDisabled(!flag);

            try
            {
                if (__instance.m_GearItem != null && !FuelUtils.IsKeroseneLamp(__instance.m_GearItem)) __instance.m_Button_RefuelBackground.SetActive(true);
            }
            catch (NullReferenceException)
            {
#if DEBUG
                FuelManager.LogError("Item is not proper, Panel_Inventory_Examine_RefreshRefuelPanel");
#endif
            }

            InterfaceManager.GetPanel<Panel_Inventory_Examine>().m_MouseRefuelButton.SetActive(flag);
            __instance.m_RequiresFuelMessage.SetActive(false);

            __instance.m_LanternFuelAmountLabel.text = $"{FuelUtils.GetLiquidQuantityStringNoOunces(currentLiters)} / {FuelUtils.GetLiquidQuantityStringNoOunces(capacityLiters)}";
            __instance.m_FuelSupplyAmountLabel.text = $"{FuelUtils.GetLiquidQuantityStringNoOunces(totalCurrent)} / {FuelUtils.GetLiquidQuantityStringNoOunces(totalCapacity)}";

            __instance.UpdateWeightAndConditionLabels();
#if DEBUG
            FuelManager.Log($"currentLiters: {currentLiters}, capacityLiters: {capacityLiters}, totalCurrent: {totalCurrent}, totalCapacity: {totalCapacity}, fuelIsAvailable: {fuelIsAvailable}, flag: {flag}");
            FuelManager.Log($"__instance.m_LanternFuelAmountLabel.text: {__instance.m_LanternFuelAmountLabel.text}");
            FuelManager.Log($"__instance.m_FuelSupplyAmountLabel.text: {__instance.m_FuelSupplyAmountLabel.text}");
#endif
            return false; // MUST BE FALSE
        }
    }
}
