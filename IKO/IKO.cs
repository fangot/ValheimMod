using System;
using System.Reflection;
using BepInEx;
using HarmonyLib;
using Jotunn.Managers;

namespace IKO
{
    [BepInPlugin("fangot", "IKO", "0.0.1")]
    [BepInDependency(Jotunn.Main.ModGuid)]
    internal class IKO : BaseUnityPlugin
    {
        public const string PluginGUID = "fangot";
        public const string PluginName = "IKO";
        public const string PluginVersion = "0.0.1";

        private Harmony _harmony;

        private void Awake()
        {
            _harmony = Harmony.CreateAndPatchAll(Assembly.GetExecutingAssembly(), "iko.mods.equipmentandquickslots");
            PrefabManager.OnPrefabsRegistered += () => {
                try
                {
                    Translator.Update();
                }
                catch (Exception ex)
                {
                    Jotunn.Logger.LogError("Error: " + ex.Message);
                }
            };
        }
    }
}

