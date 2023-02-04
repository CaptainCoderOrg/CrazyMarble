using UnityEngine;
using UnityEngine.Events;

namespace CrazyMarble
{
    public class LevelController : MonoBehaviour
    {
        [field: SerializeField]
        public UnityEvent OnLevelComplete { get; private set; }

        [SerializeField]
        private float _goalResolveTime = 3.0f;
        private float _playerEnterGoalTime;

        public void CheckGoalTriggerStart(Collider other)
        {
            if (other.attachedRigidbody.TryGetComponent<MarbleEntity>(out _))
            {
                _playerEnterGoalTime = Time.time;
            }
        }

        public void CheckGoalTriggerStay(Collider other)
        {
            if (other.attachedRigidbody.TryGetComponent<MarbleEntity>(out _))
            {
                float timeInGoal = Time.time - _playerEnterGoalTime;
                if (timeInGoal >= _goalResolveTime)
                {
                    OnLevelComplete.Invoke();
                }
            }
        }
    }
}