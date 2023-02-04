using UnityEngine;
using Cinemachine;

namespace CrazyMarble.Input
{
    [RequireComponent(typeof(CinemachineFreeLook))]
    public class CameraZoom : MonoBehaviour
    {
        [field: SerializeField]
        private float _zoom = 10f;
        [field: SerializeField]
        public float MinZoom { get; set; } = 10f;
        [field: SerializeField]
        public float MaxZoom { get; set; } = 20f;
        public float ZoomAmount
        {
            get => _zoom;
            set
            {
                _zoom = Mathf.Clamp(value, MinZoom, MaxZoom);
                for (int i = 0; i < 3; i++)
                {
                    FreeLookCamera.m_Orbits[i].m_Radius = _zoom;
                }
            }
        }

        public CinemachineFreeLook FreeLookCamera { get; private set; }

        protected void Awake()
        {
            FreeLookCamera = GetComponent<CinemachineFreeLook>();
        }
    }
}