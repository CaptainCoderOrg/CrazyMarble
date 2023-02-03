using UnityEngine;
using static UnityEngine.InputSystem.InputAction;

namespace CrazyMarble
{
    [RequireComponent(typeof(MarbleControls), typeof(MarbleEntity))]
    public class MarbleMovementController : MonoBehaviour
    {
        private MarbleControls _marbleController;
        private MarbleEntity _entity;
        [field: SerializeField]
        public Camera MainCamera { get; private set; }
        [SerializeField]
        public Vector2 InputDirection { get; private set; } = Vector2.zero;
        [field: SerializeField]
        public float RotationPower { get; private set; } = 5f;

        protected void Awake()
        {
            MarbleControls controls = GetComponent<MarbleControls>();
            controls.UserInput.MarbleControls.Movement.performed += HandleMovement;
            controls.UserInput.MarbleControls.Movement.canceled += StopMovement;
            _entity = GetComponent<MarbleEntity>();
        }

        protected void FixedUpdate()
        {
            Vector3 torqueDirection = new(InputDirection.y, 0, -InputDirection.x);
            _entity.RigidBody.AddTorque(torqueDirection * RotationPower, ForceMode.Force);
        }

        private void HandleMovement(CallbackContext context)
        {
            Vector2 rawInput = context.ReadValue<Vector2>();
            Vector3 cameraRightAngles = MainCamera.transform.right;
            Vector3 cameraForwardAngles = MainCamera.transform.forward;
            cameraForwardAngles.y = 0;
            cameraForwardAngles.Normalize();
            
            Vector2 forward2D = new (cameraForwardAngles.x, cameraForwardAngles.z);
            Vector2 right2D = new (cameraRightAngles.x, cameraRightAngles.z);

            // Debug.DrawRay(transform.position, cameraRightAngles * 3, Color.blue, 5);
            // Debug.DrawRay(transform.position, cameraForwardAngles * 3, Color.yellow, 5);
            
            forward2D = forward2D * rawInput.y;
            right2D = right2D * rawInput.x;
            InputDirection = forward2D + right2D;
        }
        private void StopMovement(CallbackContext context) => InputDirection = Vector2.zero;
    }
}