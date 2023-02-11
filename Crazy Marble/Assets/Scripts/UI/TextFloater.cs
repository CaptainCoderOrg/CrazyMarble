using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace CrazyMarble.UI
{
    public class TextFloater : MonoBehaviour
    {
        private TextMeshProUGUI[] _letters;

        [SerializeField]
        private float _scale = 2f;
        [SerializeField]
        private float _offset = 0.1f;
        [SerializeField]
        private float _speed = 1f;
        [SerializeField]
        private string _text;
        [SerializeField]
        private TextMeshProUGUI _letterTemplate;

        public void Awake()
        {
            if (_text != null && _text != string.Empty)
            {
                SetText(_text);
            }
            else
            {
                _letters = GetComponentsInChildren<TextMeshProUGUI>();
            }
        }

        public void SetText(string text)
        {
            float spacing = 40;
            float offsetX = (-text.Length * 40) / 2;
            foreach (char ch in text)
            {
                TextMeshProUGUI letter = Instantiate(_letterTemplate, transform);
                letter.name = ch.ToString();
                letter.text = ch.ToString();
                letter.gameObject.SetActive(true);
                var pos = letter.rectTransform.localPosition;
                pos.x += offsetX;
                letter.rectTransform.localPosition = pos;
                offsetX += spacing;
            }
            _letters = GetComponentsInChildren<TextMeshProUGUI>();
        }

        // Update is called once per frame
        void Update()
        {
            for (int i = 0; i < _letters.Length; i++)
            {
                Vector3 position = _letters[i].rectTransform.localPosition;
                position.y = Mathf.Sin((Time.time + i * _offset) * _speed) * _scale;
                _letters[i].rectTransform.localPosition = position;
            }
        }
    }
}