using System;
using Pender.UI.Widgets;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;

[CustomPropertyDrawer(typeof(PUIAction))]
public class PUIActionEditor : PropertyDrawer
{
    public override VisualElement CreatePropertyGUI(SerializedProperty property)
    {
        var actionType = property.FindPropertyRelative("actionType");

        var root = new Box();
        root.style.paddingBottom = root.style.paddingTop = root.style.paddingRight = root.style.paddingLeft = 7;
        root.style.marginTop = root.style.marginBottom = 5;

        var showPanel = new TextField();
        showPanel.label = "Panel Name";
        showPanel.BindProperty(property.FindPropertyRelative("panelName"));

        var actype = new EnumField();
        actype.BindProperty(actionType);
        actype.label = "Action Type";
        actype.RegisterValueChangedCallback(type => 
        {
            Enum.TryParse<PUIActionType>(type.newValue?.ToString() ?? type.previousValue.ToString(), true, out var value);
            showPanel.style.display = value == PUIActionType.ShowPanel ? DisplayStyle.Flex : DisplayStyle.None;
        });
         
        root.Add(actype);
        root.Add(showPanel);

        return root;
    }
}
