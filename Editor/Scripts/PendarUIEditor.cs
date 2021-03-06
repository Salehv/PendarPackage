using System;
using System.Collections.Generic;
using System.IO;
using Editor.Scripts.EditorExtensions;
using Pender.UI;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;

namespace PendarCo.Scripts.Editor
{
    [CustomEditor(typeof(PendarUISettings))]
    public class PendarUIEditor : UnityEditor.Editor
    {
        private Dictionary<string, CustomToggle> _toggles;

        private VisualElement content;

        public override VisualElement CreateInspectorGUI()
        {
            SelectSettings();
            _toggles = new Dictionary<string, CustomToggle>();

            var root = Resources.Load<VisualTreeAsset>("Pendar/Settings/PendarUI/tree").CloneTree();
            root.styleSheets.Add(Resources.Load<StyleSheet>("Pendar/Settings/PendarUI/style"));

            var logo = root.Q<Image>("Logo");
            logo.image = Resources.Load<Texture>("Pendar/Settings/logo");
            logo.scaleMode = ScaleMode.ScaleToFit;

            AddCustomToggle(root, "activeUI", (evt) =>
            {
                ToggleUI();
                _toggles["activeUI"].ToggleState();
            });
            _toggles["activeUI"].SetState(_ui.isUIActive);

            content = root.Q<VisualElement>("Content");
            content.style.display = _ui.isUIActive ? DisplayStyle.Flex : DisplayStyle.None;
            root.Add(content);

            return root;

        }

        private void AddCustomToggle(VisualElement root, string id, EventCallback<ClickEvent> onClick)
        {
            var toggle = new CustomToggle();
            toggle.Init(id, root);
            toggle.SetOnClick(onClick);
            toggle.SetState(false);
            _toggles.Add(id, toggle);
        }

        private void ToggleUI()
        {
            _ui.isUIActive = !_ui.isUIActive;
            content.style.display = _ui.isUIActive ? DisplayStyle.Flex : DisplayStyle.None;
        }

        #region Menu Item

        private static PendarUISettings _ui;

        [MenuItem("Pendar/UI Settings")]
        public static void SelectSettings()
        {
            Initialize();
            Selection.SetActiveObjectWithContext(_ui, null);
        }

        [InitializeOnLoadMethod]
        private static void Initialize()
        {
            _ui = Resources.Load<PendarUISettings>("Pendar/PendarUI");
            if (_ui is null)
                _ui = CreateSettings();
        }

        private static PendarUISettings CreateSettings()
        {
            if (!Directory.Exists($"{Application.dataPath}/Resources"))
                Directory.CreateDirectory($"{Application.dataPath}/Resources");

            if (!Directory.Exists($"{Application.dataPath}/Resources/Pendar"))
                Directory.CreateDirectory($"{Application.dataPath}/Resources/Pendar");

            AssetDatabase.Refresh();

            var obj = CreateInstance<PendarUISettings>();

            AssetDatabase.CreateAsset(obj, "Assets/Resources/Pendar/PendarUI.asset");
            AssetDatabase.SaveAssets();
            return obj;
        }

        #endregion
    }
}
