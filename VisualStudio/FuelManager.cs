namespace FuelManager
{
	public class Main : MelonMod
	{
#pragma warning disable CS8618, CA2211
		public static GearItem Target;
#pragma warning restore CS8618, CA2211
		[Obsolete("Use ItemLiquidVolume.Zero instead")]
		public static ItemLiquidVolume MIN_LITERS_VOLUME { get; } = ItemLiquidVolume.FromLiters(0.001f);
		public static string REFUEL_AUDIO { get; } = "Play_SndActionRefuelLantern";
		internal static ComplexLogger<Main> Logger { get; } = new();

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
			Setup.VerifyMCFiles();
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
						//Logger.Log($"Current Context: {InputManager.m_CurrentContext.name}", FlaggedLoggingLevel.Debug);
						Fuel.Refuel(gi, false, null);
					}
				}
			}
			catch (Exception e)
			{
				Logger.Log($"Attempting to handle refueling lamps VIA hotkey {Settings.Instance.RefuelLampKey} failed: ", FlaggedLoggingLevel.Verbose|FlaggedLoggingLevel.Exception, e);
			}
		}
	}
}
