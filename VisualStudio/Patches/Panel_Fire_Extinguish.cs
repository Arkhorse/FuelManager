//namespace FuelManager
//{
//    [HarmonyPatch(typeof(Panel_ActionPicker), nameof(Panel_ActionPicker.Enable), new Type[] { typeof(bool) })]
//    public class Panel_ActionPicker_ShowActionPicker
//    {
//        public static void Postfix(ref Panel_ActionPicker __instance, ref bool enable)
//        {
//            if (!enable) return;
//            ExtinguishCallback.__instance = __instance;

//            __instance.TryGetComponentFromInteractedObject<Campfire>(out Campfire componentFromInteractedObject);
//            bool IsFireLit = componentFromInteractedObject!= null && !componentFromInteractedObject.enabled && componentFromInteractedObject.m_State == CampfireState.Lit;

//            //Main.Logger.Log($"Panel_ActionPicker is enabled");

//            // GAMEPLAY_FM_Extinguish
//            var ExtinguishCallBack = new ActionPickerItemData("ico_water_prep", "Extinguish Fire", new Action(ExtinguishCallback.OnExtinguishFire));
//            //Main.Logger.Log("ActionPickerItemData created");
//            //Main.Logger.Log($"Data: {ExtinguishCallBack.m_SpriteName}, {ExtinguishCallBack.m_LocID}");
            
//            if (ExtinguishCallback.HasFire())
//            {
//                __instance.m_ActionPickerItemDataList.Add(ExtinguishCallBack);
//            }

//            if (__instance.m_ActionPickerItemDataList.Contains(ExtinguishCallBack))
//            {
//                //Main.Logger.Log($"m_ActionPickerItemDataList contains the modded button. Object: {__instance.m_ObjectInteractedWith.name}, IsFireLit: {ExtinguishCallback.HasFire()}");
//            }
//            else
//            {
//                //Main.Logger.Log($"m_ActionPickerItemDataList does not contain the modded button. Object: {__instance.m_ObjectInteractedWith.name} IsFireLit: {ExtinguishCallback.HasFire()}");
//            }
//        }
//    }
//}
