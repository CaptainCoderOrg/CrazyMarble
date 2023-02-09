using System.Collections;
using UnityEngine;
using CrazyMarble.Audio;

namespace CrazyMarble.Hazard
{
    [RequireComponent(typeof(Rigidbody))]
    public class Pusher : MonoBehaviour
    {
        private SoundEffect _soundEffect;
        private GameObject _crusher;
        private Rigidbody _rigidBody;
        private float _startAnimationTime;
        private bool _isExtending;
        private Vector3 _retractedPosition;
        private Vector3 ExtendedPosition => _retractedPosition + -transform.forward * ExtendDistance;

        [field: SerializeField]
        public float StartDelaySeconds { get; private set; } = 0f;
        [field: SerializeField]
        public float ExtendDistance { get; private set; } = 2.5f;
        [field: SerializeField]
        public float PauseSeconds { get; private set; } = 2f;
        [field: SerializeField]
        public float Speed { get; private set; } = .1f;

        public void Awake()
        {
            _soundEffect = GetComponentInChildren<SoundEffect>();
            Debug.Assert(_soundEffect != null, "Could not find Sound Effect on Pusher");
            _crusher = GetComponentInChildren<InstantDeathTrigger>()?.gameObject;
            Debug.Assert(_crusher != null, "Could not find Crusher on Pusher");
            _rigidBody = GetComponent<Rigidbody>();
            Debug.Assert(_rigidBody != null, "Could not find rigid body on Pusher.");
            _retractedPosition = transform.position;
            
            _crusher.SetActive(false);
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
                _soundEffect?.Play();
                Retract();
                yield return new WaitForSeconds(PauseSeconds);
                _soundEffect?.Play();
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
            _crusher?.SetActive(true);
        }

    }
}