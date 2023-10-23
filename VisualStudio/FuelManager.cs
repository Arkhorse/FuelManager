using Il2CppTLD.Gear;

namespace FuelManager
{
    internal class FuelManager : MelonMod
    {
        public static GearItem? Target { get; set; }
        public static LiquidType GetKerosene()
        {
            try
            {
                return LiquidType.GetKerosene();
            }
            catch
            {
                Logger.LogError("LiquidType.GetKerosene() was not found");
                throw;
            }
        }

        public static LiquidType GetLiquid(string liquid)
        {
            try
            {
                return liquid.ToLowerInvariant() switch
                {
                    "potable"       => LiquidType.GetPotableWater(),
                    "nonpotable"    => LiquidType.GetNonPotableWater(),
                    "kerosene"      => LiquidType.GetKerosene(),
                    "antiseptic"    => LiquidType.GetAntiseptic(),
                    _ => throw new BadMemeException($"string does not match existing LiquidType's, {liquid.ToLowerInvariant()}"),
                };
            }
            catch
            {
                Logger.LogError("LiquidType.GetKerosene() was not found");
                throw;
            }
        }

        public override void OnInitializeMelon()
        {
            Settings.OnLoad(false);
            Spawns.AddToModComponent();
            ConsoleCommands.RegisterCommands();
        }

        public override void OnUpdate()
        {
            base.OnUpdate();

            if (GameManager.IsEmptySceneActive()
                || GameManager.IsBootSceneActive()
                || GameManager.IsMainMenuActive()
                || GameManager.m_IsPaused
                || InterfaceManager.IsOverlayActiveCached())
            {
                return;
            }

            try
            {
                if (Settings.Instance.EnableRefuelLampKey)
                {
                    GearItem gi = GameManager.GetPlayerManagerComponent().m_ItemInHands;
                    if (gi == null) return;

                    KeroseneLampItem lamp = gi.GetComponent<KeroseneLampItem>();
                    if (lamp == null) return;

                    if (InputManager.GetKeyDown(InputManager.m_CurrentContext, Settings.Instance.RefuelLampKey))
                    {
                        FuelUtils.Refuel(gi, false, null);
                    }
                }
            }
            catch { }
        }
    }
}