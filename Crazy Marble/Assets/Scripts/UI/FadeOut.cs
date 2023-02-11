using UnityEngine;
using UnityEngine.UI;

namespace CrazyMarble.UI
{
    [RequireComponent(typeof(Image))]
    public class FadeOut : MonoBehaviour
    {
        [SerializeField]
        private AnimationCurve _animationCurve;
        [SerializeField]
        private float _fadeSeconds = 1f;
        private bool _started = false;
        private float _runTime = 0f;
        private Image _image;

        internal void Awake()
        {
            _image = GetComponent<Image>();
            Debug.Assert(_image != null);
        }

        internal void Update()
        {
            if (!_started)
            {
                _runTime = 0;
                _started = true;
            }
            else
            {
                _runTime += Time.deltaTime;   
            }
            float progress = _animationCurve.Evaluate(Mathf.Clamp01(_runTime / _fadeSeconds));
            _image.color = new Color(0, 0, 0, 1 - progress);
            if (progress >= 1) { gameObject.SetActive(false); }
        }
    }
}
