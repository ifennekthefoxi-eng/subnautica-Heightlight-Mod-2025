using BepInEx.Configuration;
using UnityEngine;

namespace HighlightMod
{
    public static class HighlightConfig
    {
        public static ConfigEntry<bool> HighlightFish;
        public static ConfigEntry<bool> HighlightPickupable;
        public static ConfigEntry<bool> HighlightScannable;
        public static ConfigEntry<bool> HighlightPDA;

        public static ConfigEntry<Color> FishColor;
        public static ConfigEntry<Color> PickupableColor;
        public static ConfigEntry<Color> ScannableColor;
        public static ConfigEntry<Color> PDAColor;

        public static ConfigEntry<float> HighlightOpacity;

        public static void Init(ConfigFile config)
        {
            HighlightFish = config.Bind(
                "Toggles",
                "HighlightFish",
                false,
                "Enable rim highlight for fish"
            );

            HighlightPickupable = config.Bind(
                "Toggles",
                "HighlightPickupable",
                true,
                "Enable rim highlight for pickupable items"
            );

            HighlightScannable = config.Bind(
                "Toggles",
                "HighlightScannable",
                false,
                "Enable rim highlight for scannable objects"
            );

            HighlightPDA = config.Bind(
                "Toggles",
                "HighlightPDA",
                false,
                "Enable rim highlight for PDA items"
            );

            FishColor = config.Bind(
                "Colors",
                "FishColor",
                new Color(0.2f, 1f, 0.6f),
                "Highlight color for fish"
            );

            PickupableColor = config.Bind(
                "Colors",
                "PickupableColor",
                new Color(1f, 1f, 0.3f),
                "Highlight color for pickupable items"
            );

            ScannableColor = config.Bind(
                "Colors",
                "ScannableColor",
                new Color(0.3f, 0.8f, 1f),
                "Highlight color for scannable objects"
            );

            PDAColor = config.Bind(
                "Colors",
                "PDAColor",
                new Color(1f, 0.5f, 0.9f),
                "Highlight color for PDA items"
            );

            HighlightOpacity = config.Bind(
                "General",
                "HighlightOpacity",
                0.3f,
                "Alpha of highlight effect"
            );
        }
    }
}
