using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[Serializable]
public class PUIEvent
{
    public List<PUIAction> actions;
    public UnityEvent customCode;
    public void OnClick()
    {
        customCode.Invoke();
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
