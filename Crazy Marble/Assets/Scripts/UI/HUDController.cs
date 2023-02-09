using UnityEngine;
using UnityEngine.SceneManagement;

namespace CrazyMarble.UI
{
    public class HUDController : MonoBehaviour
    {
        [SerializeField]
        internal BoostInfoRenderer _boostInfo;
        [SerializeField]
        internal TimerCountdownRenderer _timerCountdown;
        [SerializeField]
        internal LostMarblesRenderer _lostMarbles;
        [SerializeField]
        internal GameObject _stageCleared;

        private void Awake()
        {
            HUD._controller = this;
        }
    }

    public static class HUD
    {
        internal static HUDController _controller;
        public static void RenderBoostInfo(MarbleBoostController boost)
        {
            Initialize();
            _controller?._boostInfo.Render(boost);
        }

        public static void RenderLostMarbles(MarbleEntity entity)
        {
            Initialize();
            _controller?._lostMarbles.Render(entity);
        }

        public static void RenderTimer(float remaining)
        {
            Initialize();
            _controller?._timerCountdown.Render(remaining);
        }

        public static void StageCleared()
        {
            _controller._stageCleared.SetActive(true);   
        }
        
        public static void Initialize()
        {
            Scene hud = SceneManager.GetSceneByBuildIndex(1);
            if (!hud.isLoaded) { SceneManager.LoadScene("HUD", LoadSceneMode.Additive); }
        }
    }
}