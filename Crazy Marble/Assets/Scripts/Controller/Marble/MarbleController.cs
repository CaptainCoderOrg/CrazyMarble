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
    [SerializeField]
    private Vector2 _inputDirection = Vector2.zero;
    
    [field: SerializeField]
    public float RotationPower { get; private set; } = 5f;
    [field: SerializeReference]
    public float JumpPower { get; private set; } = 5f;

    public float _jumpDistance = .55f;

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

    public void ApplyDirectionalForce(Vector3 direction)
    {
        _rigidBody.AddTorque(direction, ForceMode.Force);
    }

    protected void Awake()
    {
        MarbleInputs.MarbleControls.Movement.performed += HandleMovement;
        MarbleInputs.MarbleControls.Movement.canceled += StopMovement;
        MarbleInputs.MarbleControls.Hop.started += HandleHop;
        _rigidBody = GetComponent<Rigidbody>();
    }

    protected void FixedUpdate()
    {
        ApplyDirectionalForce(new Vector3(_inputDirection.y, 0, -_inputDirection.x) * RotationPower);
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
    private void HandleHop(CallbackContext context)
    {
        if (!IsOnGround) { return; }
        Vector3 velocity = _rigidBody.velocity;
        velocity.y = 0;
        _rigidBody.velocity = velocity;
        _rigidBody.AddForce(Vector3.up * JumpPower, ForceMode.VelocityChange);
    }
}
