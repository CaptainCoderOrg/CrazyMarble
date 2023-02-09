using UnityEngine;
using UnityEngine.Events;
using CaptainCoder.Audio;
using CrazyMarble.Input;
using CrazyMarble.UI;
using UnityEngine.SceneManagement;

namespace CrazyMarble
{
    public class LevelController : MonoBehaviour
    {
        [field: SerializeField]
        public UnityEvent OnLevelComplete { get; private set; }

        [SerializeField]
        private float _goalResolveTime = 3.0f;
        [SerializeField]
        private int _musicTrack = 0;
        private float _playerEnterGoalTime;

        public void Awake() {
            GeneralControls.Initialize();
        }

        public void Start()
        {
            MusicController.Instance.StartTrack(_musicTrack);
        }

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
                    HUD.StageCleared();
                    OnLevelComplete.Invoke();
                }
            }
        }
    }
}