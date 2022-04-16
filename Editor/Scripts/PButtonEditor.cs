#if UNITY_EDITOR
using Pender.UI.Widgets;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;

[CustomEditor(typeof(PButton))]
public class PButtonEditor : UnityEditor.Editor
{
    public override VisualElement CreateInspectorGUI()
    {
        var pb = target as PButton;

        var root = new VisualElement();

        var b = new Box();
        b.style.paddingBottom = b.style.paddingTop = b.style.paddingRight = b.style.paddingLeft = 5;
        var p = new PropertyField();
        p.bindingPath = "onClick";
        b.Add(p);   
        root.Add(b);

        /*var action = new EnumField(PendarClickAction.ShowPanel);
        action.label = "Action Type";
        action.bindingPath = "onClick";
        b.Add(action);*/

        /*var evt = new PropertyField();
        evt.label = "Additional OnClick Calls";
        evt.bindingPath = "onClick";
        firstFold.Add(evt);*/

        var color = new ColorField();
        color.label = "Color";
        color.bindingPath = "buttonColor";
        color.RegisterValueChangedCallback(evt => pb.targetGraphic.color = evt.newValue);
        root.Add(color);

        var text = new TextField();
        text.label = "Caption";
        text.bindingPath = "text";
        text.RegisterValueChangedCallback(evt =>
        {
            pb.targetText.text = evt.newValue;
            if (pb.autoResize)
                pb.AutoResize();
        });
        root.Add(text);

        var autoResize = new Toggle();
        autoResize.label = "Auto Resize";
        autoResize.bindingPath = "autoResize";
        autoResize.style.paddingTop = 10;
        autoResize.RegisterValueChangedCallback(evt =>
        {
            if (evt.newValue)
            {
                pb.AutoResize();
            }
            else
            {

            }
        });
        root.Add(autoResize);


        var leftPadding = new FloatField();
        var rightPadding = new FloatField();
        leftPadding.label = "Left";
        rightPadding.label = "Right";
        leftPadding.bindingPath = "padding.left";
        rightPadding.bindingPath = "padding.right";

        leftPadding.RegisterValueChangedCallback(evt =>
        {
            if (pb.autoResize)
                pb.AutoResize();
        });
        rightPadding.RegisterValueChangedCallback(evt =>
        {
            if (pb.autoResize)
                pb.AutoResize();
        });

        var l = new Label("Padding");     
        l.style.marginTop = 7;
        l.style.unityFontStyleAndWeight = FontStyle.Bold;
        root.Add(l);

        var g = new Box();
        g.style.paddingLeft = g.style.paddingRight = 6;
        g.style.paddingTop = g.style.paddingBottom = 3;
        g.Add(leftPadding);
        g.Add(rightPadding);
        root.Add(g);

        return root;
    }
}
#endif