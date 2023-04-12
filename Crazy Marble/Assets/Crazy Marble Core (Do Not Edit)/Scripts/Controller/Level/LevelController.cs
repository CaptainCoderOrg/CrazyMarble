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
        public static LevelController CurrentLevel { get; private set; }
        [field: SerializeField]
        public UnityEvent OnLevelComplete { get; private set; }
        [field: SerializeField]
        public UnityEvent OnLevelStart { get; private set; }
        public bool IsStarted { get; private set; }
        [SerializeField]
        private string _stageName;

        [SerializeField]
        private float _goalResolveTime = 3.0f;
        [SerializeField]
        private int _musicTrack = 0;
        private float _playerEnterGoalTime;
        private bool _completed = false;

        public void Awake() {
            CurrentLevel = this;
            GeneralControls.Initialize();
            HUD.Initialize();
            HUD.SetStageName(_stageName);
            LevelPreviewCamera preview = FindObjectOfType<LevelPreviewCamera>();
            if (preview == null)
            {
                StartLevel();
            }
            
        }

        public void OnDestroy()
        {
            CurrentLevel = null;
            MarbleControls.UserInput.MarbleControls.Disable();
            MarbleControls.UserInput.CameraControls.Disable();
        }

        private void Start()
        {
            MusicController.Instance.StartTrack(_musicTrack);
        }

        public void StartLevel()
        {
            if (IsStarted) { return; }
            IsStarted = true;
            MarbleControls.UserInput.MarbleControls.Enable();
            MarbleControls.UserInput.CameraControls.Enable();
            OnLevelStart.Invoke();
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