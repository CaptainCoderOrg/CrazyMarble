using UnityEngine;
using CaptainCoder.Audio;
using UnityEngine.SceneManagement;
using System.Collections;
using CrazyMarble.Input;

namespace CrazyMarble
{

    public class MenuScreenController : MonoBehaviour
    {
        // Start is called before the first frame update
        void Start()
        {
            GeneralControls.Initialize();
            MusicController.Instance.StartTrack(0);
        }

        public void OpenOptions() => StartCoroutine(LoadScene("Options"));
        public void StageSelect() => StartCoroutine(LoadScene("StageSelect"));

        private IEnumerator LoadScene(string scene)
        {
            AsyncOperation op = SceneManager.LoadSceneAsync(scene, LoadSceneMode.Additive);
            while (!op.isDone)
            {
                yield return null;
            }
        }
    }

}