using UnityEngine;
using static UnityEngine.InputSystem.InputAction;

namespace CrazyMarble
{
    [RequireComponent(typeof(MarbleControls), typeof(MarbleMovementController), typeof(MarbleEntity))]
    public class MarbleBoostController : MonoBehaviour
    {
        private MarbleEntity _entity;
        private MarbleMovementController _marbleMovement;
        [field: SerializeField]
        private bool _isBoosting = false;
        [field: SerializeField]
        private float _stoppedBoostingAt = 0;
        [field: SerializeField]
        private float _fuel = 2.5f;
        [field: SerializeField]
        public float BoostRotationSpeed { get; private set; } = 2.5f;
        [field: SerializeField]
        public float BoostForce { get; private set; } = 1f;
        public float Fuel
        {
            get => _fuel;
            private set => _fuel = Mathf.Clamp(value, 0, MaxFuel);
        }
        [field: SerializeField]
        public float MaxFuel { get; private set; } = 2.5f;
        public bool IsBoosting => _isBoosting && Fuel > 0;
        public bool IsRecharging => !_isBoosting && Time.time > (_stoppedBoostingAt + RechargeDelay);

        [field: SerializeField]
        public float RechargeDelay { get; private set; } = 1f;

        protected void Awake()
        {
            MarbleControls controls = GetComponent<MarbleControls>();
            controls.UserInput.MarbleControls.Boost.performed += (_) => StartBoost();
            controls.UserInput.MarbleControls.Boost.canceled += (_) => StopBoost();

            _marbleMovement = GetComponent<MarbleMovementController>();
            _entity = GetComponent<MarbleEntity>();            
        }

        public void FixedUpdate()
        {
            if (!IsBoosting) { return; }
            Vector3 torqueDirection = new(_marbleMovement.InputDirection.y, 0, -_marbleMovement.InputDirection.x);
            _entity.RigidBody.AddTorque(torqueDirection * BoostRotationSpeed, ForceMode.Force);
            Vector3 forceDirection = new(_marbleMovement.InputDirection.x, 0, _marbleMovement.InputDirection.y);
            _entity.RigidBody.AddForce(forceDirection * BoostForce, ForceMode.Acceleration);
        }

        protected void Update()
        {
            if (IsBoosting)
            {
                Fuel -= Time.deltaTime;
            }
            else if (IsRecharging)
            {
                Fuel += Time.deltaTime;
            }
        }

        private void StartBoost() => _isBoosting = true;
        private void StopBoost()
        {
            _isBoosting = false;
            _stoppedBoostingAt = Time.time;
        }
    }
}