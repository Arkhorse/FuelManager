using Il2CppTLD.IntBackedUnit;

namespace FuelManager
{
    internal class Message
    {
        internal static void SendLostMessageDelayed(ItemLiquidVolume amount)
        {
            MelonCoroutines.Start(SendDelayedLostMessageIEnumerator(amount));
        }

        private static System.Collections.IEnumerator SendDelayedLostMessageIEnumerator(ItemLiquidVolume amount)
        {
            yield return null;
            yield return new WaitForSecondsRealtime(1f);

            SendLostMessageImmediate(amount);
        }

        internal static void SendLostMessageImmediate(ItemLiquidVolume amount)
        {
            GearMessage.AddMessage(
                "GEAR_JerrycanRusty",
                Localization.Get("GAMEPLAY_BFM_Lost"),
                $"{Localization.Get("GAMEPLAY_Kerosene")}, {Fuel.GetLiquidQuantityStringWithUnitsNoOunces(amount)}",
                Color.red,
                false
                );
            /*
            GearMessage.AddMessage(
                "GEAR_JerrycanRusty", Localization.Get("GAMEPLAY_BFM_Lost"), " " + Localization.Get("GAMEPLAY_Kerosene") + " (" + Fuel.GetLiquidQuantityStringWithUnitsNoOunces(InterfaceManager.m_Panel_OptionsMenu.m_State.m_Units, amount) + ")", Color.red, false);
            */
        }
    }
}
