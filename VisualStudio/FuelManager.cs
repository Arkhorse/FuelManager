global using ComplexLogger;

global using FuelManager.Utilities;

using Il2CppTLD.Gear;


namespace FuelManager
{
    public class Main : MelonMod
    {
        public static GearItem? Target { get; set; }
        public static float MIN_LITERS { get; } = 0.001f;
        public static string REFUEL_AUDIO { get; } = "Play_SndActionRefuelLantern";
        public static ComplexLogger<Main> Logger;

        public static LiquidType GetKerosene()
        {
            try
            {
                return LiquidType.GetKerosene();
            }
            catch (Exception e)
            {
                throw new BadMemeException($"LiquidType.GetKerosene() was not found due to {e.Message}");
            }
        }

        public override void OnInitializeMelon()
        {
            Logger ??= new();

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
                        Fuel.Refuel(gi, false, null);
                    }
                }
            }
            catch { }
        }
    }
}
