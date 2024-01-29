using System;
using System.Collections.Generic;

namespace IKO
{
    public class VanilaLocalization : Localy<VanilaLocalization>
    {
        public static void Update(KeyValuePair<string, string> row)
        {
            string prefabName = row.Key.ToString();
            string text = row.Value.ToString();
            try
            {
                Localization.instance.AddWord(prefabName, text);
            }
            catch (Exception ex)
            {
                Jotunn.Logger.LogError("Error: " + ex.Message);
            }
        }
    }
}
