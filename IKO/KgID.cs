using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using HarmonyLib;
using kg_ItemDrawers;

namespace IKO
{
    [HarmonyPatch(typeof(DrawerComponent), "GetHoverText")]
    public static class KgID
    {
        private static readonly Dictionary<string, string> lang = LangYml.GetLocalList("ItemDrawers");

        public static String Postfix(String __result)
        {
            foreach (KeyValuePair<string, string> row in lang)
            {
                __result = Regex.Replace(__result, @row.Key, row.Value).Replace("Left", "Л.");
            }
            return __result;
        }
    }
}
