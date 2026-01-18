using Nautilus.Options;
using UnityEngine;

namespace HighlightMod
{
    public class HighlightOptions : ModOptions
    {
        public static HighlightOptions Instance;

        public HighlightOptions() : base("Highlight Categories")
        {
            Instance = this;

            var fishToggle = ModToggleOption.Create(
                "HighlightFish",
                "Enable Fish Highlight",
                HighlightFish,
                "Toggle rim highlight for fish."
            );
            fishToggle.OnChanged += (_, e) => HighlightFish = e.Value;
            AddItem(fishToggle);

            var pickupToggle = ModToggleOption.Create(
                "HighlightPickupable",
                "Enable Pickupable Highlight",
                HighlightPickupable,
                "Toggle rim highlight for pickupable items."
            );
            pickupToggle.OnChanged += (_, e) => HighlightPickupable = e.Value;
            AddItem(pickupToggle);

            var scanToggle = ModToggleOption.Create(
                "HighlightScannable",
                "Enable Scannable Highlight",
                HighlightScannable,
                "Toggle rim highlight for scannable objects."
            );
            scanToggle.OnChanged += (_, e) => HighlightScannable = e.Value;
            AddItem(scanToggle);

            var pdaToggle = ModToggleOption.Create(
                "HighlightPDA",
                "Enable PDA Highlight",
                HighlightPDA,
                "Toggle rim highlight for PDA items."
            );
            pdaToggle.OnChanged += (_, e) => HighlightPDA = e.Value;
            AddItem(pdaToggle);

            var fishColor = ModColorOption.Create(
                "FishColor",
                "Fish Color",
                FishColor,
                false,
                "Change highlight color for fish.",
                true
            );
            fishColor.OnChanged += (_, e) => FishColor = e.Value;
            AddItem(fishColor);

            var pickupColor = ModColorOption.Create(
                "PickupableColor",
                "Pickupable Color",
                PickupableColor,
                false,
                "Change highlight color for pickupable items.",
                true
            );
            pickupColor.OnChanged += (_, e) => PickupableColor = e.Value;
            AddItem(pickupColor);

            var scanColor = ModColorOption.Create(
                "ScannableColor",
                "Scannable Color",
                ScannableColor,
                false,
                "Change highlight color for scannable objects.",
                true
            );
            scanColor.OnChanged += (_, e) => ScannableColor = e.Value;
            AddItem(scanColor);

            var pdaColor = ModColorOption.Create(
                "PDAColor",
                "PDA Color",
                PDAColor,
                false,
                "Change highlight color for PDA items.",
                true
            );
            pdaColor.OnChanged += (_, e) => PDAColor = e.Value;
            AddItem(pdaColor);

            var opacity = ModSliderOption.Create(
                "HighlightOpacity",
                "Highlight Opacity",
                HighlightOpacity,
                0.2f,
                1f,
                0.01f,
                "{0:F2}",
                1f,
                "Controls the alpha of highlights."
            );
            opacity.OnChanged += (_, e) => HighlightOpacity = e.Value;
            AddItem(opacity);

            fishToggle.OnChanged += (_, e) =>
            {
                HighlightFish = e.Value;
                ScannableHighlighterPatch.RefreshAllHighlights();
            };

            pickupToggle.OnChanged += (_, e) =>
            {
                HighlightPickupable = e.Value;
                ScannableHighlighterPatch.RefreshAllHighlights();
            };
            
            scanToggle.OnChanged += (_, e) =>
            {
                HighlightScannable = e.Value;
                ScannableHighlighterPatch.RefreshAllHighlights();
            };
            
            pdaToggle.OnChanged += (_, e) =>
            {
                HighlightPDA = e.Value;
                ScannableHighlighterPatch.RefreshAllHighlights();
            };

            fishColor.OnChanged += (_, e) =>
            {
                FishColor = e.Value;
                ScannableHighlighterPatch.RefreshAllHighlights();
            };

            pickupColor.OnChanged += (_, e) =>
            {
                PickupableColor = e.Value;
                ScannableHighlighterPatch.RefreshAllHighlights();
            };

            scanColor.OnChanged += (_, e) =>
            {
                ScannableColor = e.Value;
                ScannableHighlighterPatch.RefreshAllHighlights();
            };

            pdaColor.OnChanged += (_, e) =>
            {
                PDAColor = e.Value;
                ScannableHighlighterPatch.RefreshAllHighlights();
            };

            opacity.OnChanged += (_, e) =>
            {
                HighlightOpacity = e.Value;
                ScannableHighlighterPatch.RefreshAllHighlights();
            };

        }


        public bool HighlightFish
        {
            get => HighlightConfig.HighlightFish.Value;
            set => HighlightConfig.HighlightFish.Value = value;
        }

        public bool HighlightPickupable
        {
            get => HighlightConfig.HighlightPickupable.Value;
            set => HighlightConfig.HighlightPickupable.Value = value;
        }

        public bool HighlightScannable
        {
            get => HighlightConfig.HighlightScannable.Value;
            set => HighlightConfig.HighlightScannable.Value = value;
        }

        public bool HighlightPDA
        {
            get => HighlightConfig.HighlightPDA.Value;
            set => HighlightConfig.HighlightPDA.Value = value;
        }

        public Color FishColor
        {
            get => HighlightConfig.FishColor.Value;
            set => HighlightConfig.FishColor.Value = value;
        }

        public Color PickupableColor
        {
            get => HighlightConfig.PickupableColor.Value;
            set => HighlightConfig.PickupableColor.Value = value;
        }

        public Color ScannableColor
        {
            get => HighlightConfig.ScannableColor.Value;
            set => HighlightConfig.ScannableColor.Value = value;
        }

        public Color PDAColor
        {
            get => HighlightConfig.PDAColor.Value;
            set => HighlightConfig.PDAColor.Value = value;
        }

        public float HighlightOpacity
        {
            get => HighlightConfig.HighlightOpacity.Value;
            set => HighlightConfig.HighlightOpacity.Value = value;
        }
    }
}
