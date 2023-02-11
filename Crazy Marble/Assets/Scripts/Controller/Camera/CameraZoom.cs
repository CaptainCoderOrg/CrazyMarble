using UnityEngine;
using Cinemachine;

namespace CrazyMarble.Input
{
    [RequireComponent(typeof(CinemachineFreeLook))]
    public class CameraZoom : MonoBehaviour
    {
        [field: SerializeField]
        private float _zoom = 8f;
        [field: SerializeField]
        public float MinZoom { get; set; } = 2f;
        [field: SerializeField]
        public float MaxZoom { get; set; } = 15f;
        
        private CinemachineFreeLook.Orbit _bottomRig;
        private CinemachineFreeLook.Orbit _midRig;
        private CinemachineFreeLook.Orbit _topRig;

        public float ZoomAmount
        {
            get => _zoom;
            set
            {
                _zoom = Mathf.Clamp(value, MinZoom, MaxZoom);
                FreeLookCamera.m_Orbits[0].m_Height = -_zoom;
                FreeLookCamera.m_Orbits[1].m_Radius = _zoom;
                FreeLookCamera.m_Orbits[2].m_Height = _zoom;
            }
        }

        public CinemachineFreeLook FreeLookCamera { get; private set; }

        protected void Awake()
        {
            FreeLookCamera = GetComponent<CinemachineFreeLook>();
        }
    }
}