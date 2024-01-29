using System;
using System.Collections.Generic;
using Jotunn.Managers;
using System.Reflection;

namespace IKO
{
    public class VanilaHoverable : Localy<VanilaHoverable>
    {
        public static void Update(KeyValuePair<string, string> row)
        {
            string prefabName = row.Key.ToString();
            string text = row.Value.ToString();
            try
            {
                Hoverable obj = PrefabManager.Instance.GetPrefab(prefabName).GetComponent<Hoverable>();
                Type type = ((object)obj).GetType();
                FieldInfo field;
                if (type == typeof(HoverText))
                {
                    field = type.GetField("m_text", BindingFlags.Instance | BindingFlags.Public);
                }
                else
                {
                    field = type.GetField("m_name", BindingFlags.Instance | BindingFlags.NonPublic);
                    field ??= type.GetField("m_name", BindingFlags.Instance | BindingFlags.Public);
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
