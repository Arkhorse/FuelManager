using ExamineActionsAPI;

namespace FuelManager.API
{
#pragma warning disable CS8600, CS8602 // Converting null literal or possible null value to non-nullable type.
	class UnloadingFuelExamineAction : IExamineAction, IExamineActionProduceLiquid, IExamineActionDisplayInfo, IExamineActionCancellable
	{
		public UnloadingFuelExamineAction()
		{
			info = new(new() { m_LocalizationID = "Loaded" }, $"? L");
		}
		InfoItemConfig info;
		#region IExamineAction Implementation
		string IExamineAction.Id => nameof(UnloadingFuelExamineAction);
		string IExamineAction.MenuItemLocalizationKey => "(EAPI) Drain";
		string? IExamineAction.MenuItemSpriteName => "ico_lightSource_lantern";
		LocalizedString IExamineAction.ActionButtonLocalizedString { get; } = new LocalizedString() { m_LocalizationID = "(EAPI) Drain" };
		IExamineActionPanel? IExamineAction.CustomPanel => null;
		int IExamineAction.GetDurationMinutes(ExamineActionState state) => 3;
		float IExamineAction.GetProgressSeconds(ExamineActionState state) => 2;
		bool IExamineAction.IsActionAvailable(GearItem item)
		{
			return Fuel.IsFuelItem(item);
		}
		bool IExamineAction.CanPerform(ExamineActionState state)
		{
			if (state.Subject == null) return false;
			GearItem gi = state.Subject;
			ItemLiquidVolume currentLiters = Fuel.GetIndividualCurrentLiters(gi);
			ItemLiquidVolume totalCapacity = Fuel.GetTotalCapacityLiters(gi);
			ItemLiquidVolume totalCurrent = Fuel.GetTotalCurrentLiters(gi);

			Main.Logger.Log($"item is {gi.name}, currentLiters: {currentLiters}, totalCurrent {totalCurrent}, totalCapacity: {totalCapacity}", FlaggedLoggingLevel.Debug);

			return (!Constants.Empty(currentLiters)) && (!Fuel.IsFull(gi));
		}
		void IExamineAction.OnPerforming(ExamineActionState state)
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
		void IExamineAction.OnSuccess(ExamineActionState state)
		{
			if (state.Subject == null) return;
			GearItem gi = state.Subject;
			ItemLiquidVolume litersToDrain = Fuel.GetLitersToDrain(gi);
			Fuel.AddTotalCurrentLiters(litersToDrain, gi);
			Fuel.AddLiters(gi, -litersToDrain);
			if ((bool)state.Temp[0])
			{
				gi.m_KeroseneLampItem.TurnOn(true);
			}
		}
		bool IExamineAction.ShouldConsumeOnSuccess(ExamineActionState state) => false;
		void IExamineAction.OnActionInterruptedBySystem(ExamineActionState state)
		{
			StringBuilder sb = new();

			if (state.Subject.m_KeroseneLampItem != null)
			{
				sb.Append(state.Temp[0]);
			}

			Main.Logger.Log(sb.ToString(), FlaggedLoggingLevel.Debug);
		}
		void IExamineAction.OnActionDeselected(ExamineActionState state) { }
		void IExamineAction.OnActionSelected(ExamineActionState state) { }
		#endregion
		#region IExamineActionProduceLiquid Implementation
		void IExamineActionProduceLiquid.GetProductLiquid(ExamineActionState state, List<MaterialOrProductLiquidConf> liquids)
		{
			GearItem gi = state.Subject;
			if (gi == null) return;
			LiquidType kerosene = PowderAndLiquidTypesLocator.KeroseneType;
			if (kerosene == null) return;

			liquids.Add(new(kerosene, Fuel.GetIndividualCurrentLiters(gi).ToQuantity(1), 100));
		}
		#endregion
		#region IExamineActionDisplayInfo Implementation
		void IExamineActionDisplayInfo.GetInfoConfigs(ExamineActionState state, List<InfoItemConfig> configs)
		{
			info.Content = Fuel.GetLiquidQuantityString(state.Subject.gameObject.GetComponent<LiquidItem>().m_Liquid);
			configs.Add(info);
		}
		#endregion
		#region IExamineActionCancellable Implementation
		void IExamineActionCancellable.OnActionCancellation(ExamineActionState state)
		{
			
		}
		bool IExamineActionCancellable.ShouldConsumeOnCancellation(ExamineActionState state) => false;
		#endregion
	}
#pragma warning restore CS8600, CS8602 // Converting null literal or possible null value to non-nullable type.
}
