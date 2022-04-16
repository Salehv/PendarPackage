using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Pender.UI.Widgets
{
    [RequireComponent(typeof(Image))]
    public class PButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
    {
        [SerializeField]

        public PUIEvent onClick;

        public Color buttonColor;
        public string text;
        public bool autoResize;
        public HPadding padding;
        
        [Space]
        public Image targetGraphic;
        public TMP_Text targetText;

        [Space]
        [SerializeField] private Sprite defaultSprite;
        [SerializeField] private Sprite pressedSprite;


        public void OnPointerDown(PointerEventData eventData)
        {
            targetGraphic.sprite = pressedSprite;
            ((RectTransform)targetText.transform).anchoredPosition += new Vector2(0, -13);
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            targetGraphic.sprite = defaultSprite;
            ((RectTransform)targetText.transform).anchoredPosition += new Vector2(0, 13);
        }

        public void AutoResize()
        {
            var size = ((RectTransform)transform).sizeDelta;

            size.x = targetText.preferredWidth + padding.left + padding.right;
            ((RectTransform)transform).sizeDelta = size;

            targetText.rectTransform.sizeDelta = new Vector2(targetText.preferredWidth, targetText.rectTransform.sizeDelta.y);
            targetText.rectTransform.anchoredPosition = new Vector2((padding.right - padding.left) / 2, 7.5f);
        }
    }

    [Serializable]
    public struct HPadding
    {
        public float right;
        public float left;
    }
}