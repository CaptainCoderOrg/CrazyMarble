using UnityEngine;
using TMPro;

namespace CrazyMarble.UI
{
    [RequireComponent(typeof(TextMeshProUGUI))]
    public class LostMarblesRenderer : MonoBehaviour
    {
        private TextMeshProUGUI _text;

        public void Render(MarbleEntity entity)
        {
            _text.text = entity.LostCount.ToString();
        }

        protected void Awake()
        {
            _text = GetComponent<TextMeshProUGUI>();
        }



    }
}
