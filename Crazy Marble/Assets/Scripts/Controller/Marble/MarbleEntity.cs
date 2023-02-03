using System.Collections;
using UnityEngine;
using UnityEngine.Events;

namespace CrazyMarble
{
    [RequireComponent(typeof(Rigidbody))]
    public class MarbleEntity : MonoBehaviour
    {
        private Vector3 _spawnPoint;
        [SerializeField]
        private float _respawnTime = 1.44f;
        [SerializeField]
        private float _outOfBoundsDeathRange = 10f;
        private bool _isDead;

        [field: SerializeField]
        public UnityEvent OnDeath { get; private set; }
        public Rigidbody RigidBody { get; private set; }
        [field: SerializeField]
        public float GroundDistanceLength { get; private set; } = .75f;
        public bool IsOnGround
        {
            get
            {
                RaycastHit hitInfo;
                bool isHit = Physics.Raycast(transform.position, Vector3.down, out hitInfo, GroundDistanceLength);
                // Debug.DrawRay(transform.position, Vector3.down * _jumpDistance, Color.red, 5);
                return isHit;
            }
        }

        public void Kill()
        {
            OnDeath.Invoke();
            _isDead = true;
            StartCoroutine(Respawn());
        }

        public IEnumerator Respawn()
        {
            RigidBody.constraints = RigidbodyConstraints.FreezeAll;
            // TODO: Hide Model
            GameObject child = GetComponentInChildren<Renderer>().gameObject;
            child.SetActive(false);
            yield return new WaitForSeconds(_respawnTime);
            RigidBody.constraints = RigidbodyConstraints.None;
            RigidBody.position = _spawnPoint;
            RigidBody.velocity = Vector3.zero;
            RigidBody.rotation = Quaternion.identity;
            child.SetActive(true);
            yield return new WaitForFixedUpdate();
            _isDead = false;
        }

        protected void Awake()
        {
            RigidBody = GetComponent<Rigidbody>();
            _spawnPoint = RigidBody.position;
            Platforms.Initialize();
        }

        protected void Update()
        {
            CheckInBounds();
        }

        private void CheckInBounds()
        {
            if (_isDead) { return; }
            if ((RigidBody.position.y + _outOfBoundsDeathRange) < Platforms.MinY)
            {
                Kill();
            }
        }
    }
}