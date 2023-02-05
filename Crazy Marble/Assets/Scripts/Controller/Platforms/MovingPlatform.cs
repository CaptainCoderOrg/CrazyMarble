using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class MovingPlatform : MonoBehaviour
{
    private Rigidbody _rigidBody;
    private bool _isChangingDirection;
    private float _startTime;
    private Vector3 _endPosition;
    private Vector3 _startPosition;

    [field: SerializeField]
    public Transform StartPosition { get; private set; }
    [field: SerializeField]
    public Transform EndPosition { get; private set; }
    [field: SerializeField]
    public float TravelDuration { get; private set; }
    [field: SerializeField]
    public float DelayDuration { get; private set; }

    public void Awake()
    {
        _rigidBody = GetComponent<Rigidbody>();
        _endPosition = EndPosition.position;
        _startPosition = StartPosition.position;
        _startTime = Time.time;
    }

    public void FixedUpdate()
    {
        float progress = Mathf.Clamp((Time.time - _startTime) / TravelDuration, 0, 1);
        UpdatePosition(progress);
        CheckDirectionChange(progress);
    }

    private void UpdatePosition(float progress)
    {
        Vector3 currentPosition = Vector3.Lerp(_startPosition, _endPosition, progress);
        _rigidBody.MovePosition(currentPosition);
    }

    private void CheckDirectionChange(float progress)
    {
        if (_isChangingDirection || progress < 1) { return; }
        _isChangingDirection = true;
        Invoke(nameof(ChangeDirection), DelayDuration);
    }

    private void ChangeDirection()
    {
        (_startPosition, _endPosition) = (_endPosition, _startPosition);
        _startTime = Time.time;
        _isChangingDirection = false;
    }
}