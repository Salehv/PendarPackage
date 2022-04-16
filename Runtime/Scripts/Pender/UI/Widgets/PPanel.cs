using UnityEngine;

namespace Pender.UI.Widgets
{
    public class PPanel : MonoBehaviour
    {
        public string panelName;
        public bool showOnStart;
        
        private void Start()
        {
            Pendar.ui.RegisterPanel(this);
        }
        
        

        public void SetVisibility(bool show)
        {
            gameObject.SetActive(show);
        }
    }
}
