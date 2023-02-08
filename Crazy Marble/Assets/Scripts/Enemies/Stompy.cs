using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CrazyMarble.Audio;

namespace CrazyMarble.Enemy
{
    public enum StompyState { Waiting, Dropping, Retracting }

    [RequireComponent(typeof(Rigidbody))]
    public class Stompy : MonoBehaviour
    {
        private SoundEffect _crashSound;
        private Rigidbody _rigidBody;
        private StompyState _state = StompyState.Waiting;
        [SerializeField]
        private BoxCollider _stompBox;
        [SerializeField]
        private float _shakeRate = 1f;
        [SerializeField]
        private float _shakeScale = 5f;
        private float _startTransitionTime;
        [SerializeField]
        private Vector3 _startPosition;
        [SerializeField]
        private Vector3 _endPosition;
        [SerializeField]
        private Transform _bottomCenter;        
        [field: SerializeField]
        public float ShakeDistanceFromMarble { get; private set; } = 5f;
        [field: SerializeField]
        public float DropDistanceFromMarble { get; private set; } = 3.5f;
        [field: SerializeField]
        public float StompDelay { get; private set; } = 1f;
        [field: SerializeField]
        public float RetractSeconds { get; private set; } = 3f;
        [field: SerializeField]
        public float DropSeconds { get; private set; } = 0.5f;

        public void Awake() {
            _crashSound = GetComponentInChildren<SoundEffect>();
            _rigidBody = GetComponent<Rigidbody>();
            _startPosition = transform.position;
            _endPosition = transform.position;
            Physics.Raycast(_bottomCenter.position, Vector3.down, out RaycastHit hit);
            _endPosition.y -= hit.distance;
        }

        public void FixedUpdate() {
            Action method = _state switch {
                StompyState.Dropping => LerpDropPosition,
                StompyState.Retracting => LerpRetractingPosition,
                StompyState.Waiting => HandleWait,
                _ => throw new Exception("Ooops!"),
            };
            method.Invoke();
        }

        private void LerpDropPosition()
        {
            float progress = Mathf.Clamp01((Time.time - _startTransitionTime) / DropSeconds);
            _rigidBody.MovePosition(Vector3.Lerp(_startPosition, _endPosition, progress));
        }

        private void LerpRetractingPosition()
        {
            float progress = Mathf.Clamp01((Time.time - _startTransitionTime) / RetractSeconds);
            _rigidBody.MovePosition(Vector3.Lerp(_endPosition, _startPosition, progress));
        }

        private void HandleWait()
        {
            if (_state != StompyState.Waiting) { return; }
            float distance = DistanceFrom(MarbleEntity.Instance);
            _rigidBody.velocity = new Vector3();
            if (distance <= DropDistanceFromMarble)
            {
                StartCoroutine(Drop());
            }
            else if (distance <= ShakeDistanceFromMarble)
            {
                float sine =  Mathf.Sin(Time.time * _shakeRate) * _shakeScale;
                Vector3 scale = Vector3.up * sine;                
                _rigidBody.MovePosition(_startPosition + scale);
            }
            else
            {
                _rigidBody.MovePosition(_startPosition);
            }
        }

        public void CheckSquashMarble(Collider other) 
        {
            if (other.attachedRigidbody == null) { return; }
            MarbleEntity marble = other.attachedRigidbody.GetComponent<MarbleEntity>();
            marble?.Kill();
        }

        public float DistanceFrom(MarbleEntity marble)
        {
            Vector3 marblePosition = marble.RigidBody.position;
            marblePosition.y = 0;
            Vector3 stompyPosition = _bottomCenter.position;
            stompyPosition.y = 0;
            return Vector3.Distance(marblePosition, stompyPosition);
        }

        private IEnumerator Drop()
        {
            if (_state == StompyState.Waiting)
            {
                _stompBox.gameObject.SetActive(true);
                _state = StompyState.Dropping;
                _startTransitionTime = Time.time;
                yield return new WaitForSeconds(DropSeconds);
                _crashSound.Play();
                GameCamera.CurrentCamera.Shake(.5f, 5);
                yield return new WaitForSeconds(StompDelay);
                _stompBox.gameObject.SetActive(false);
                _state = StompyState.Retracting;
                _startTransitionTime = Time.time;
                yield return new WaitForSeconds(RetractSeconds);

                _state = StompyState.Waiting;
            }
        }
    }
}