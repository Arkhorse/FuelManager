//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//using ExamineActionsAPI;

//using Il2Cpp;

//using Il2CppTLD.Gear;
//using Il2CppTLD.IntBackedUnit;

//using static UnityEngine.GraphicsBuffer;

//namespace FuelManager.API
//{
//	class UnloadingFuelAction : IExamineAction, IExamineActionProduceLiquid
//	{
//		public UnloadingFuelAction() { }
//		public string Id => nameof(UnloadingFuelAction);
//		public string MenuItemLocalizationKey => "GAMEPLAY_BFM_Drain";
//		public string? MenuItemSpriteName => "ico_lightSource_lantern";
//		public LocalizedString ActionButtonLocalizedString { get; } = new LocalizedString() { m_LocalizationID = "GAMEPLAY_BFM_Drain" };
//		public IExamineActionPanel? CustomPanel => null;

//		public bool IsActionAvailable(ref GearItem item)
//		{
//			return Fuel.IsFuelItem(ref item);
//		}
//		public bool CanPerform(ExamineActionState state)
//		{
//			if (state.Subject == null) return false;
//			GearItem gi = state.Subject;
//			ItemLiquidVolume currentLiters = Fuel.GetIndividualCurrentLiters(ref gi);
//			ItemLiquidVolume totalCapacity = Fuel.GetTotalCapacityLiters(ref gi);
//			ItemLiquidVolume totalCurrent = Fuel.GetTotalCurrentLiters(ref gi);

//			Main.Logger.Log($"item is {gi.name}, currentLiters: {currentLiters}, totalCurrent {totalCurrent}, totalCapacity: {totalCapacity}", FlaggedLoggingLevel.Debug);

//			return (currentLiters != ItemLiquidVolume.Zero) && (totalCapacity != totalCurrent);
//		}
//		public void OnPerform(ExamineActionState state)
//		{
//			if (state.Subject == null) return;
//			GearItem gi = state.Subject;

//			ItemLiquidVolume currentLiters = Fuel.GetIndividualCurrentLiters(ref gi);
//			ItemLiquidVolume totalCapacity = Fuel.GetTotalCapacityLiters(ref gi);
//			ItemLiquidVolume totalCurrent = Fuel.GetTotalCurrentLiters(ref gi);
//			if (Fuel.IsKeroseneLamp(ref gi))
//			{
//				bool wason = gi.m_KeroseneLampItem.IsOn();
//				state.Temp[0] = wason;
//				if (wason) gi.m_KeroseneLampItem.TurnOff(true);
//			}
//			state.Temp[1] = currentLiters;
//		}
//		public void OnSuccess(ExamineActionState state)
//		{
//			if (state.Subject == null) return;
//			GearItem gi = state.Subject;
//			ItemLiquidVolume litersToDrain = Fuel.GetLitersToDrain(ref gi);
//			Fuel.AddTotalCurrentLiters(litersToDrain, ref gi);
//			Fuel.AddLiters(ref gi, -litersToDrain);
//		}

//		public int CalculateDurationMinutes(ExamineActionState state) => 3;
//		public float CalculateProgressSeconds(ExamineActionState state) => 2;
//		public bool ConsumeOnSuccess(ExamineActionState state) => false;

//		public void GetProductLiquid(ExamineActionState state, List<MaterialOrProductLiquidConf> liquids)
//		{
//			GearItem? gi = state.Subject;
//			if (gi == null) return;
//			var kerosene = ExamineActionsAPI.PowderAndLiquidTypesLocator.KeroseneType;
//			if (kerosene == null) return;

//			liquids.Add(new(kerosene, Fuel.GetIndividualCurrentLiters(ref gi).ToQuantity(1), 100));
//		}

//		public void OnActionDeselected(ExamineActionState state) { }
//		public void OnActionInterruptedBySystem(ExamineActionState state) { }
//		public void OnActionSelected(ExamineActionState state) { }
//	}
//}
