//#define RMU

#if RMU
using RadialMenuUtilities;
#endif

namespace FuelManager
{
    public enum LoggingLevel
    {
        None, Info, Debug, Warn, Error, Fatal, Trace
    }

    internal class Settings : JsonModSettings
    {
        internal static bool RadialOverride { get; set; } = false;
        internal static Settings Instance { get; } = new();
#if RMU
        internal static CustomRadialMenu? radialMenu { get; set; }
#endif
        internal static List<string> GearNames { get; } = new List<string>
        { "GEAR_GasCan", "GEAR_JerrycanRusty", "GEAR_LampFuel", "GEAR_LampFuelFull" };

        internal static List<GearItem> GearItems { get; set; } = new();

#if RMU
        [Section("Gameplay Settings")]
        [Name("Use Radial Menu")]
        [Description("Enables a new radial menu for you to easily access your fuel containers.")]
        public bool enableRadial = false;

        [Name("Key for Radial Menu")]
        [Description("The key you press to show the new menu.")]
        public KeyCode keyCode = KeyCode.G;
#endif
        [Section("Kerosene Lamp refueling")]

        [Name("Enable")]
        [Description("If your holding a lamp and all other conditions are met, this will refuel the lamp when you press the hotkey below")]
        public bool EnableRefuelLampKey = false;

        [Name("Refuel Lamp Hotkey")]
        public KeyCode RefuelLampKey = KeyCode.R;

        [Section("Main")]

        [Name("Refuel Time")]
        [Description("How much time it takes to refuel/ drain, in seconds. Default: 3")]
        [Slider(1f, 60f, 240)]
        public float refuelTime = 3f;

        [Name("Amount of water to consume when extinguishing fires")]
        //[Description("")]
        [Slider(0.1f, 10f)]
        public float waterToExtinguishFire = 0.1f;

        [Name("Use Non Potable Water")]
        [Description("Will use dirty water first if there is any")]
        public bool UseNonPotableWaterSupply = false;

        [Name("Logging")]
        [Description("Enables extra logging that will spam your log")]
        public bool ExtraLogging = false;

        [Section("Spawn Settings")]

        [Name("Pilgram / Very High Loot Custom")]
        [Description("Setting to zero disables them on this game mode")]
        [Slider(0f, 100f, 101)]
        public float pilgramSpawnExpectation = 70f;

        [Name("Voyager / High Loot Custom")]
        [Description("Setting to zero disables them on this game mode")]
        [Slider(0f, 100f, 101)]
        public float voyagerSpawnExpectation = 40f;

        [Name("Stalker / Medium Loot Custom")]
        [Description("Setting to zero disables them on this game mode")]
        [Slider(0f, 100f, 101)]
        public float stalkerSpawnExpectation = 20f;

        [Name("Interloper / Low Loot Custom")]
        [Description("Setting to zero disables them on this game mode")]
        [Slider(0f, 100f, 101)]
        public float interloperSpawnExpectation = 8f;

        [Name("Challenges")]
        [Description("Setting to zero disables them on this game mode")]
        [Slider(0f, 100f, 101)]
        public float challengeSpawnExpectation = 50f;

        //[Section("Fuel Items")]

        //[Name("")]
        //[Description("")]
        //public bool AddFuelTo = false;

        //[Section("Logging")]

        //[Name("Level")]
        //[Description("Depending on the level of logging, you will get different logging")]
        //public LoggingLevel loggingLevel = LoggingLevel.Debug;

        protected override void OnConfirm()
        {
            Refresh();
            base.OnConfirm();
        }
#if RMU
        private void ConstructRadialArm(bool enable)
        {
            RadialOverride = enable;
            if (!enable) return;

            try
            {
                radialMenu = new CustomRadialMenu(
                    GearNames,
                    Instance.keyCode,
                    CustomRadialMenuType.AllOfEach,
                    Instance.enableRadial,
                    BuildInfo.GUIName
                    );

                radialMenu!.SetValues(keyCode, enableRadial);
            }
            catch (MissingMethodException)
            {
                if (RadialOverride) return;
                else
                {
                    Logger.LogError("MissingMethodException: Either your missing RadialMenuUtilies or your version of this mod or RMU is outdated", Color.red);
                    throw;
                }
            }
            catch
            {
                Logger.LogError($"Threw exception while attempting to create new radial menu");
                throw;
            }
        }
#endif
        private void Refresh()
        {
            SetFieldVisible(nameof(refuelTime), true);
            SetFieldVisible(nameof(pilgramSpawnExpectation), true);
            SetFieldVisible(nameof(voyagerSpawnExpectation), true);
            SetFieldVisible(nameof(stalkerSpawnExpectation), true);
            SetFieldVisible(nameof(interloperSpawnExpectation), true);
            SetFieldVisible(nameof(challengeSpawnExpectation), true);

#if RMU
            SetFieldVisible(nameof(enableRadial), !RadialOverride);
            SetFieldVisible(nameof(keyCode), !RadialOverride);
#endif
        }

        internal static void OnLoad(bool EnableRadial)
        {
            Instance.AddToModSettings(BuildInfo.GUIName);
#if RMU
            Instance.ConstructRadialArm(EnableRadial);
#endif
            Instance.RefreshGUI();
        }
    }
}
