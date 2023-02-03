using UnityEngine;
using static UnityEngine.InputSystem.InputAction;

namespace CrazyMarble
{
    [RequireComponent(typeof(MarbleControls), typeof(MarbleEntity))]
    public class MarbleMovementController : MonoBehaviour
    {
        private MarbleControls _marbleController;
        private MarbleEntity _entity;
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

        private void HandleMovement(CallbackContext context) => InputDirection = context.ReadValue<Vector2>();
        private void StopMovement(CallbackContext context) => InputDirection = Vector2.zero;
    }
}