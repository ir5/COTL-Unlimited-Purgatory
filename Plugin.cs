using BepInEx;
using BepInEx.Logging;
using HarmonyLib;

namespace UnlimitedPurgatory
{
    [BepInPlugin(PluginInfo.PLUGIN_GUID, PluginInfo.PLUGIN_NAME, PluginInfo.PLUGIN_VERSION)]
    [HarmonyPatch]
    public class Plugin : BaseUnityPlugin
    {
        internal static ManualLogSource Log;
        internal readonly static Harmony Harmony = new(PluginInfo.PLUGIN_GUID);

        private void Awake()
        {
            // Plugin startup logic
            Logger.LogInfo($"Plugin {PluginInfo.PLUGIN_GUID} is loaded!");
            Plugin.Log = base.Logger;
            Harmony.PatchAll();
        }

        [HarmonyPatch(typeof(Interaction_EnterEndlessMode), nameof(Interaction_EnterEndlessMode.OnBecomeCurrent))]
        [HarmonyPrefix]
        public static bool Interaction_EnterEndlessMode_OnBecomeCurrent_Prefix()
        {
            DataManager.Instance.EndlessModeOnCooldown = false;
            Plugin.Log.LogInfo("Cooldown is false");
            return true;
        }
    }
}
