using System;
using UnityEngine;

namespace Pender.UI
{
    public class PendarUIManager : MonoBehaviour
    {
        private PendarOverlayMenu _overlay;
        public void Init()
        {
            _overlay = Instantiate(Resources.Load<GameObject>("Pendar/UIModules/Overlay"))
                .GetComponent<PendarOverlayMenu>();
            _overlay.Init();
            
            
        }
        
        
        public void SetGold(int count)
        {
            OnGoldChanged(count);
        }

        public static void ShowWin()
        {
            
        }

        public static void ShowLose()
        {
            
        }

        public static void ShowSettings()
        {
            
        }

        public void Escape()
        {
            
        }
        
        

        #region events
        public event Action<int> GoldChanged;
        public event Action<bool> SfxChanged;
        public event Action<bool> MusicChanged;
        private void OnGoldChanged(int count) => GoldChanged?.Invoke(count);

        private void OnMuteSfx(bool isOn) => SfxChanged?.Invoke(isOn);

        private void OnMuteMusic(bool isOn) => MusicChanged?.Invoke(isOn);
        #endregion
    }
}
