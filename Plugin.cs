using System.Reflection;
using BepInEx;
using HarmonyLib;
using Nautilus.Handlers;
using UnityEngine;

namespace HighlightMod
{
    [BepInPlugin("com.mediccookie.highlightcategories", "Highlight Categories", "1.0.0")]
    public class Plugin : BaseUnityPlugin
    {
        public static HighlightOptions Options;

        private void Awake()
        {
            // Initialize config (creates .cfg file)
            HighlightConfig.Init(Config);

            // Register Nautilus options
            Options = new HighlightOptions();
            OptionsPanelHandler.RegisterModOptions(Options);

            // Apply Harmony patches
            Harmony harmony = new Harmony("com.mediccookie.highlightcategories");
            harmony.PatchAll(Assembly.GetExecutingAssembly());

            Logger.LogInfo("Highlight Categories loaded.");
        }
    }
}
