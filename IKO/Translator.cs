using System;
using Jotunn.Managers;
using System.Reflection;
using HarmonyLib;
using System.Collections.Generic;

namespace IKO
{
    public static class Translator
    {
        public static void Update()
        {
            foreach (KeyValuePair<string, string> row in LangYml.GetLocalList("Hoverable"))
            {
                SetHoverText(row);
            }
            foreach (KeyValuePair<string, string> row in LangYml.GetLocalList("Localization"))
            {
                SetLocalization(row);
            }
        }

        private static Hoverable GetHoverable(string prefabName)
        {
            try
            {
                return PrefabManager.Instance.GetPrefab(prefabName).GetComponent<Hoverable>();
            }
            catch (Exception ex)
            {
                Jotunn.Logger.LogError("Error: " + ex.Message);
                return null;
            }
        }

        private static void SetLocalization(KeyValuePair<string, string> row)
        {
            string prefabName = row.Key.ToString();
            string text = row.Value.ToString();

            Localization.instance.AddWord(prefabName, text);
        }

        private static void SetHoverText(KeyValuePair<string, string> row)
        {
            string prefabName = row.Key.ToString();
            string text = row.Value.ToString();
            try
            {
                Hoverable obj = GetHoverable(prefabName);
                Type type = ((object)obj).GetType();
                FieldInfo field;
                if (type == typeof(HoverText))
                {
                    field = type.GetField("m_text", BindingFlags.Instance | BindingFlags.Public);
                }
                else
                {
                    field = type.GetField("m_name", BindingFlags.Instance | BindingFlags.NonPublic);
                    if ((object)field == null)
                    {
                        field = type.GetField("m_name", BindingFlags.Instance | BindingFlags.Public);
                    }
                }
                field.SetValue(obj, text);
            }
            catch (Exception ex)
            {
                Jotunn.Logger.LogError("Error: " + ex.Message);
            }
        }
    }
}