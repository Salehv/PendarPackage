using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Pender.UI.Widgets
{
    [Serializable]
    public class PUIEvent
    {
        public List<PUIAction> actions;
        public UnityEvent customCode;

        public void Clicked()
        {
            foreach (var action in actions)
            {
                switch (action.actionType)
                {
                    case PUIActionType.ShowPanel:
                        try
                        {
                            Pendar.ui.ShowPanel(action.panelName);
                        }
                        catch (ArgumentException e)
                        {
                            Debug.LogError($"Show Panel Name: {action.panelName}\n{e.Message}");
                        }

                        break;

                    case PUIActionType.Escape:
                        Pendar.ui.Escape();
                        break;
                }
            }

            customCode?.Invoke();
        }
    }

    [Serializable]
    public class PUIAction
    {
        public PUIActionType actionType;
        public string panelName;
    }

    [Serializable]
    public enum PUIActionType
    {
        ShowPanel,
        Escape,
    }
}