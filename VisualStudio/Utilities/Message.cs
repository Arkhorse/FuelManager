namespace FuelManager
{
    internal class Message
    {
        internal static Panel_OptionsMenu _Panel_OptionsMenu = new();
        internal static void SendLostMessageDelayed(float amount)
        {
            MelonCoroutines.Start(SendDelayedLostMessageIEnumerator(amount));
        }

        private static System.Collections.IEnumerator SendDelayedLostMessageIEnumerator(float amount)
        {
            yield return new WaitForSeconds(1f);

            SendLostMessageImmediate(amount);
        }

        internal static void SendLostMessageImmediate(float amount)
        {
            GearMessage.AddMessage(
                "GEAR_JerrycanRusty",
                Localization.Get("GAMEPLAY_BFM_Lost"),
                $"{Localization.Get("GAMEPLAY_Kerosene")}, {FuelUtils.GetLiquidQuantityStringWithUnitsNoOunces(amount)}",
                Color.red,
                false
                );
            /*
            GearMessage.AddMessage(
                "GEAR_JerrycanRusty", Localization.Get("GAMEPLAY_BFM_Lost"), " " + Localization.Get("GAMEPLAY_Kerosene") + " (" + FuelUtils.GetLiquidQuantityStringWithUnitsNoOunces(InterfaceManager.m_Panel_OptionsMenu.m_State.m_Units, amount) + ")", Color.red, false);
            */
        }
    }
}
