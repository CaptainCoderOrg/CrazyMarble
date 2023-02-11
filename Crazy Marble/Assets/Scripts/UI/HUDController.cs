using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

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
        [SerializeField]
        internal TextMeshProUGUI _infoText;
        internal GameObject _infoTextContainer;
        internal float _hideInfoTextAt;
        [SerializeField]
        internal TextFloater _stageName;
        [SerializeField]
        internal FadeOut _fadeOut;

        private void Awake()
        {
            if (HUD._controller == null)
            {
                HUD._controller = this;
                _infoTextContainer = _infoText.transform.parent.gameObject;
                _fadeOut.gameObject.SetActive(true);
                if (HUD._stageName != null)
                {
                    _stageName.SetText(HUD._stageName);
                }
            }
            else
            {
                Destroy(gameObject);
            }
        }

        public void Update()
        {
            if (_infoTextContainer.activeInHierarchy && Time.time > _hideInfoTextAt)
            {
                _infoTextContainer.gameObject.SetActive(false);
            }
        }

        public void StageSelect() => StartCoroutine(LoadScene("StageSelect"));

        private IEnumerator LoadScene(string scene)
        {
            AsyncOperation op = SceneManager.LoadSceneAsync(scene, LoadSceneMode.Additive);
            while (!op.isDone)
            {
                yield return null;
            }
        }

        private void OnDestroy() {
            HUD._controller = null;
            HUD._init = false;
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

        public static void ShowInfoText(string text, float duration)
        {
            Debug.Assert(duration > 0);
            _controller._hideInfoTextAt = Time.time + duration;
            _controller._infoText.text = text;
            _controller._infoTextContainer.SetActive(true);
        }

        public static void ClearStageName() => _controller._stageName.gameObject.SetActive(false);

        internal static string _stageName;
        public static void SetStageName(string name)
        {
            _stageName = name;
            _controller?._stageName?.SetText(name);
        }

        internal static bool _init = false;

        public static void Initialize()
        {
            if (_init) { return; }
            _init = true;
            Scene hud = SceneManager.GetSceneByBuildIndex(1);
            if (!hud.isLoaded) { SceneManager.LoadScene("HUD", LoadSceneMode.Additive); }
        }
    }
}