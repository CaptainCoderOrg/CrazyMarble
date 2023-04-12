using UnityEngine;
using Cinemachine;
using static UnityEngine.InputSystem.InputAction;

namespace CrazyMarble.Input
{
    [RequireComponent(typeof(CinemachineFreeLook), typeof(CameraZoom), typeof(GameCamera))]
    public class CameraGamePadController : MonoBehaviour
    {
        private float _zoomIn = 0;
        private float _zoomOut = 0;
        private GameCamera _gameCamera;
        public CameraZoom CameraZoom { get; private set; }
        public CinemachineFreeLook FreeLookCamera { get; private set; }
        public Vector2 InputVector { get; private set; } = Vector2.zero;
        [field: SerializeField]
        public float RotationSpeed { get; private set; } = 100f;
        [field: SerializeField]
        public float TiltSpeed { get; private set; } = 3f;
        [field: SerializeField]
        public float ZoomSpeed { get; private set; } = 20f;

        protected void Update()
        {
            FreeLookCamera.m_XAxis.Value += InputVector.x * Time.deltaTime * RotationSpeed * (CameraControls.InvertXAxis ? -1 : 1);
            FreeLookCamera.m_YAxis.Value += InputVector.y * Time.deltaTime * TiltSpeed * (CameraControls.InvertYAxis ? -1 : 1);
            CameraZoom.ZoomAmount += (_zoomIn + _zoomOut) * ZoomSpeed * Time.deltaTime;
        }

        protected void Awake()
        {
            FreeLookCamera = GetComponent<CinemachineFreeLook>();
            CameraZoom = GetComponent<CameraZoom>();
            _gameCamera = GetComponent<GameCamera>();
        }

        protected void OnEnable()
        {
            CameraControls.UserInput.MoveCamera.performed += HandleCameraRotation;
            CameraControls.UserInput.MoveCamera.canceled += StopCameraRotation;
            CameraControls.UserInput.ZoomIn.performed += StartZoomIn;
            CameraControls.UserInput.ZoomIn.canceled += StopZoomIn;
            CameraControls.UserInput.ZoomOut.performed += StartZoomOut;
            CameraControls.UserInput.ZoomOut.canceled += StopZoomOut;
        }
        protected void OnDisable()
        {
            CameraControls.UserInput.MoveCamera.performed -= HandleCameraRotation;
            CameraControls.UserInput.MoveCamera.canceled -= StopCameraRotation;
            CameraControls.UserInput.ZoomIn.performed -= StartZoomIn;
            CameraControls.UserInput.ZoomIn.canceled -= StopZoomIn;
            CameraControls.UserInput.ZoomOut.performed -= StartZoomOut;
            CameraControls.UserInput.ZoomOut.canceled -= StopZoomOut;
        }

        private void StartZoomIn(CallbackContext context) => _zoomIn = -1;
        private void StopZoomIn(CallbackContext context) => _zoomIn = 0;
        private void StartZoomOut(CallbackContext context) => _zoomOut = 1;
        private void StopZoomOut(CallbackContext context) => _zoomOut = 0;
        private void HandleCameraRotation(CallbackContext context)
        {
            InputVector = context.ReadValue<Vector2>();
            _gameCamera.IsMoving = true;
        }
        private void StopCameraRotation(CallbackContext context)
        {
            InputVector = Vector2.zero;
            _gameCamera.IsMoving = false;
        }
    }
}