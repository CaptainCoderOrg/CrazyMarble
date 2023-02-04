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

        public float TimeLeft
        {
            get => _timeLeft;
            private set
            {
                float newVal = Mathf.Max(0, value);
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

        public void ResetTimer()
        {
            TimeLeft = StartingTimeAmount;
        }

        protected void Update()
        {
            TimeLeft -= Time.deltaTime;
        }
    }
}