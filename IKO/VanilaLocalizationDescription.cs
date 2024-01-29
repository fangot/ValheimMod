using System;
using System.Collections.Generic;
using Jotunn.Managers;
using System.Reflection;
using UnityEngine;

namespace IKO
{
    public class VanilaLocalizationDescription : Localy<VanilaLocalizationDescription>
    {
        public static void Update(KeyValuePair<string, string> row)
        {
            string prefabName = row.Key.ToString();
            string text = row.Value.ToString();
            try
            {
                GameObject gameObj = PrefabManager.Instance.GetPrefab(prefabName);
                if (gameObj != null)
                {
                    Piece obj = gameObj.GetComponent<Piece>();
                    Type type = ((object)obj).GetType();
                    FieldInfo field = type.GetField("m_description", BindingFlags.Instance | BindingFlags.Public);
                    field.SetValue(obj, text);
                }
            }
            catch (Exception ex)
            {
                Jotunn.Logger.LogError("Error: " + ex.Message);
            }
        }
    }
}
