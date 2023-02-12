using Cinemachine;
using UnityEngine;
using static UnityEngine.InputSystem.InputAction;

namespace CrazyMarble
{

    public class LevelPreviewCamera : MonoBehaviour
    {
        [SerializeField]
        private AnimationCurve _animationCurve;
        [SerializeField]
        private float _duration = 5f;
        private CinemachineVirtualCamera _cam;
        private CinemachineTrackedDolly _dolly;
        private bool _isStarted = false;
        private float _elapsed;

        internal void Awake()
        {
            _cam = GetComponentInChildren<CinemachineVirtualCamera>();
            Debug.Assert(_cam != null);
            _dolly = _cam.GetCinemachineComponent<CinemachineTrackedDolly>();
            Debug.Assert(_dolly != null);
            MarbleControls.UserInput.GeneralControls.Enable();
            MarbleControls.UserInput.GeneralControls.Skip.performed += Skip;

        }

        private void Skip(CallbackContext context) => gameObject.SetActive(false);
        private void OnDisable() {
            MarbleControls.UserInput.GeneralControls.Skip.performed -= Skip;
        }

        internal void Update()
        {
            if (!_isStarted) 
            { 
                _elapsed = 0; 
                _isStarted = true;
            }
            else
            {
                _elapsed += Time.deltaTime;
            }
            float progress = _animationCurve.Evaluate(Mathf.Clamp01(_elapsed/_duration));
            _dolly.m_PathPosition = progress;
            if (progress == 1)
            {
                _cam.Priority = 0;
                LevelController.CurrentLevel.StartLevel();
                gameObject.SetActive(false);                
            }
        }

    }

}