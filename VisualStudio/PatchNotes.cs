﻿using System.Text.Json.Serialization;

namespace FuelManager
{
    public class Patch
    {
#nullable disable
        [JsonInclude]
        public Version m_Version { get; set; }
        [JsonInclude]
        public List<string> Changes { get; set; }
        public Patch Instance { get; set; } = null;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="m_Version"></param>
        /// <param name="Changes"></param>
        public Patch(Version m_Version, List<string> Changes)
        {
            Instance = this;
            this.m_Version = m_Version;
            this.Changes = Changes;
        }
#nullable enable
    }

    public class PatchNotes
    {
        /// <summary>
        /// This is used in console commands to print the change logs.
        /// </summary>
        public static List<Patch> ChangeNotes = new();

        internal static void CreateChangelog()
        {
            ChangeNotes.Add(
            new Patch(
                new Version(1, 2, 0),
                new List<string>
                {
                    "Change _settings to Instance",
                    "Add a new Constants class",
                    "Add templated Logger class",
                    "Add GetItem<T>()",
                    "Add suppression CS8600 and CS8604 to Buttons.cs",
                    "Fix ambiguios reference with new HL code for PlayerManager.DeductLiquidFromInventory",
                    "Switch to using a FuelAPI to add Harvest and Repairable to existing items"
                })
            );
            ChangeNotes.Add(
            new Patch(
                new Version(1, 2, 1),
                new List<string>
                {
                    "Nothing notworthy, minor change to permit easier log reading"
                })
            );
            ChangeNotes.Add(
            new Patch(
                new Version(1, 2, 2),
                new List<string>
                {
                    "Disable radial as HL changed how this works. Requires work on RMU"
                })
            );
            ChangeNotes.Add(
            new Patch(
                new Version(1,2,3),
                new List<string>
                {
                    "Changed GearItem.Deserialize to GearItem.Awake as the former was no longer working"
                })
            );
            ChangeNotes.Add(
            new Patch(
                new Version(1,2,4),
                new List<string>
                {
                    "Add Try|Catch too all FuelItemAPI methods",
                    "Removed GetComponent<GearItem>() from a bunch of calls and methods",
                    "Targeted GameObject for FuelItemAPI method's",
                    "Switched to using GearItem.DisplayName for checks",
                    "RMU dedicated code changes",
                    "Updated GasCan properties: WeightKG: 0.1, InitialCondition: High, InspectDistance: 0.5"
                })
            );
            ChangeNotes.Add(
                new Patch(
                    new Version(1, 2, 5),
                new List<string>
                {
                    "Game Update 2.23 on 2023/09/26",
                    "Game removed GearLiquidTypeEnum, updated calls to use FuelManager.GetKerosene()",
                    "Removed Repairable from all vanilla items as its still not working. Harvest remains active"
                })
            );
            ChangeNotes.Add(
                new Patch(
                    new Version(1, 2, 6),
                new List<string>
                {
                    "Add last RMU conditional",
                    "Disable RMU"
                })
            );
            ChangeNotes.Add(
                new Patch(
                    new Version(1, 2, 7),
                new List<string>
                {
                    "Merge PR by Elderly-Emre",
                    "Add Modders Gear toolbox optional version"
                })
            );
        }

        public static void PrintChangeLog()
        {
            for (int i = 0; i < ChangeNotes.Count; i++)
            {
                Logger.Log($"Version: {ChangeNotes[i].Instance.m_Version}");
                Logger.Log("Changes:");

                for (int v = 0; v < ChangeNotes[i].Instance.Changes.Count; v++)
                {
                    Logger.Log($"\t{ChangeNotes[i].Instance.Changes[v]}");
                }
            }
        }
    }
}
