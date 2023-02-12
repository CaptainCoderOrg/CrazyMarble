using UnityEngine;
using TMPro;

namespace CrazyMarble.UI
{
    [RequireComponent(typeof(TextMeshProUGUI))]
    public class TimerRenderer : MonoBehaviour
    {
        private TextMeshProUGUI _text;

        protected void Awake()
        {
            _text = GetComponent<TextMeshProUGUI>();
        }

        public void Render(float timeRemaining)
        {
            if (_text == null) { return; }
            int seconds = ((int)timeRemaining) % 60;
            int minutes = ((int)timeRemaining)/60;
            int millis = (int)((timeRemaining - ((int)timeRemaining))*100);
            
            string Format(int v) => $"{v}".PadLeft(2, '0');
            _text.text = $"{minutes}:{Format(seconds)}:{Format(millis)}";
        }

        
    }
}