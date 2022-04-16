using Pender;
using Pender.UI.Widgets;
using UnityEditor;
using UnityEngine;

namespace Editor.Scripts
{
    public class PendarWidgetsEditor : MonoBehaviour
    {
        #region Panels
        [MenuItem("GameObject/UI/Pendar/Custom Panel", priority = -3)]
        private static void NewPanel(MenuCommand cmd)
        {
            var g = Instantiate(Resources.Load<GameObject>($"Pendar/UIModules/CustomPanel"));
            g.name = "Panel";
            GameObjectUtility.SetParentAndAlign(g, cmd.context as GameObject);
            Selection.activeObject = g;
        }

        #endregion

        #region Buttons
        [MenuItem("GameObject/UI/Pendar/SimpleButton", true)]
        [MenuItem("GameObject/UI/Pendar/AdButton", true)]
        [MenuItem("GameObject/UI/Pendar/BackButton", true)]
        [MenuItem("GameObject/UI/Pendar/CurrencyButton", true)]
        [MenuItem("GameObject/UI/Pendar/TextButton", true)]
        [MenuItem("GameObject/UI/Pendar/CloseButton", true)]
        [MenuItem("GameObject/UI/Pendar/Custom Panel", true)]

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

        [MenuItem("GameObject/UI/Pendar/SimpleButton", priority = 1)]
        private static void CreateSimpleButton(MenuCommand cmd)
        {
            _ = CreateButton("SimpleButton", cmd.context as GameObject);
        }


        [MenuItem("GameObject/UI/Pendar/AdButton", priority = 1)]
        private static void CreateAdButton(MenuCommand cmd)
        {
            _ = CreateButton("AdButton", cmd.context as GameObject);
        }


        [MenuItem("GameObject/UI/Pendar/BackButton", priority = 1)]
        private static void CreateBackButton(MenuCommand cmd)
        {
            _ = CreateButton("BackButton", cmd.context as GameObject);
        }


        [MenuItem("GameObject/UI/Pendar/CurrencyButton", priority = 1)]
        private static void CreateCurrencyButton(MenuCommand cmd)
        {
            _ = CreateButton("CurrencyButton", cmd.context as GameObject);
        }


        [MenuItem("GameObject/UI/Pendar/TextButton", priority = 1)]
        private static void CreateTextButton(MenuCommand cmd)
        {
            _ = CreateButton("TextButton", cmd.context as GameObject);
        }

        [MenuItem("GameObject/UI/Pendar/CloseButton", priority = 1)]
        private static void CreateCloseButton(MenuCommand cmd)
        {
            _ = CreateButton("CloseButton", cmd.context as GameObject);
        }
        #endregion
    }
}
