using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using TMPro;

namespace CrazyMarble.UI
{
    [RequireComponent(typeof(TextMeshProUGUI))]
    public class MenuOption : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerMoveHandler, IPointerClickHandler
    {
        [field: SerializeField]
        public UnityEvent OnSelect { get; private set; }
        
        [SerializeField]
        private Color _selectedColor;

        private TextMeshProUGUI _text;
        private Color _startColor;        

        public void Awake()
        {
            _text = GetComponent<TextMeshProUGUI>();
            _startColor = _text.color;
        }

        public void Highlight()
        {
            _text.color = _selectedColor;
        }

        public void Unhighlight()
        {
            _text.color = _startColor;
        }

        public void OnPointerClick(PointerEventData eventData) => OnSelect.Invoke();
        public void OnPointerEnter(PointerEventData eventData) => Highlight();

        public void OnPointerExit(PointerEventData eventData) => Unhighlight();
        public void OnPointerMove(PointerEventData eventData) => Highlight();
    }
}