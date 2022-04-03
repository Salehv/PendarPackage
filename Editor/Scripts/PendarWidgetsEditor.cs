using Pender.UI.Widgets;
using UnityEditor;
using UnityEngine;

namespace Editor.Scripts
{
    public class PendarWidgetsEditor : MonoBehaviour
    {
        [MenuItem("GameObject/UI/Pendar/SimpleButton", true)]
        [MenuItem("GameObject/UI/Pendar/AdButton", true)]
        [MenuItem("GameObject/UI/Pendar/BackButton", true)]
        [MenuItem("GameObject/UI/Pendar/CurrencyButton", true)]
        [MenuItem("GameObject/UI/Pendar/TextButton", true)]
        private static bool CanCreateButton(MenuCommand cmd)
        {
            if (Selection.activeGameObject is null || Selection.count != 1)
                return false;

            var context = Selection.activeGameObject;
            
            return context.GetComponentInParent<Canvas>() != null || context.GetComponent<Canvas>() != null;
        }
        
        
        [MenuItem("GameObject/UI/Pendar/SimpleButton", priority = 1)]
        private static void CreateSimpleButton(MenuCommand cmd)
        {
            var g = new GameObject();
            g.AddComponent<PButton>();
            g.transform.SetParent((cmd.context as GameObject)?.transform);
            Selection.SetActiveObjectWithContext(g, null);
        }
        
        
        [MenuItem("GameObject/UI/Pendar/AdButton", priority = 1)]
        private static void CreateAdButton(MenuCommand cmd)
        {
            
        }
        
        
        [MenuItem("GameObject/UI/Pendar/BackButton", priority = 1)]
        private static void CreateBackButton(MenuCommand cmd)
        {
            
        }
        
        
        [MenuItem("GameObject/UI/Pendar/CurrencyButton", priority = 1)]
        private static void CreateCurrencyButton(MenuCommand cmd)
        {
            
        }
        
        
        [MenuItem("GameObject/UI/Pendar/TextButton", priority = 1)]
        private static void CreateTextButton(MenuCommand cmd)
        {
            
        }
    }
}
