using System;
using System.Collections.Generic;
using Pender.UI.Widgets;
using UnityEngine;

namespace Pender.UI
{
    public class PendarUIManager : MonoBehaviour
    {
        private PendarOverlayMenu _overlay;

        public void Init()
        {
            activePanels = new List<PPanel>();
            registeredPanels = new Dictionary<string, PPanel>();


            _overlay = Instantiate(Resources.Load<GameObject>("Pendar/UIModules/Overlay"))
                .GetComponent<PendarOverlayMenu>();
            _overlay.Init();
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Escape))
                Escape();
        }

        public void SetGold(int count)
        {
            OnGoldChanged(count);
        }

        private List<PPanel> activePanels;
        private Dictionary<string, PPanel> registeredPanels;

        public void ShowPanel(string panelName)
        {
            if (!registeredPanels.ContainsKey(panelName))
                throw new ArgumentException(
                    $"No panel with name {panelName} exists! make sure you typed name correctly.");

            var panel = registeredPanels[panelName];
            panel.SetVisibility(true);
            activePanels.Add(panel);
            MovePanelToTop(panel);
        }

        private void MovePanelToTop(PPanel panel)
        {
            panel.transform.SetAsLastSibling();
        }

        public bool Escape()
        {
            if (activePanels.Count == 0)
                return false;

            activePanels[activePanels.Count - 1].SetVisibility(false);
            activePanels.RemoveAt(activePanels.Count - 1);
            return true;
        }


        #region events

        public event Action<int> GoldChanged;
        public event Action<bool> SfxChanged;
        public event Action<bool> MusicChanged;
        private void OnGoldChanged(int count) => GoldChanged?.Invoke(count);

        private void OnSfxToggle(bool currentState) => SfxChanged?.Invoke(currentState);

        private void OnMusicToggle(bool currentState) => MusicChanged?.Invoke(currentState);

        #endregion

        public void RegisterPanel(PPanel pPanel)
        {
            registeredPanels.Add(pPanel.panelName, pPanel);
            pPanel.SetVisibility(pPanel.showOnStart);
        }
    }
}