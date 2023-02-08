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

        public void Awake()
        {
            _letters = GetComponentsInChildren<TextMeshProUGUI>();
        }

        // Update is called once per frame
        void Update()
        {
            for (int i = 0; i < _letters.Length; i++)
            {
                Vector3 position = _letters[i].rectTransform.localPosition;
                position.y = Mathf.Sin((Time.time + i*_offset) * _speed) * _scale;
                _letters[i].rectTransform.localPosition = position;
            }
        }
    }
}