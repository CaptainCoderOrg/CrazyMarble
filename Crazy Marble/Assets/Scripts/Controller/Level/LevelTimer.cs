using CrazyMarble.UI;
using UnityEngine;
using UnityEngine.Events;

namespace CrazyMarble
{
    public class LevelTimer : MonoBehaviour
    {
        [SerializeField]
        private float _timeLeft = 0;
        [field: SerializeField]
        public UnityEvent OnTimesUp { get; private set; }
        [field: SerializeField]
        public UnityEvent<float> OnTimeChange { get; private set; }
        private bool _isPaused = false;

        public void Awake()
        {
            OnTimeChange.AddListener(HUD.RenderTimer);
        }

        public float TimeLeft
        {
            get => Mathf.Clamp(_timeLeft, 0, 99);
            private set
            {
                float newVal = Mathf.Clamp(value, 0, 99);
                if (_timeLeft == newVal) { return; }
                _timeLeft = newVal;
                OnTimeChange.Invoke(_timeLeft);
                if (_timeLeft == 0)
                {
                    OnTimesUp.Invoke();
                }
            }
        }

        [field: SerializeField]
        public float StartingTimeAmount { get; private set; } = 15;

        public void Pause() => _isPaused = true;
        public void UnPause() => _isPaused = false;

        public void ResetTimer()
        {
            TimeLeft = StartingTimeAmount;
        }

        protected void Update()
        {
            if (_isPaused) { return; }
            TimeLeft -= Time.deltaTime;
        }
    }
}