using System;
using Jotunn.Managers;
using System.Reflection;
using HarmonyLib;
using System.IO;
using BepInEx;
using System.Collections.Generic;
using YamlDotNet.Serialization;
using YamlDotNet.Serialization.NamingConventions;

namespace IKO
{
    public static class Translator
    {
        private static readonly string langPath = Path.Combine(Paths.BepInExRootPath, "plugins\\IKO\\lang.yml");

        [HarmonyPostfix]
        [HarmonyWrapSafe]
        [HarmonyPriority(int.MinValue)]
        [HarmonyPatch(typeof(ZNetScene), "Awake")]
        public static void Update()
        {
            if (!File.Exists(langPath))
            {
                return;
            }
            using StreamReader input = new StreamReader(langPath);
            IDeserializer deserializer = new DeserializerBuilder().WithNamingConvention(CamelCaseNamingConvention.Instance).Build();
            Dictionary<string, Dictionary<string, string>> dictionary = deserializer.Deserialize<Dictionary<string, Dictionary<string, string>>>(input);

            foreach (KeyValuePair<string, Dictionary<string, string>> item in dictionary)
            {
                foreach (KeyValuePair<string, string> row in item.Value)
                {
                    if (item.Key == "Hoverable")
                    {
                        SetHoverText(row);
                    }
                    if (item.Key == "Localization")
                    {
                        SetLocalization(row);
                    }
                }
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

            Jotunn.Logger.LogInfo(text);
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
                Jotunn.Logger.LogInfo(text);
                field.SetValue(obj, text);
            }
            catch (Exception ex)
            {
                Jotunn.Logger.LogError("Error: " + ex.Message);
            }
        }
    }
}