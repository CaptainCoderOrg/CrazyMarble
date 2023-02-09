using CaptainCoder.Audio;
using CrazyMarble.Input;
using UnityEngine;
using UnityEngine.SceneManagement;


namespace CrazyMarble.UI
{
    public class StageSelectController : MonoBehaviour
    {       
        public void Close()
        {
            AsyncOperation op = SceneManager.UnloadSceneAsync("StageSelect");
        }

        public void TowerOfBullying() => SceneManager.LoadScene("Tower of Bullying");
        public void StompysBridge() => SceneManager.LoadScene("Stompys Bridge");
        public void JumpysDomain() => SceneManager.LoadScene("Jumpys Domain");
        public void LockedOut() => SceneManager.LoadScene("Locked Out");

    }
}