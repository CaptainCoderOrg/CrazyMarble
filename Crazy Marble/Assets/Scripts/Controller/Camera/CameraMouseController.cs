using UnityEngine;
using Cinemachine;
using static UnityEngine.InputSystem.InputAction;

namespace CrazyMarble.Input
{
    [RequireComponent(typeof(CinemachineFreeLook), typeof(CameraZoom))]
    public class CameraMouseController : MonoBehaviour
    {
        public CameraZoom Zoom { get; private set; }
        public CinemachineFreeLook FreeLookCamera { get; private set; }
        public bool IsCameraMoveable { get; private set; } = false;
        [field: SerializeField]
        public float MouseRotationSpeed { get; private set; } = 1200f;
        [field: SerializeField]
        public float MouseTiltSpeed { get; private set; } = 5f;

        protected void Awake()
        {
            FreeLookCamera = GetComponent<CinemachineFreeLook>();
            Zoom = GetComponent<CameraZoom>();
        }

        protected void OnEnable()
        {
            CameraControls.UserInput.StartCameraRotation.started += EnableMouseRotation;
            CameraControls.UserInput.StartCameraRotation.canceled += DisableMouseRotation;
            CameraControls.UserInput.ZoomScroll.performed += HandleZoom;
        }
        protected void OnDisable()
        {
            CameraControls.UserInput.StartCameraRotation.started -= EnableMouseRotation;
            CameraControls.UserInput.StartCameraRotation.canceled -= DisableMouseRotation;
            CameraControls.UserInput.ZoomScroll.performed -= HandleZoom;
        }

        private void EnableMouseRotation(CallbackContext context)
        {
            FreeLookCamera.m_XAxis.m_MaxSpeed = MouseRotationSpeed * CameraControls.MouseSensitivity;
            FreeLookCamera.m_YAxis.m_MaxSpeed = MouseTiltSpeed * CameraControls.MouseSensitivity;
            FreeLookCamera.m_XAxis.m_InvertInput = CameraControls.InvertXAxis;
            FreeLookCamera.m_YAxis.m_InvertInput = CameraControls.InvertYAxis;
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }

        private void DisableMouseRotation(CallbackContext context)
        {
            FreeLookCamera.m_XAxis.m_MaxSpeed = 0;
            FreeLookCamera.m_YAxis.m_MaxSpeed = 0;
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }

        private void HandleZoom(CallbackContext context) => Zoom.ZoomAmount += System.Math.Sign(-context.ReadValue<float>());
    }
}