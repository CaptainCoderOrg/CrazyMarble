using System.Collections;
using UnityEngine;

namespace CrazyMarble.Hazard
{
    [RequireComponent(typeof(Rigidbody))]
    public class Pusher : MonoBehaviour
    {
        private Rigidbody _rigidBody;
        private float _startAnimationTime;
        private bool _isExtending;
        private Vector3 _retractedPosition;
        private Vector3 ExtendedPosition => _retractedPosition + Direction * ExtendDistance;

        [field: SerializeField]
        public float StartDelaySeconds { get; private set; } = 0f;
        [field: SerializeField]
        public float ExtendDistance { get; private set; } = 2.5f;
        [field: SerializeField]
        public float PauseSeconds { get; private set; } = 2f;
        [field: SerializeField]
        public float Speed { get; private set; } = .1f;
        [field: SerializeField]
        public Vector3 Direction { get; private set; } = new Vector3(0, 0, -1);

        public void Awake()
        {
            _rigidBody = GetComponent<Rigidbody>();
            _retractedPosition = transform.position;
            StartCoroutine(nameof(PusherLoop));
        }

        public void FixedUpdate() {
            float progress = Mathf.Clamp01((Time.time - _startAnimationTime) / Speed);
            Vector3 start = _isExtending ? _retractedPosition : ExtendedPosition;
            Vector3 end = _isExtending ? ExtendedPosition : _retractedPosition;
            Vector3 position = Vector3.Lerp(start, end, progress);
            _rigidBody.MovePosition(position);
        }

        private IEnumerator PusherLoop()
        {
            yield return new WaitForSeconds(StartDelaySeconds);
            while (true)
            {
                Extend();
                yield return new WaitForSeconds(PauseSeconds);
                Retract();
                yield return new WaitForSeconds(PauseSeconds);
            }
        }

        private void Extend()
        {
            _startAnimationTime = Time.time;
            _isExtending = true;
        }

        private void Retract()
        {
            _startAnimationTime = Time.time;
            _isExtending = false;
        }

    }
}