using UnityEngine;
using TMPro;

namespace Pender.UI
{
    public class PendarOverlayMenu : MonoBehaviour
    {
        public TMP_Text goldText;
        public void Init()
        {
            Pendar.ui.GoldChanged += UpdateGold;
            UpdateGold(0);
        }

        private void UpdateGold(int gold)
        {
            goldText.text = gold + "";
        }
    }
}
