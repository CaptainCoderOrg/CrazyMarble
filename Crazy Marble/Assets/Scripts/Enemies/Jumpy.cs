using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;

namespace CrazyMarble.Enemy
{
    public enum JumpyState { Waiting, Jumping }

    public class Jumpy : MonoBehaviour
    {
        [SerializeField]
        private GameObject _sphere;
        [SerializeField]
        private Transform _waypointsContainer;
        private Rigidbody _rigidBody;
        [SerializeField]
        private List<Vector3> _waypoints;
        [SerializeField]
        private int _currentWaypoint = 0;
        private int _previousWaypoint = 0;
        [SerializeField]
        private bool _reverseOrder = false;
        [SerializeField]
        private float _jumpDuration = 1f;
        [SerializeField]
        private float _waitDuration = 2f;
        [SerializeField]
        private float _jumpHeight = 3f;
        [SerializeField]
        private AnimationCurve _jumpCurve;
        private float _transitionStartTime;
        private JumpyState _state = JumpyState.Waiting;
        

        public void Awake()
        {
            _rigidBody = GetComponentInChildren<Rigidbody>();
            _waypoints = new ();
            foreach (Transform child in _waypointsContainer)
            {
                _waypoints.Add(child.position);
            }
            Debug.Assert(_rigidBody != null, "Could not find RigidBody on child");
            
            _previousWaypoint = _currentWaypoint;
            _currentWaypoint = NextWaypoint(_currentWaypoint, _waypoints.Count);

            StartCoroutine(UpdateLoop());
        }

        public void FixedUpdate() {
            Action method = _state switch {
                JumpyState.Waiting => LerpWaitPosition,
                JumpyState.Jumping => LerpJumpPosition,
                _ => throw new Exception("Ooops!"),
            };
            method.Invoke();
        }

        private void LerpWaitPosition()
        {
            _rigidBody.MovePosition(_waypoints[_currentWaypoint]);
            Vector3 scale = _sphere.transform.localScale;
            scale.y = .25f;
            _sphere.transform.localScale = scale;
        }

        private void LerpJumpPosition()
        {
            float progress = Mathf.Clamp01((Time.time - _transitionStartTime) / _jumpDuration);
            Vector3 startPosition = _waypoints[_previousWaypoint];
            Vector3 endPosition = _waypoints[_currentWaypoint];
            Vector3 position = Vector3.Lerp(startPosition, endPosition, progress);
            float yOff = _jumpCurve.Evaluate(progress) * _jumpHeight;
            position.y += yOff;
            Vector3 scale = _sphere.transform.localScale;
            scale.y = _jumpCurve.Evaluate(progress) + .25f;
            _sphere.transform.localScale = scale;
            _rigidBody.MovePosition(position);
        }

        private IEnumerator UpdateLoop()
        {
            while (true)
            {
                _state = JumpyState.Waiting;
                _transitionStartTime = Time.time;
                yield return new WaitForSeconds(_waitDuration);
                _state = JumpyState.Jumping;
                _transitionStartTime = Time.time;
                _previousWaypoint = _currentWaypoint;
                _currentWaypoint = NextWaypoint(_currentWaypoint, _waypoints.Count);
                yield return new WaitForSeconds(_jumpDuration);
            }
        }

        private int NextWaypoint(int ix, int bound)
        {
            int direction = _reverseOrder ? -1 : 1;
            int next = ix + direction;
            if (next >= bound) { return 0; }
            if (next < 0) { return bound - 1; }
            return next;
        }
    }
}
