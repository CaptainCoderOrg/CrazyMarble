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

        public void BasicMovement() => SceneManager.LoadScene("00 - Basic Movement");
        public void BoostTutorial() => SceneManager.LoadScene("01 - Boost");
        public void TowerOfBullying() => SceneManager.LoadScene("02 - Tower of Bullying");
        public void StompysBridge() => SceneManager.LoadScene("03 - Stompys Bridge");
        public void JumpysDomain() => SceneManager.LoadScene("04 - Jumpys Domain");
        public void LockedOut() => SceneManager.LoadScene("05 - Locked Out");
        public void ForAghast() => SceneManager.LoadScene("EZ - Aghast");

    }
}