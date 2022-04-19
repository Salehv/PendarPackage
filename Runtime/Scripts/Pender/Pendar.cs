using Pender.ColorSettings;
using Pender.UI;
using UnityEngine;

namespace Pender
{
    public class Pendar
    {
        private static Pendar _;
        public static readonly PendarColorSettings ColorSettings;
        public static readonly PendarUISettings UISettings;
        public static PendarUIManager ui;
        
        static Pendar()
        {
            ColorSettings = Resources.Load<PendarColorSettings>("Pendar/PendarColorSettings");
            UISettings = Resources.Load<PendarUISettings>("Pendar/PendarUI");
        }

        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
        public static void Init()
        {
            if (UISettings.isUIActive)
            {
                var g = new GameObject
                {
                    name = "UIManager"
                };
                
                ui = g.AddComponent<PendarUIManager>();
                ui.Init();
            }
        }
    }
}
