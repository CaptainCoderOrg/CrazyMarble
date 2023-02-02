using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CrazyMarble.Input;
using static UnityEngine.InputSystem.InputAction;

[RequireComponent(typeof(Rigidbody))]
public class MarbleController : MonoBehaviour
{
    private static MarbleInputs s_marbleInputs;
    public static MarbleInputs MarbleInputs
    {
        get
        {
            if (s_marbleInputs == null)
            {
                s_marbleInputs = new MarbleInputs();
            }
            return s_marbleInputs;
        }
    }

    private Rigidbody _rigidBody;
    [field: SerializeField]
    private bool _isBoosting = false;
    [field: SerializeField]
    private float _boostFuel = 2.5f;

    [field: SerializeField]
    private float _jumpDistance = .75f;
    [SerializeField]
    private Vector2 _inputDirection = Vector2.zero;

    [field: SerializeField]
    private float _baseRotationPower = 5f;
    public float RotationPower
    {
        get
        {
            float multiplier = IsBoosting && BoostFuel > 0 ? BoostRotationBonus : 1;
            return _baseRotationPower * multiplier;
        }
    }

    [field: SerializeField]
    public float BoostRotationBonus { get; private set; } = 2.5f;
    [field:SerializeField]
    public float BoostForce { get; private set; } = 1f;
    public float BoostFuel 
    { 
        get => _boostFuel; 
        private set => _boostFuel = Mathf.Clamp(value, 0, BoostFuelMax); 
    }
    [field: SerializeField]
    public float BoostFuelMax { get; private set; } = 2.5f;
    public bool IsBoosting => _isBoosting && BoostFuel > 0;

    [field: SerializeField]
    public float JumpPower { get; private set; } = 5f;    

    public bool IsOnGround
    {
        get
        {
            RaycastHit hitInfo;
            bool isHit = Physics.Raycast(transform.position, Vector3.down, out hitInfo, _jumpDistance);
            // Debug.DrawRay(transform.position, Vector3.down * _jumpDistance, Color.red, 5);
            return isHit;
        }
    }

    public void ApplyRotationalForce(Vector3 direction)
    {
        _rigidBody.AddTorque(direction, ForceMode.Force);
    }

    public void ApplyDirectionalForce(Vector3 direction)
    {
        _rigidBody.AddForce(direction, ForceMode.Acceleration);
    }

    protected void Awake()
    {
        MarbleInputs.MarbleControls.Movement.performed += HandleMovement;
        MarbleInputs.MarbleControls.Movement.canceled += StopMovement;
        MarbleInputs.MarbleControls.Hop.started += HandleHop;
        MarbleInputs.MarbleControls.Boost.performed += HandleBoost;
        MarbleInputs.MarbleControls.Boost.canceled += StopBoost;
        _rigidBody = GetComponent<Rigidbody>();
    }

    protected void Update()
    {
        ConsumeFuel();
    }

    private void ConsumeFuel()
    {
        if (IsBoosting)
        {
            BoostFuel -= Time.deltaTime;
        }
    }

    protected void FixedUpdate()
    {
        Vector3 torqueDirection = new (_inputDirection.y, 0, -_inputDirection.x);
        ApplyRotationalForce(torqueDirection * RotationPower);
        if (IsBoosting)
        {
            Vector3 direction = new (_inputDirection.x, 0, _inputDirection.y);
            ApplyDirectionalForce(direction * BoostForce);
        }
    }

    protected void OnEnable()
    {
        MarbleInputs.Enable();
    }

    protected void OnDisable()
    {
        MarbleInputs.Disable();
    }

    private void HandleMovement(CallbackContext context) => _inputDirection = context.ReadValue<Vector2>();
    private void StopMovement(CallbackContext context) => _inputDirection = Vector2.zero;
    private void HandleBoost(CallbackContext context) => _isBoosting = true;
    private void StopBoost(CallbackContext context) => _isBoosting = false;
    private void HandleHop(CallbackContext context)
    {
        if (!IsOnGround) { return; }
        Vector3 velocity = _rigidBody.velocity;
        velocity.y = 0;
        _rigidBody.velocity = velocity;
        _rigidBody.AddForce(Vector3.up * JumpPower, ForceMode.VelocityChange);
    }
}
