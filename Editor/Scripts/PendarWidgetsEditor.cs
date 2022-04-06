using Pender.UI.Widgets;
using UnityEditor;
using UnityEngine;

namespace Editor.Scripts
{
    public class PendarWidgetsEditor : MonoBehaviour
    {
        #region Panels
        [MenuItem("GameObject/UI/Pendar/Panel", priority = -3)]
        private static void NewPanel()
        {


        }
        #endregion

        #region Buttons
        [MenuItem("GameObject/UI/Pendar/SimpleButton", true)]
        [MenuItem("GameObject/UI/Pendar/AdButton", true)]
        [MenuItem("GameObject/UI/Pendar/BackButton", true)]
        [MenuItem("GameObject/UI/Pendar/CurrencyButton", true)]
        [MenuItem("GameObject/UI/Pendar/TextButton", true)]
        private static bool ValidateButton()
        {
            return true;
        }

        private static GameObject CreateButton(string name, Transform parent)
        {
            var g = Instantiate(Resources.Load<GameObject>($"Pendar/UIModules/Buttons/{name}"));
            g.name = name;
            g.transform.SetParent(parent);
            g.GetComponent<PButton>().AutoResize();
            Selection.SetActiveObjectWithContext(g, null);
            return g;
        }

        [MenuItem("GameObject/UI/Pendar/SimpleButton", priority = 1)]
        private static void CreateSimpleButton(MenuCommand cmd)
        {
            _ = CreateButton("SimpleButton", (cmd.context as GameObject)?.transform);
        }
        
        
        [MenuItem("GameObject/UI/Pendar/AdButton", priority = 1)]
        private static void CreateAdButton(MenuCommand cmd)
        {
            _ = CreateButton("AdButton", (cmd.context as GameObject)?.transform);
        }
        
        
        [MenuItem("GameObject/UI/Pendar/BackButton", priority = 1)]
        private static void CreateBackButton(MenuCommand cmd)
        {
            _ = CreateButton("BackButton", (cmd.context as GameObject)?.transform);
        }
        
        
        [MenuItem("GameObject/UI/Pendar/CurrencyButton", priority = 1)]
        private static void CreateCurrencyButton(MenuCommand cmd)
        {
            _ = CreateButton("CurrencyButton", (cmd.context as GameObject)?.transform);
        }
        
        
        [MenuItem("GameObject/UI/Pendar/TextButton", priority = 1)]
        private static void CreateTextButton(MenuCommand cmd)
        {
            _ = CreateButton("TextButton", (cmd.context as GameObject)?.transform);
        }
        #endregion
    }
}
