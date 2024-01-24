using System;
using HarmonyLib;
using TMPro;
using UnityEngine;
using static InventoryGrid;
using static ItemDrop;
using Object = UnityEngine.Object;

namespace IKO
{
    [HarmonyPatch(typeof(InventoryGrid), "UpdateGui", new Type[]
    {
        typeof(Player),
        typeof(ItemData)
    })]
    public static class EaQS
    {
        public static void Finalizer(InventoryGrid __instance)
        {
            if (((Object)__instance).name == "EquipmentSlotGrid")
            {
                for (int i = 0; i < 5; i++)
                {
                    Element val = __instance.m_elements[i];
                    TMP_Text component = ((Component)val.m_go.transform.Find("binding")).GetComponent<TMP_Text>();
                    Jotunn.Logger.LogInfo(component.text);
                    component.text = "123";
                    Jotunn.Logger.LogInfo(component.text);
                }
            }
        }
    }
}
