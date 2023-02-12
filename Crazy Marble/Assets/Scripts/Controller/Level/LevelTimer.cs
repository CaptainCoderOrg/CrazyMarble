using CrazyMarble.UI;
using UnityEngine;
using UnityEngine.Events;

namespace CrazyMarble
{
    public class LevelTimer : MonoBehaviour
    {
        public static LevelTimer Instance { get; private set; }
        [SerializeField]
        private float _timeElapsed = 0;
        [field: SerializeField]
        public UnityEvent<float> OnTimeChange { get; private set; }
        private bool _isPaused = true;

        public void Awake()
        {
            Instance = this;
            OnTimeChange.AddListener(HUD.RenderTimer);
        }

        public float TimeElapsed
        {
            get => _timeElapsed;
            private set
            {
                if (_timeElapsed == value) { return; }
                _timeElapsed = value;
                OnTimeChange.Invoke(_timeElapsed);
            }
        }

        public void Pause() => _isPaused = true;
        public void UnPause() => _isPaused = false;

        protected void Update()
        {
            if (_isPaused) { return; }
            TimeElapsed += Time.deltaTime;
        }
    }
}