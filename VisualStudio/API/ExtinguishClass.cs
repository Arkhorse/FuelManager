using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FuelManager
{
    public class ExtinguishCallback
    {
#pragma warning disable IDE1006 // Naming Styles
        public static Panel_ActionPicker? __instance { get; set; }
        private static Fire? fire { get; set; }
        private static WaterSupply cleanWaterSupply { get; } = GameManager.GetInventoryComponent().GetPotableWaterSupply().m_WaterSupply;
        private static WaterSupply dirtyWaterSupply { get; } = GameManager.GetInventoryComponent().GetNonPotableWaterSupply().m_WaterSupply;
#pragma warning restore IDE1006 // Naming Styles

        public static bool HasFire()
        {
            if (__instance != null)
            {
                GameObject target = __instance.GetObjectInteractedWith();

                if (target.name.Contains("WoodStove") || target.name.Contains("CampFire") || target.name.Contains("INTERACTIVE_FirePlace")) return true;
            }

            return false;
        }

        public static void OnExtinguishFire()
        {
            if (Settings.Instance.UseNonPotableWaterSupply && dirtyWaterSupply.m_VolumeInLiters >= Settings.Instance.waterToExtinguishFire)
            {
                OnExtinguishFireComplete(true);
                return;
            }
            else if (cleanWaterSupply.m_VolumeInLiters > Settings.Instance.waterToExtinguishFire)
            {
                OnExtinguishFireComplete(true);
                return;
            }
            else
            {
                // not enough water
                // GAMEPLAY_FM_Extinguish_Failed
                HUDMessage.AddMessage("Not Enough Water", false, true);
                GameAudioManager.PlayGUIError();
                Logger.Log($"Amount of clean water: {cleanWaterSupply.m_VolumeInLiters}");
                Logger.Log($"Amount of dirty water: {dirtyWaterSupply.m_VolumeInLiters}, enabled: {Settings.Instance.UseNonPotableWaterSupply}");
            }
        }

        public static void OnExtinguishFireComplete(bool success)
        {
            if (!success) return;

            fire = __instance!.m_ObjectInteractedWith.GetComponent<Fire>();

            if (fire)
            {
                if (Settings.Instance.UseNonPotableWaterSupply)
                {
                    dirtyWaterSupply.m_VolumeInLiters -= Settings.Instance.waterToExtinguishFire;
                }
                else
                {
                    cleanWaterSupply.m_VolumeInLiters -= Settings.Instance.waterToExtinguishFire;
                }
                fire.TurnOff();
                return;
            }
            else
            {
                Logger.LogError("Fire not found");
            }
        }
    }
}
