using System.IO;
using Pender.ColorSettings;
using UnityEditor;
using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UIElements;
using UnityEditor.UIElements;

namespace PendarCo.Scripts.Editor
{
    [CustomEditor(typeof(PendarColorSettings))]
    public class PendarColorSettingsEditor : UnityEditor.Editor
    {
        private PopupField<string> paletteSelector;
        private VisualElement _customColorBox;
        private Toggle _customPalette;
        private ColorField _baseColor;
        private Slider _hueOffset;
        private Slider _highlight;
        private Slider _shadow;
        private VisualElement[] _paletteColors;

        public override VisualElement CreateInspectorGUI()
        {
            SelectSettings();

            var root = Resources.Load<VisualTreeAsset>("Pendar/Settings/PendarColorSettings/tree").CloneTree();
            root.styleSheets.Add(Resources.Load<StyleSheet>("Pendar/Settings/PendarColorSettings/style"));

            var logo = root.Q<Image>("Logo");
            logo.image = Resources.Load<Texture>("Pendar/Settings/logo");
            logo.scaleMode = ScaleMode.ScaleToFit;


            _highlight = root.Q<Slider>("palette-highlight");
            _highlight.bindingPath = "highlight";
            _highlight.RegisterCallback<ChangeEvent<float>>((evt) => UpdateShadows(_highlight.value, _colorSettings.shadow));

            _shadow = root.Q<Slider>("palette-shadow");
            _shadow.bindingPath = "shadow";
            _shadow.RegisterCallback<ChangeEvent<float>>((evt) => UpdateShadows(_colorSettings.highlight, _shadow.value));

            _customColorBox = root.Q<VisualElement>("CustomPalette");
            _customPalette = root.Q<Toggle>("custom-palette");
            _customPalette.bindingPath = "customPalette";
            _customPalette.RegisterCallback<ChangeEvent<bool>>((evt) => SetCustomColorPalette(evt.newValue));

            var paletteSelector = root.Q<VisualElement>("palette-selector");
            InitPaletteSelector(paletteSelector);

            InitColorBox(root);

            SetCustomColorPalette(_colorSettings.customPalette);

            var grayscale = root.Q<VisualElement>("grayscale");
            var on = grayscale.Q<VisualElement>("grayscale-on");
            var off = grayscale.Q<VisualElement>("grayscale-off");
            grayscale.RegisterCallback<ClickEvent>((evt) => ToggleGrayscale(on, off));
            off.style.display = _colorSettings.grayscale ? DisplayStyle.None : DisplayStyle.Flex;
            on.style.display = _colorSettings.grayscale ? DisplayStyle.Flex : DisplayStyle.None;

            return root;
        }

        private void ToggleGrayscale(VisualElement on, VisualElement off)
        {
            _colorSettings.grayscale = !_colorSettings.grayscale;
            off.style.display = _colorSettings.grayscale ? DisplayStyle.None : DisplayStyle.Flex;
            on.style.display = _colorSettings.grayscale ? DisplayStyle.Flex : DisplayStyle.None;
            GrayInit();
        }

        private void InitPaletteSelector(VisualElement root)
        {
            var palettes = Resources.LoadAll<PrColorPalette>("Pendar/Palettes");
            var paletteList = new List<string>();
            foreach (var p in palettes)
            {
                paletteList.Add(p.name);
            }

            paletteSelector = new PopupField<string>("Palette", paletteList, 0);
            paletteSelector.RegisterValueChangedCallback((evt) => PaletteSelected(evt.newValue));
            paletteSelector.value = _colorSettings.selectedPalette;
            paletteSelector.bindingPath = "selectedPalette";
            

            root.Add(paletteSelector);
        }

        private void PaletteSelected(string value)
        {
            _colorSettings.activePalette = Resources.Load<PrColorPalette>($"Pendar/Palettes/{value}");
            UpdateColors(_colorSettings.activePalette);
        }

        private void SetCustomColorPalette(bool show)
        {
            _customColorBox.style.display = show ? DisplayStyle.Flex : DisplayStyle.None;

            if (show)
            {
                paletteSelector.SetEnabled(false);
                UpdateColors(_colorSettings.baseColor, _colorSettings.hueOffset);
                return;
            }

            paletteSelector.SetEnabled(true);
            PaletteSelected(paletteSelector.value);
        }

        private void InitColorBox(VisualElement root)
        {
            _baseColor = root.Q<ColorField>("palette-base-color");
            _baseColor.bindingPath = "baseColor";
            _baseColor.RegisterCallback<ChangeEvent<Color>>((evt) => UpdateColors(_baseColor.value, _colorSettings.hueOffset));

            _hueOffset = root.Q<Slider>("palette-hue-offset");
            _hueOffset.bindingPath = "hueOffset";
            _hueOffset.RegisterCallback<ChangeEvent<float>>((evt) => UpdateColors(_colorSettings.baseColor, _hueOffset.value));

            _paletteColors = new VisualElement[PendarColorSettings.ColorCount];

            for (var i = 0; i < PendarColorSettings.ColorCount; i++)
            {
                _paletteColors[i] = root.Q<VisualElement>($"palette-color-{i + 1}");
            }

            UpdateColors(_colorSettings.baseColor, _colorSettings.hueOffset);
        }


        private static readonly int SIDColor = Shader.PropertyToID("_Color");
        private void UpdateColors(Color baseColor, float hueOffset)
        {
            var color = baseColor;
            for (var i = 1; i <= PendarColorSettings.ColorCount; i++)
            {
                _paletteColors[i - 1].style.backgroundColor = color;
                if(_colorSettings.regularMaterials.Length > i - 1) {
                    _colorSettings.regularMaterials[i - 1].SetColor(SIDColor, color);
                }
                

                Color.RGBToHSV(color, out float h, out float s, out float l);
                color = Color.HSVToRGB((h + (hueOffset / 100f)) % 1, s, l);
            }

        }

        private void UpdateColors(PrColorPalette palette)
        {
            for (var i = 1; i <= PendarColorSettings.ColorCount; i++)
            {
                if(_colorSettings.regularMaterials.Length > i - 1 && palette.colors.Length > i - 1) {
                    _paletteColors[i - 1].style.backgroundColor = palette.colors[i - 1];
                    _colorSettings.regularMaterials[i - 1].SetColor(SIDColor, palette.colors[i - 1]);
                }
            }

        }

        private static readonly int SIDHighlight = Shader.PropertyToID("_HColor");
        private static readonly int SIDShadow = Shader.PropertyToID("_SColor");
        private void UpdateShadows(float highlight, float shadow)
        {
            highlight /= 100f;
            shadow /= 100f;

            for (var i = 0; i < PendarColorSettings.ColorCount; i++)
            {
                _colorSettings.regularMaterials[i].SetColor(SIDHighlight, new Color(highlight, highlight, highlight, 1));
                _colorSettings.regularMaterials[i].SetColor(SIDShadow, new Color(shadow, shadow, shadow, 1));
            }
        }


        [InitializeOnLoadMethod]
        private static void GrayInit()
        {
            Initialize();

            var cam = Camera.main;
            if (cam)
            {
                if (!cam.TryGetComponent<PendarCameraSettings>(out var camSettings))
                {
                    camSettings = cam.gameObject.AddComponent<PendarCameraSettings>();
                }

                camSettings.SetGrayscale(_colorSettings.grayscale);
            }
        }


        #region Menu Item
        private static PendarColorSettings _colorSettings;
        [MenuItem("Pendar/Color Settings")]
        public static void SelectSettings()
        {
            Initialize();
            
            Selection.SetActiveObjectWithContext(_colorSettings, null);
        }

        private static void Initialize()
        {
            _colorSettings = Resources.Load<PendarColorSettings>("Pendar/PendarColorSettings");
            if (_colorSettings is null)
                _colorSettings = CreateSettings();
        }

        // TODO: Add Regular Materials and Default Palettes
        private static PendarColorSettings CreateSettings()
        {
            if (!Directory.Exists($"{Application.dataPath}/Resources"))
                Directory.CreateDirectory($"{Application.dataPath}/Resources");
                    
            if (!Directory.Exists($"{Application.dataPath}/Resources/Pendar"))
                Directory.CreateDirectory($"{Application.dataPath}/Resources/Pendar");
            
            AssetDatabase.Refresh();
            
            var obj = CreateInstance<PendarColorSettings>();
            obj.regularMaterials = Resources.LoadAll<Material>("Pendar/Materials");

            AssetDatabase.CreateAsset(obj, "Assets/Resources/Pendar/PendarColorSettings.asset");
            AssetDatabase.SaveAssets();
            return obj;
        }
        #endregion
    }
}
