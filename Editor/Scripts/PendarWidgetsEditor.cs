using Pender;
using Pender.UI.Widgets;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

namespace Editor.Scripts
{
    public class PendarWidgetsEditor : MonoBehaviour
    {
        [MenuItem("GameObject/Pendar UI/Canvas - Vertical", priority = -11)]
        private static void CreateVCanvas()
        {
            CreateCanvas(1080, 2160);   
        }
        
        [MenuItem("GameObject/Pendar UI/Canvas - Horizontal", priority = -10)]
        private static void CreateHCanvas()
        {
            CreateCanvas(2160, 1080);
        }

        private static void CreateCanvas(float w, float h)
        {
            var g = new GameObject();
            g.name = "Pendar Canvas";
            var c = g.AddComponent<Canvas>();
            var sc = g.AddComponent<CanvasScaler>();
            var gr = g.AddComponent<GraphicRaycaster>();
            c.renderMode = RenderMode.ScreenSpaceOverlay;
            sc.uiScaleMode = CanvasScaler.ScaleMode.ScaleWithScreenSize;
            sc.referenceResolution = new Vector2(w, h);
        }

        #region Buttons

        [MenuItem("GameObject/Pendar UI/Canvas - Horizontal", true)]
        [MenuItem("GameObject/Pendar UI/Canvas - Vertical", true)]
        [MenuItem("GameObject/Pendar UI/Buttons/SimpleButton", true)]
        [MenuItem("GameObject/Pendar UI/Buttons/AdButton", true)]
        [MenuItem("GameObject/Pendar UI/Buttons/BackButton", true)]
        [MenuItem("GameObject/Pendar UI/Buttons/CurrencyButton", true)]
        [MenuItem("GameObject/Pendar UI/Buttons/TextButton", true)]
        [MenuItem("GameObject/Pendar UI/Buttons/CloseButton", true)]
        [MenuItem("GameObject/Pendar UI/Panels/Custom Panel", true)]
        private static bool ValidateButton()
        {
            return Pendar.UISettings.isUIActive;
        }

        private static GameObject CreateButton(string name, GameObject parent)
        {
            var g = Instantiate(Resources.Load<GameObject>($"Pendar/UIModules/Buttons/{name}"));
            g.name = name;
            GameObjectUtility.SetParentAndAlign(g, parent);
            // Register the creation in the undo system
            Undo.RegisterCreatedObjectUndo(g, "Create " + g.name);

            g.GetComponent<PButton>().AutoResize();
            Selection.activeObject = g;
            return g;
        }

        [MenuItem("GameObject/Pendar UI/Buttons/SimpleButton", priority = 0)]
        private static void CreateSimpleButton(MenuCommand cmd)
        {
            _ = CreateButton("SimpleButton", cmd.context as GameObject);
        }


        [MenuItem("GameObject/Pendar UI/Buttons/AdButton", priority = 0)]
        private static void CreateAdButton(MenuCommand cmd)
        {
            _ = CreateButton("AdButton", cmd.context as GameObject);
        }


        [MenuItem("GameObject/Pendar UI/Buttons/BackButton", priority = 0)]
        private static void CreateBackButton(MenuCommand cmd)
        {
            _ = CreateButton("BackButton", cmd.context as GameObject);
        }


        [MenuItem("GameObject/Pendar UI/Buttons/CurrencyButton", priority = 0)]
        private static void CreateCurrencyButton(MenuCommand cmd)
        {
            _ = CreateButton("CurrencyButton", cmd.context as GameObject);
        }


        [MenuItem("GameObject/Pendar UI/Buttons/TextButton", priority = 0)]
        private static void CreateTextButton(MenuCommand cmd)
        {
            _ = CreateButton("TextButton", cmd.context as GameObject);
        }

        [MenuItem("GameObject/Pendar UI/Buttons/CloseButton", priority = 0)]
        private static void CreateCloseButton(MenuCommand cmd)
        {
            _ = CreateButton("CloseButton", cmd.context as GameObject);
        }

        #endregion


        #region Panels

        [MenuItem("GameObject/Pendar UI/Panels/Custom Panel", false, 10)]
        private static void NewPanel(MenuCommand cmd)
        {
            var g = Instantiate(Resources.Load<GameObject>($"Pendar/UIModules/CustomPanel"));
            g.name = "Panel";
            GameObjectUtility.SetParentAndAlign(g, cmd.context as GameObject);
            Selection.activeObject = g;
        }

        #endregion
    }
}