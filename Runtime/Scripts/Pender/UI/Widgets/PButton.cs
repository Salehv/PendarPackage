using System;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Pender.UI.Widgets
{
    [RequireComponent(typeof(Image))]
    public class PButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
    {
        [SerializeField] private Color buttonColor;
        [SerializeField] private string text;

        [Space] [SerializeField] private RectOffset padding;
        [SerializeField] private Image targetGraphic;
        [SerializeField] private TMP_Text targetText;

        [Space] [SerializeField] private Sprite defaultSprite;
        [SerializeField] private Sprite pressedSprite;


        public void OnPointerDown(PointerEventData eventData)
        {
            targetGraphic.sprite = pressedSprite;
            ((RectTransform) targetText.transform).anchoredPosition += new Vector2(0, -13);
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            targetGraphic.sprite = defaultSprite;
            ((RectTransform) targetText.transform).anchoredPosition += new Vector2(0, 13);
        }

#if UNITY_EDITOR
        private void OnValidate()
        {
            targetGraphic.color = buttonColor;
            targetText.text = text;
            UnityEditor.EditorApplication.delayCall += Resize;
        }

        private void Resize()
        {
            if (this == null)
                return;

            var size = ((RectTransform) transform).sizeDelta;

            size.x = targetText.preferredWidth + padding.right + padding.left;
            size.y = targetText.preferredHeight + padding.top + padding.bottom;
            ((RectTransform) transform).sizeDelta = size;
        }
#endif
    }
}