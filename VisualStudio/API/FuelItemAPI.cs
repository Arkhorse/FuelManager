namespace FuelManager
{
	public static class FuelItemAPI
	{
		/// <summary>
		/// Adds all the components that are currently added to the vanilla items
		/// </summary>
		/// <param name="gi">The GearItem instance to use</param>
		/// <param name="RepairUnits">The number of things used to repair this item (currently just scrap metal)</param>
		/// <param name="HarvestUnits">The number of things that harvesting this item will get (currently just scrap metal)</param>
		/// <param name="MillableRepairUnits">The number of things used to repair this item at a mill (currently just scrap metal)</param>
		/// <param name="MillableRestoreUnits">The number of things used to restore this item at the mill (currently just scrap metal)</param>
		/// <returns>returns <see langword="true"/> if it succeeds, <see langword="false"/> otherwise</returns>
		/// <remarks>This method is entirely written with try/catches in place</remarks>
		public static bool ApplyAllComponents(ref GearItem gi, int RepairUnits, int HarvestUnits, int MillableRepairUnits, int MillableRestoreUnits)
		{
			try
			{
				AddRepair(ref gi, Constants.REPAIR_TOOLS, [RepairUnits], Constants.REPAIR_TOOLS, "Play_RepairingMetal", 15, 25);
				Main.Logger.Log($"Adding {nameof(Repairable)} to {gi.name} was successful", FlaggedLoggingLevel.Debug);
			}
			catch (Exception e)
			{
				Main.Logger.Log($"Adding {nameof(Repairable)} failed\n", FlaggedLoggingLevel.Exception, e);
				return false;
			}
			try
			{
				AddHarvest(ref gi, Constants.REPAIR_HARVEST_GEAR, [HarvestUnits], Constants.HARVEST_TOOLS, "Play_HarvestingMetalSaw");
				Main.Logger.Log($"Adding {nameof(Harvest)} to {gi.name} was successful", FlaggedLoggingLevel.Debug);
			}
			catch (Exception e)
			{
				Main.Logger.Log($"Adding {nameof(Harvest)} failed\n", FlaggedLoggingLevel.Exception, e);
				return false;
			}
			try
			{
				AddMillable(ref gi, Constants.REPAIR_HARVEST_GEAR, [MillableRepairUnits], Constants.REPAIR_HARVEST_GEAR, [MillableRestoreUnits], true, 30, 60, SkillType.ToolRepair);
				Main.Logger.Log($"Adding {nameof(Millable)} to {gi.name} was successful", FlaggedLoggingLevel.Debug);
			}
			catch (Exception e)
			{
				Main.Logger.Log($"Adding {nameof(Millable)} failed\n", FlaggedLoggingLevel.Exception, e);
				return false;
			}

			return true;
		}

		/* NOTES
			All components need to have fully filled out fields
		*/

		#region Repair
		/// <summary>
		/// Used with the console command
		/// </summary>
		/// <param name="gi"></param>
		public static void RefreshRepairComponent(GearItem gi)
		{
			if (gi == null)
			{
				Main.Logger.Log($"Requested GearItem is null", FlaggedLoggingLevel.Debug);
				return;
			}

			Repairable repair = gi.gameObject.GetComponent<Repairable>();

			if (repair is null)
			{
				Main.Logger.Log($"Requested GearItem {gi.name} does not have the Repairable Component", FlaggedLoggingLevel.Debug);
				return;
			}
			else
			{
#pragma warning disable IDE0059, CS8600
				repair = null; // Want this to be null in order to remove the component already assigned
#pragma warning restore IDE0059, CS8600
			}
		}

		public static void AddRepair(
			ref GearItem gi, string[] requiredGear, int[] repairUnits, string[] extra, string audio, int duration = 15, float ConditionIncrease = 50, bool requiresTools = true, bool NeverFail = true)
		{
			if (gi == null) return;

			//GameObject? Target = GetInstancedObject(gi);
			//if (Target is null) return;
			//var component = gi.gameObject.GetComponent<Repairable>();
			//if (!(component == null)) return;

			Repairable component = CommonUtilities.GetOrCreateComponent<Repairable>(gi);
			//Repairable component = new Repairable();
			//gi.gameObject.AddComponent<Repairable>();

			GearItem[] gear = CommonUtilities.GetItems<GearItem>(requiredGear);
			ToolsItem[] tools = CommonUtilities.GetItems<ToolsItem>(extra);

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
				component.m_RepairAudio				= audio;
				component.m_DurationMinutes			= duration;
				component.m_ConditionIncrease		= ConditionIncrease;
				component.m_RequiredGear			= gear;
				component.m_RequiredGearUnits		= repairUnits;

				component.m_RepairToolChoices		= tools;
				component.m_RequiresToolToRepair	= requiresTools;
				component.m_NeverFail				= NeverFail;
				component.m_RepairConditionCap		= 100f;

				gi.m_Repairable = gi.gameObject.GetComponent<Repairable>();
			}
			catch (Exception e)
			{
				Main.Logger.Log("Error attempting to add Repairable Component", FlaggedLoggingLevel.Exception, e);
			}
		}
		#endregion
		#region Harvest
		/// <summary>
		/// Used with the console command
		/// </summary>
		/// <param name="gi"></param>
		public static void RefreshHarvestComponent(GearItem gi)
		{
			if (gi == null)
			{
				Main.Logger.Log($"Requested GearItem is null", FlaggedLoggingLevel.Debug);
				return;
			}

			Harvest harvest = gi.gameObject.GetComponent<Harvest>();

			if (harvest == null)
			{
				Main.Logger.Log($"Requested GearItem {gi.name} does not have the Repairable Component", FlaggedLoggingLevel.Debug);
				return;
			}
			else
			{
#pragma warning disable IDE0059, CS8600
				harvest = null; // Want this to be null in order to remove the component already assigned
#pragma warning restore IDE0059, CS8600
			}
		}

		public static void AddHarvest(
			ref GearItem gi, string[] YieldGear, int[] YieldUnits, string[] RequiredTools, string audio, SkillType skillType = SkillType.None, int duration = 15)
		{
			if (gi == null) return;

			//GameObject? Target = GetInstancedObject(gi);
			//if (Target is null) return;
			//var component = gi.gameObject.GetComponent<Harvest>();
			//if (!(component == null)) return;

			Harvest component = CommonUtilities.GetOrCreateComponent<Harvest>(gi);
			//Harvest component = new Harvest();
			//gi.gameObject.AddComponent<Harvest>();

			GearItem[] yieldgear = CommonUtilities.GetItems<GearItem>(YieldGear);
			ToolsItem[] tools = CommonUtilities.GetItems<ToolsItem>(RequiredTools);

			if (component == null)
			{
				Main.Logger.Log($"component is null", FlaggedLoggingLevel.Critical);
				return;
			}
			else
			{
				Main.Logger.Log($"component is not null", FlaggedLoggingLevel.Debug);
			}
			if (yieldgear == null || yieldgear.Length == 0)
			{
				Main.Logger.Log($"yieldgear is null or empty", FlaggedLoggingLevel.Critical);
				return;
			}
			else
			{
				Main.Logger.Log($"yieldgear is not null and has {yieldgear.Length} items", FlaggedLoggingLevel.Debug);
			}
			if (tools == null || tools.Length == 0)
			{
				Main.Logger.Log($"tools is null or empty", FlaggedLoggingLevel.Critical);
				return;
			}
			else
			{
				Main.Logger.Log($"tools is not null and has {tools.Length} items", FlaggedLoggingLevel.Debug);
			}

			try
			{
				component.m_Audio = audio;
				component.m_DurationMinutes = duration;
				component.m_YieldGear = yieldgear;
				component.m_YieldGearUnits = YieldUnits;
				component.m_AppliedSkillType = skillType;
				component.m_RequiredTools = tools;
				// these are required as null references are bad
				component.m_YieldPowder = new(0);
				component.m_YieldPowderAmount = new(0);

				gi.m_Harvest = gi.gameObject.GetComponent<Harvest>();
			}
			catch (Exception e)
			{
				Main.Logger.Log($"Attempting to add the Harvest Component to {gi.name} failed", FlaggedLoggingLevel.Exception, e);
			}
		}
		#endregion
		#region Millable
		public static void AddMillable(
			ref GearItem gi, string[] RequiredGear, int[] RequiredGearUnits, string[] RestoreRequiredGear, int[] RestoreRequiredGearUnits, bool CanRestoreFromWornOut, int RepairDurationMinutes, int RecoveryDurationMinutes, SkillType Skill)
		{
			if (gi == null) return;

			//GameObject? Target = GetInstancedObject(gi);
			//if (Target is null) return;
			//var component = gi.gameObject.GetComponent<Millable>();
			//if (!(component == null)) return;

			Millable component = CommonUtilities.GetOrCreateComponent<Millable>(gi);
			GearItem[] repair = CommonUtilities.GetItems<GearItem>(RequiredGear);
			GearItem[] restore = CommonUtilities.GetItems<GearItem>(RestoreRequiredGear);

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
				component.m_RestoreRequiredGearUnits	= RestoreRequiredGearUnits;
				component.m_CanRestoreFromWornOut		= CanRestoreFromWornOut;
				component.m_RepairDurationMinutes		= RepairDurationMinutes;
				component.m_RecoveryDurationMinutes		= RecoveryDurationMinutes;
				component.m_Skill						= Skill;
			}
			catch (Exception e)
			{
				Main.Logger.Log($"Attempting to add the {nameof(Millable)} Component to {gi.name} failed", FlaggedLoggingLevel.Exception, e);
			}
		}
		#endregion
		#region Fuel Source
		public static void AddFuelSource(
			ref GearItem gi, float burnHours, float fireAge, float fireStartDuration, float fireStartSkill, float heatIncrease, float heatInner, float heatOuter, bool isBurnt, bool isTinder, bool isWet)
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
