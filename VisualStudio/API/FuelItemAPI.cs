using System.Text;

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
			//var component = gi.gameObject.GetComponent<Repairable>();
			//if (!(component == null)) return;

			var component = CommonUtilities.GetOrCreateComponent<Repairable>(gi);

			var gear = CommonUtilities.GetItems<GearItem>(requiredGear);
			var tools = CommonUtilities.GetItems<ToolsItem>(extra);

			if (component == null)
			{
				Main.Logger.Log($"component is null", FlaggedLoggingLevel.Critical);
				return;
			}
			if (requiredGear == null || requiredGear.Length == 0)
			{
				Main.Logger.Log($"requiredGear is null or empty", FlaggedLoggingLevel.Critical);
				return;
			}
			if (tools == null || tools.Length == 0)
			{
				Main.Logger.Log($"tools is null or empty", FlaggedLoggingLevel.Critical);
				return;
			}

			try
			{
				component.m_RepairAudio            = audio;
				component.m_DurationMinutes        = duration;
				component.m_ConditionIncrease      = ConditionIncrease;

				component.m_RequiredGear           = gear;
				component.m_RequiredGearUnits      = repairUnits;

				component.m_RepairToolChoices      = tools;
				component.m_RequiresToolToRepair   = requiresTools;
				component.m_NeverFail              = NeverFail;
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
			//var component = gi.gameObject.GetComponent<Harvest>();
			//if (!(component == null)) return;

			var component = CommonUtilities.GetOrCreateComponent<Harvest>(gi);
			var yieldgear = CommonUtilities.GetItems<GearItem>(YieldGear);
			var tools = CommonUtilities.GetItems<ToolsItem>(RequiredTools);

			if (component == null)
			{
				Main.Logger.Log($"component is null", FlaggedLoggingLevel.Critical);
				return;
			}
			if (yieldgear == null || yieldgear.Length == 0)
			{
				Main.Logger.Log($"yieldgear is null or empty", FlaggedLoggingLevel.Critical);
				return;
			}
			if (tools == null || tools.Length == 0)
			{
				Main.Logger.Log($"tools is null or empty", FlaggedLoggingLevel.Critical);
				return;
			}

			component.m_Audio = audio;
			component.m_DurationMinutes = duration;

			component.m_YieldGear = yieldgear;
			component.m_YieldGearUnits = YieldUnits;

			component.m_AppliedSkillType = skillType;
			component.m_RequiredTools = tools;
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
			//var component = gi.gameObject.GetComponent<Millable>();
			//if (!(component == null)) return;

			var component = CommonUtilities.GetOrCreateComponent<Millable>(gi);
			var repair = CommonUtilities.GetItems<GearItem>(RequiredGear);
			var restore = CommonUtilities.GetItems<GearItem>(RestoreRequiredGear);

			if (component == null)
			{
				Main.Logger.Log($"component is null", FlaggedLoggingLevel.Critical);
				return;
			}
			if (repair == null || repair.Length == 0)
			{
				Main.Logger.Log($"repair is null or empty", FlaggedLoggingLevel.Critical);
				return;
			}
			if (restore == null || restore.Length == 0)
			{
				Main.Logger.Log($"restore is null or empty", FlaggedLoggingLevel.Critical);
				return;
			}

			try
			{
				component.m_RepairRequiredGear			= repair;
				component.m_RepairRequiredGearUnits		= RequiredGearUnits;
				component.m_RestoreRequiredGear			= restore;
				component.m_RestoreRequiredGearUnits		= RestoreRequiredGearUnits;
				component.m_CanRestoreFromWornOut		= CanRestoreFromWornOut;
				component.m_RepairDurationMinutes		= RepairDurationMinutes;
				component.m_RecoveryDurationMinutes		= RecoveryDurationMinutes;
				component.m_Skill						= Skill;
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
