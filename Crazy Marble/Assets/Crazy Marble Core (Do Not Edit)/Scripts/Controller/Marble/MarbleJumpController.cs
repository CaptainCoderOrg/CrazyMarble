using UnityEngine;
using static UnityEngine.InputSystem.InputAction;

namespace CrazyMarble
{
    [RequireComponent(typeof(MarbleEntity))]
    public class MarbleJumpController : MonoBehaviour
    {
        private MarbleEntity _entity;
        [field: SerializeField]
        public float JumpPower { get; private set; } = 5f;

        protected void Awake()
        {
            MarbleControls.UserInput.MarbleControls.Hop.started += TryJump;
            _entity = GetComponent<MarbleEntity>();
        }

        private void OnDestroy() {
            MarbleControls.UserInput.MarbleControls.Hop.started -= TryJump;
        }

        private void TryJump(CallbackContext context)
        {
            if (!_entity.IsOnGround) { return; }
            Vector3 velocity = _entity.RigidBody.velocity;
            float diff = Mathf.Max(0, JumpPower - _entity.RigidBody.velocity.y);
            _entity.RigidBody.AddForce(Vector3.up * diff, ForceMode.VelocityChange);
        }
    }
}