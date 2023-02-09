using UnityEngine;
using TMPro;

namespace CrazyMarble.UI
{
    [RequireComponent(typeof(TextMeshProUGUI))]
    public class TimerCountdownRenderer : MonoBehaviour
    {
        private TextMeshProUGUI _text;

        protected void Awake()
        {
            _text = GetComponent<TextMeshProUGUI>();
        }

        public void Render(float timeRemaining)
        {
            if (_text == null) { return; }
            _text.text = $"{(int)timeRemaining}".PadLeft(2, '0');
        }

        
    }
}