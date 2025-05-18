using ExamineActionsAPI;

namespace FuelManager.API
{
	class UnloadingFuelExamineAction : IExamineAction, IExamineActionProduceLiquid, IExamineActionDisplayInfo
	{
		public UnloadingFuelExamineAction()
		{
			info = new(new() { m_LocalizationID = "Loaded" }, $"? L");
		}
		public string Id => nameof(UnloadingFuelExamineAction);
		public string MenuItemLocalizationKey => "GAMEPLAY_BFM_Drain";
		public string? MenuItemSpriteName => "ico_lightSource_lantern";
		public LocalizedString ActionButtonLocalizedString { get; } = new LocalizedString() { m_LocalizationID = "GAMEPLAY_BFM_Drain" };
		public IExamineActionPanel? CustomPanel => null;
		InfoItemConfig info;
		public bool IsActionAvailable(GearItem item)
		{
			return Fuel.IsFuelItem(item);
		}
		public bool CanPerform(ExamineActionState state)
		{
			if (state.Subject == null) return false;
			GearItem gi = state.Subject;
			ItemLiquidVolume currentLiters = Fuel.GetIndividualCurrentLiters(gi);
			ItemLiquidVolume totalCapacity = Fuel.GetTotalCapacityLiters(gi);
			ItemLiquidVolume totalCurrent = Fuel.GetTotalCurrentLiters(gi);

			Main.Logger.Log($"item is {gi.name}, currentLiters: {currentLiters}, totalCurrent {totalCurrent}, totalCapacity: {totalCapacity}", FlaggedLoggingLevel.Debug);

			return (!Constants.Empty(currentLiters)) && (!Fuel.IsFull(gi));
		}
		public void OnPerform(ExamineActionState state)
		{
			if (state.Subject == null) return;
			GearItem gi = state.Subject;

			ItemLiquidVolume currentLiters = Fuel.GetIndividualCurrentLiters(gi);
			ItemLiquidVolume totalCapacity = Fuel.GetTotalCapacityLiters(gi);
			ItemLiquidVolume totalCurrent = Fuel.GetTotalCurrentLiters(gi);
			if (Fuel.IsKeroseneLamp(gi))
			{
				bool wason = gi.m_KeroseneLampItem.IsOn();
				state.Temp[0] = wason;
				if (wason) gi.m_KeroseneLampItem.TurnOff(true);
			}
			state.Temp[1] = currentLiters;
		}
		public void OnSuccess(ExamineActionState state)
		{
			if (state.Subject == null) return;
			GearItem gi = state.Subject;
			ItemLiquidVolume litersToDrain = Fuel.GetLitersToDrain(gi);
			Fuel.AddTotalCurrentLiters(litersToDrain, gi);
			Fuel.AddLiters(gi, -litersToDrain);
		}
		public int CalculateDurationMinutes(ExamineActionState state) => 3;
		public float CalculateProgressSeconds(ExamineActionState state) => 2;
		public bool ConsumeOnSuccess(ExamineActionState state) => false;
		public void GetProductLiquid(ExamineActionState state, List<MaterialOrProductLiquidConf> liquids)
		{
			GearItem gi = state.Subject;
			if (gi == null) return;
			LiquidType kerosene = PowderAndLiquidTypesLocator.KeroseneType;
			if (kerosene == null) return;

			liquids.Add(new(kerosene, Fuel.GetIndividualCurrentLiters(gi).ToQuantity(1), 100));
		}
		public void OnActionDeselected(ExamineActionState state) { }
		public void OnActionInterruptedBySystem(ExamineActionState state) { }
		public void OnActionSelected(ExamineActionState state) { }
		public void OnPerforming(ExamineActionState state)
		{
			throw new NotImplementedException();
		}
		public int GetDurationMinutes(ExamineActionState state)
		{
			throw new NotImplementedException();
		}
		public float GetProgressSeconds(ExamineActionState state)
		{
			throw new NotImplementedException();
		}
		public bool ShouldConsumeOnSuccess(ExamineActionState state) => false;
		public void GetInfoConfigs(ExamineActionState state, List<InfoItemConfig> configs)
		{
			info.Content = Fuel.GetLiquidQuantityString(state.Subject.gameObject.GetComponent<LiquidItem>().m_Liquid);
			configs.Add(info);
		}
	}
}
