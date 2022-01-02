using System;
using UnityEngine;

namespace Pender.ColorSettings
{
    public class PendarColorSettings : ScriptableObject
    {
        public const int ColorCount = 6;
        
        public Material[] regularMaterials;
        public PrColorPalette activePalette;
        public string selectedPalette = "DefaultPalette";
        public bool customPalette;
        public Color baseColor;
        public float hueOffset;
        public float highlight;
        public float shadow;
        public bool grayscale;

        public static Material GetSharedMaterial(PrColorId colorId)
        {
            return Pendar.ColorSettings.regularMaterials[(int) colorId];
        }
    }
}