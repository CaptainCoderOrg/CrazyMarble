using System.Collections;
using CaptainCoder.Audio;
using CrazyMarble.Input;
using UnityEngine;
using UnityEngine.SceneManagement;


namespace CrazyMarble.UI
{
    public class OptionsScreenController : MonoBehaviour
    {
        [SerializeField]
        private UnityEngine.UI.Slider _musicVolumeSlider;
        [SerializeField]
        private UnityEngine.UI.Slider _sfxVolumeSlider;
        [SerializeField]
        private UnityEngine.UI.Slider _mouseSensitivitySlider;
        [SerializeField]
        private UnityEngine.UI.Toggle _invertYAxisToggle;
        [SerializeField]
        private UnityEngine.UI.Toggle _invertXAxisToggle;
        [SerializeField]
        private GameObject[] _hiddenIfMainMenu;
        public float MusicVolume
        {
            get => MusicController.Instance.MusicVolume;
            set => MusicController.Instance.MusicVolume = value;
        }
        public float SFXVolume
        {
            get => SFXController.Volume;
            set => SFXController.Volume = value;
        }
        public float MouseSensitivity
        {
            get => CameraControls.MouseSensitivity;
            set => CameraControls.MouseSensitivity = value;
        }
        public bool InvertXAxis
        {
            get => CameraControls.InvertXAxis;
            set => CameraControls.InvertXAxis = value;
        }
        public bool InvertYAxis
        {
            get => CameraControls.InvertYAxis;
            set => CameraControls.InvertYAxis = value;
        }

        public void Close()
        {
            AsyncOperation op = SceneManager.UnloadSceneAsync("Options");
        }

        public void MainMenu() => StartCoroutine(LoadScene("Main Menu"));

        private IEnumerator LoadScene(string scene)
        {
            AsyncOperation op = SceneManager.LoadSceneAsync(scene);
            while (!op.isDone)
            {
                yield return null;
            }
        }

        private void Awake()
        {
            Scene _mainMenu = SceneManager.GetSceneByName("Main Menu");
            if (_mainMenu.isLoaded)
            {
                foreach (GameObject go in _hiddenIfMainMenu)
                {
                    go.SetActive(false);
                }
            }
            _mouseSensitivitySlider.value = MouseSensitivity;
            _musicVolumeSlider.value = MusicVolume;
            _sfxVolumeSlider.value = SFXVolume;
            _invertXAxisToggle.isOn = InvertXAxis;
            _invertYAxisToggle.isOn = InvertYAxis;
        }

    }
}