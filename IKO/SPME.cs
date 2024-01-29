using System.Collections.Generic;
using HarmonyLib;
using UnityEngine;
using static MessageHud;

namespace IKO
{
    [HarmonyPatch(typeof(Player), "Message")]
    public static class SPME
    {
        private static readonly Dictionary<string, string> lang = LangYml.GetLocalList("SnapPointsMadeEasy");

        public static bool Prefix(string msg)
        {
            if (msg.Contains("Snap Points Made Easy"))
            {
                foreach (KeyValuePair<string, string> row in lang)
                {
                    if (msg.Contains(row.Key))
                    {
                        ((Character)Player.m_localPlayer).Message((MessageType)1, msg.Replace(row.Key, row.Value), 0, (Sprite)null);
                    }
                }
                return false;
            }
            return true;
        }
    }
}
