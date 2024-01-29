using System.Collections.Generic;

namespace IKO
{
    public abstract class Localy<T> where T : Localy<T>
    {
        public static void SetLocalization()
        {
            foreach (KeyValuePair<string, string> row in LangYml.GetLocalList(typeof(T).Name))
            {
                var update = typeof(T).GetMethod("Update");
                update?.Invoke(null, parameters: new object[] { row });
            }
        }
    }
}
