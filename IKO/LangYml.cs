using System.Collections.Generic;
using System.IO;
using BepInEx;
using YamlDotNet.Serialization.NamingConventions;
using YamlDotNet.Serialization;

namespace IKO
{
    public static class LangYml
    {
        private static readonly string langPath = Path.Combine(Paths.PluginPath, "IKO\\lang.yml");
        private static Dictionary<string, Dictionary<string, string>> lang;

        static LangYml()
        {
            if (!File.Exists(langPath))
            {
                return;
            }
            using StreamReader input = new StreamReader(langPath);
            IDeserializer deserializer = new DeserializerBuilder().WithNamingConvention(CamelCaseNamingConvention.Instance).Build();
            lang = deserializer.Deserialize<Dictionary<string, Dictionary<string, string>>>(input);
        }

        public static Dictionary<string, string> GetLocalList(string group)
        {
            if (lang.ContainsKey(group))
            {
                foreach (KeyValuePair<string, Dictionary<string, string>> item in lang)
                {
                    if (item.Key == group)
                    {
                        return item.Value;
                    }
                }
            }

            Jotunn.Logger.LogError("Error: " + group + " not found!");
            return new Dictionary<string, string>();
        }
    }
}
