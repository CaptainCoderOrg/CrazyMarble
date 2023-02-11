using UnityEngine;
using UnityEngine.Events;
using CaptainCoder.Audio;
using CrazyMarble.Input;
using CrazyMarble.UI;
using UnityEngine.SceneManagement;
using System.Collections;
using CrazyMarble.Audio;

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
        private bool _completed = false;

        public void Awake() {
            GeneralControls.Initialize();
            HUD.Initialize();
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
                if (!_completed && timeInGoal >= _goalResolveTime)
                {
                    _completed = true;
                    HUD.StageCleared();
                    GetComponent<LevelTimer>().Pause();
                    StartCoroutine(VictoryMusic());
                    OnLevelComplete.Invoke();
                }
            }
        }

        private IEnumerator VictoryMusic()
        {
            float volume = MusicController.Instance.MusicVolume;
            MusicController.Instance.MusicVolume = 0;
            SFXController.Play(SFXDatabase.Instance.VictoryCrush);
            yield return new WaitForSeconds(1.5f);
            MusicController.Instance.MusicVolume = volume;
            MusicController.Instance.StartTrackImmediately(MusicController.Instance.VictoryTrack);
        }
    }
}