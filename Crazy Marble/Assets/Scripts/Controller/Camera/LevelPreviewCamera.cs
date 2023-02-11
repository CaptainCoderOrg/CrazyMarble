using Cinemachine;
using UnityEngine;

namespace CrazyMarble
{

    public class LevelPreviewCamera : MonoBehaviour
    {
        
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
            float progress = Mathf.Clamp01(_elapsed/_duration);
            _dolly.m_PathPosition = progress;
            if (progress == 1)
            {
                _cam.Priority = 0;
                gameObject.SetActive(false);
            }
        }

    }

}