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
        public bool _isStarted = false;

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

        /// <summary>
        /// Starts the timer if it has never been started before, otherwise does nothing.
        /// </summary>
        public void StartTimer()
        {
            if (_isStarted) { return; }
            _isStarted = true;
            UnPause();
        }

        protected void Update()
        {
            if (_isPaused) { return; }
            TimeElapsed += Time.deltaTime;
        }
    }
}