﻿using System.Text;

namespace FuelManager
{
	[RegisterTypeInIl2Cpp(false)]
	public class FuelItemAPI : MonoBehaviour
	{
		#region Repair
		public static void RefreshRepairComponent(GearItem? gi)
		{
			if (gi == null)
			{
				Main.Logger.Log($"Requested GearItem is null", FlaggedLoggingLevel.Debug);
				return;
			}

			Repairable? repair = gi.gameObject.GetComponent<Repairable>();

			if (repair is null)
			{
				Main.Logger.Log($"Requested GearItem {gi.name} does not have the Repairable Component", FlaggedLoggingLevel.Debug);
				return;
			}
			else
			{
#pragma warning disable IDE0059 // Unnecessary assignment of a value
				repair = null; // Want this to be null in order to remove the component already assigned
#pragma warning restore IDE0059 // Unnecessary assignment of a value
			}
		}

		public static void AddRepair(
			ref GearItem? gi,
			string[] requiredGear,
			int[] repairUnits,
			string[] extra,
			string audio,
			int duration = 15,
			float ConditionIncrease = 50,
			bool requiresTools = true,
			bool NeverFail = true)
		{
			if (gi == null) return;

			//GameObject? Target = GetInstancedObject(gi);

			//if (Target is null) return;

			var component = gi.gameObject.GetComponent<Repairable>();

			if (!(component == null)) return;

			try
			{
				Repairable repairable				= gi.gameObject.AddComponent<Repairable>();
				repairable.m_RepairAudio            = audio;
				repairable.m_DurationMinutes        = duration;
				repairable.m_ConditionIncrease      = ConditionIncrease;

				repairable.m_RequiredGear           = CommonUtilities.GetItems<GearItem>(requiredGear);
				repairable.m_RequiredGearUnits      = repairUnits;

				repairable.m_RepairToolChoices      = CommonUtilities.GetItems<ToolsItem>(extra);
				repairable.m_RequiresToolToRepair   = requiresTools;
				repairable.m_NeverFail              = NeverFail;
			}
			catch (Exception e)
			{
				Main.Logger.Log("Error attempting to add Repairable Component", FlaggedLoggingLevel.Exception, e);
			}
		}
		#endregion
		#region Harvest
		public static void RefreshHarvestComponent(GearItem? gi)
		{
			if (gi == null)
			{
				Main.Logger.Log($"Requested GearItem is null", FlaggedLoggingLevel.Debug);
				return;
			}

			Harvest? harvest = gi.gameObject.GetComponent<Harvest>();

			if (harvest == null)
			{
				Main.Logger.Log($"Requested GearItem {gi.name} does not have the Repairable Component", FlaggedLoggingLevel.Debug);
				return;
			}
			else
			{
#pragma warning disable IDE0059 // Unnecessary assignment of a value
				harvest = null; // Want this to be null in order to remove the component already assigned
#pragma warning restore IDE0059 // Unnecessary assignment of a value
			}
		}

		public static void AddHarvest(
			ref GearItem gi,
			string[] YieldGear,
			int[] YieldUnits,
			string[] RequiredTools,
			string audio,
			SkillType skillType = SkillType.None,
			int duration = 15
			)
		{
			if (gi == null) return;

			//GameObject? Target = GetInstancedObject(gi);

			//if (Target is null) return;

			var component = gi.gameObject.GetComponent<Harvest>();

			if (!(component == null)) return;

			try
			{
				Harvest harvest					= gi.gameObject.AddComponent<Harvest>();
				harvest.m_Audio					= audio;
				harvest.m_DurationMinutes		= duration;

				harvest.m_YieldGear				= CommonUtilities.GetItems<GearItem>(YieldGear);
				harvest.m_YieldGearUnits		= YieldUnits;

				harvest.m_AppliedSkillType		= skillType;
				harvest.m_RequiredTools			= CommonUtilities.GetItems<ToolsItem>(RequiredTools);
			}
			catch (Exception e)
			{
				Main.Logger.Log("Error while attempting to add Harvest Component", FlaggedLoggingLevel.Exception, e);
			}

		}
		#endregion
		#region Millable
		public static void AddMillable(
			ref GearItem gi,
			string[] RequiredGear,
			int[] RequiredGearUnits,
			string[] RestoreRequiredGear,
			int[] RestoreRequiredGearUnits,
			bool CanRestoreFromWornOut,
			int RepairDurationMinutes,
			int RecoveryDurationMinutes,
			SkillType Skill
			)
		{
			if (gi == null) return;

			//GameObject? Target = GetInstancedObject(gi);

			//if (Target is null) return;

			var component = gi.gameObject.GetComponent<Millable>();

			if (!(component == null)) return;

			try
			{
				Millable millable						= gi.gameObject.AddComponent<Millable>();
				millable.m_RepairRequiredGear			= CommonUtilities.GetItems<GearItem>(RequiredGear);
				millable.m_RepairRequiredGearUnits		= RequiredGearUnits;
				millable.m_RestoreRequiredGear			= CommonUtilities.GetItems<GearItem>(RestoreRequiredGear);
				millable.m_RestoreRequiredGearUnits		= RestoreRequiredGearUnits;
				millable.m_CanRestoreFromWornOut		= CanRestoreFromWornOut;
				millable.m_RepairDurationMinutes		= RepairDurationMinutes;
				millable.m_RecoveryDurationMinutes		= RecoveryDurationMinutes;
				millable.m_Skill						= Skill;
			}
			catch (Exception e)
			{
				Main.Logger.Log($"Attempting to add the Millable Component to {gi.name} failed", FlaggedLoggingLevel.Exception, e);
			}
		}
		#endregion
		#region Fuel Source
		public static void AddFuelSource(ref GearItem gi,
										 float burnHours,
										 float fireAge,
										 float fireStartDuration,
										 float fireStartSkill,
										 float heatIncrease,
										 float heatInner,
										 float heatOuter,
										 bool isBurnt,
										 bool isTinder,
										 bool isWet)
		{
			if (gi == null) return;

			//GameObject? Target = GetInstancedObject(gi);

			//if (Target is null) return;

			var component = gi.gameObject.GetComponent<FuelSourceItem>();

			if (!(component == null)) return;

			FuelSourceItem fuelSourceItem                   = CommonUtilities.GetOrCreateComponent<FuelSourceItem>(gi.gameObject);

			try
			{
				fuelSourceItem.m_IsBurntInFireTracked           = isBurnt;
				fuelSourceItem.m_IsWet                          = isWet;
				fuelSourceItem.m_IsTinder                       = isTinder;

				fuelSourceItem.m_FireAgeMinutesBeforeAdding     = fireAge;

				fuelSourceItem.m_HeatOuterRadius                = heatOuter;
				fuelSourceItem.m_HeatInnerRadius                = heatInner;
				fuelSourceItem.m_HeatIncrease                   = heatIncrease;

				fuelSourceItem.m_BurnDurationHours              = burnHours;
				fuelSourceItem.m_FireStartDurationModifier      = fireStartDuration;
				fuelSourceItem.m_FireStartSkillModifier         = fireStartSkill;
			}
			catch (Exception e)
			{
				Main.Logger.Log($"Could not add FuelSourceItem to {gi.name}", FlaggedLoggingLevel.Exception, e);
			}
		}
		#endregion

		//public static GameObject? GetInstancedObject(GearItem gi)
		//{
		//	GameObject? Target      = null;
		//	GameObject? gameObject  = null;
		//	GearItem? prefab        = null;

		//	StringBuilder sb = new StringBuilder();

		//	string name = CommonUtilities.NormalizeName(gi.name);

		//	if (gi is null) return null;

		//	try
		//	{
		//		gameObject = GameObject.Find(name);
		//	}
		//	catch (Exception e)
		//	{
		//		sb.Append("Could not Instantiate item ");
		//		if (!string.IsNullOrWhiteSpace(name))
		//		{
		//			sb.Append(name);
		//		}

		//		Main.Logger.Log(sb.ToString(), FlaggedLoggingLevel.Exception, e);
		//		sb.Clear();
		//	}

		//	if (gameObject == null)
		//	{
		//		prefab = CommonUtilities.GetGearItemPrefab(CommonUtilities.NormalizeName(name));
		//		if (!(prefab == null))
		//		{
		//			Target = prefab.gameObject;
		//		}
		//		return Target;
		//	}

		//	try
		//	{
		//		GearItem component = gameObject.GetComponent<GearItem>();
		//		prefab = CommonUtilities.GetGearItemPrefab(CommonUtilities.NormalizeName(component.name));
		//		if (prefab == null)
		//		{
		//			return null;
		//		}
		//		Target = prefab.gameObject;
		//	}
		//	catch (Exception e)
		//	{
		//		sb.Append("Attempting to get the Prefab failed, GearItem: ");
		//		if (!string.IsNullOrWhiteSpace(name))
		//		{
		//			sb.Append(name);
		//		}

		//		Target = gi.gameObject;

		//		if (Target == null)
		//		{
		//			sb.Append(" WARNING: TARGET IS NULL ");
		//		}

		//		Main.Logger.Log(sb.ToString(), FlaggedLoggingLevel.Exception, e);
		//	}

		//	return Target;
		//}
	}
}
