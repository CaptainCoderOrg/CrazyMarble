using CrazyMarble.UI;
using UnityEngine;
using static UnityEngine.InputSystem.InputAction;

namespace CrazyMarble
{
    [RequireComponent(typeof(MarbleEntity))]
    public class MarbleMovementController : MonoBehaviour
    {
        private MarbleEntity _entity;
        [field: SerializeField]
        public Camera MainCamera { get; private set; }
        [SerializeField]
        public Vector2 InputDirection { get; private set; } = Vector2.zero;
        [field: SerializeField]
        public float RotationPower { get; private set; } = 5f;

        protected void Awake()
        {
            MarbleControls.UserInput.MarbleControls.Movement.performed += HandleMovement;
            MarbleControls.UserInput.MarbleControls.Movement.canceled += StopMovement;
            _entity = GetComponent<MarbleEntity>();
        }

        protected void OnDestroy() {
            MarbleControls.UserInput.MarbleControls.Movement.performed -= HandleMovement;
            MarbleControls.UserInput.MarbleControls.Movement.canceled -= StopMovement;
        }

        protected void FixedUpdate()
        {
            Vector3 torqueDirection = new(InputDirection.y, 0, -InputDirection.x);
            _entity.RigidBody.AddTorque(torqueDirection * RotationPower, ForceMode.Force);
        }

        private void HandleMovement(CallbackContext context)
        {
            HUD.ClearStageName();
            LevelTimer.Instance.StartTimer();
            Vector2 rawInput = context.ReadValue<Vector2>();
            Quaternion cameraRotation = MainCamera.transform.rotation;
            Vector3 inputDirection = cameraRotation * new Vector3(rawInput.x, 0, rawInput.y);
            InputDirection = new Vector2(inputDirection.x, inputDirection.z);
        }

        private void StopMovement(CallbackContext context) => InputDirection = Vector2.zero;
    }
}