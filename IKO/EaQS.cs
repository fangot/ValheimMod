using System;
using System.Collections.Generic;
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
        private static Dictionary<string, string> lang = LangYml.GetLocalList("EquipmentSlotGrid");

        public static void Finalizer(InventoryGrid __instance)
        {
            if (((Object)__instance).name == "EquipmentSlotGrid")
            {
                for (int i = 0; i < lang.Count; i++)
                {
                    Element val = __instance.m_elements[i];
                    TMP_Text component = ((Component)val.m_go.transform.Find("binding")).GetComponent<TMP_Text>();

                    foreach (KeyValuePair<string, string> row in lang)
                    {
                        if (row.Key == component.text)
                        {
                            component.text = row.Value;
                        }
                    }
                }
            }
        }
    }
}
