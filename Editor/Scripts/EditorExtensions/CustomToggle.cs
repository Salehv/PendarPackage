using Pender.UI.Widgets;
using UnityEngine;
using UnityEngine.UIElements;

namespace Editor.Scripts.EditorExtensions
{
    public class CustomToggle
    {
        private string _id;
        private VisualElement _baseElement;
        private VisualElement _on;
        private VisualElement _off;

        private bool _state;
        
        public void Init(string id, VisualElement root)
        {
            _id = id;
            _baseElement = root.Q<VisualElement>(_id);
            _on = _baseElement.Q<VisualElement>($"{_id}-on");
            _off = _baseElement.Q<VisualElement>($"{_id}-off");
        }
        
        public void SetOnClick(EventCallback<ClickEvent> onClick)
        {
            _baseElement.RegisterCallback(onClick);
        }
        
        public void SetState(bool state)
        {
            _state = state;
            _off.style.display = state ? DisplayStyle.None : DisplayStyle.Flex;
            _on.style.display = state ? DisplayStyle.Flex : DisplayStyle.None;
        }

        public void ToggleState()
        {
            SetState(!_state);
        }
    }
}
