using UnityEngine;
using TMPro;

namespace CrazyMarble.UI
{
    [RequireComponent(typeof(TextMeshProUGUI))]
    public class TimerCountdownRenderer : MonoBehaviour
    {
        private TextMeshProUGUI _text;

        public void Render(float timeRemaining)
        {
            _text.text = $"{(int)timeRemaining}".PadLeft(2, '0');
        }

        protected void Awake()
        {
            _text = GetComponent<TextMeshProUGUI>();
        }
    }
}