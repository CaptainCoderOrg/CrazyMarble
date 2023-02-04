using UnityEngine;
using static UnityEngine.InputSystem.InputAction;

namespace CrazyMarble
{
    [RequireComponent(typeof(MarbleControls), typeof(MarbleEntity))]
    public class MarbleJumpController : MonoBehaviour
    {
        private MarbleEntity _entity;
        [field: SerializeField]
        public float JumpPower { get; private set; } = 5f;

        protected void Awake()
        {
            MarbleControls controls = GetComponent<MarbleControls>();
            controls.UserInput.MarbleControls.Hop.started += TryJump;
            _entity = GetComponent<MarbleEntity>();
        }

        private void TryJump(CallbackContext context)
        {
            if (!_entity.IsOnGround) { return; }
            Vector3 velocity = _entity.RigidBody.velocity;
            velocity.y = 0;
            _entity.RigidBody.velocity = velocity;
            _entity.RigidBody.AddForce(Vector3.up * JumpPower, ForceMode.VelocityChange);
        }
    }
}