using System.Collections;
using UnityEngine;
using UnityEngine.Events;

namespace CrazyMarble
{
    [RequireComponent(typeof(MarbleController), typeof(Rigidbody))]
    public class PlayerEntity : MonoBehaviour
    {
        private Vector3 _spawnPoint;
        private Rigidbody _rigidBody;
        [SerializeField]
        private float _respawnTime = 1.44f;
        [SerializeField]
        private float _outOfBoundsDeathRange = 10f;
        private bool _isDead;

        [field: SerializeField]
        public UnityEvent OnDeath { get; private set; }

        public void Kill()
        {
            OnDeath.Invoke();
            _isDead = true;        
            StartCoroutine(Respawn());
        }

        public IEnumerator Respawn()
        {
            _rigidBody.constraints = RigidbodyConstraints.FreezeAll;
            // TODO: Hide Model
            GameObject child = GetComponentInChildren<Renderer>().gameObject;
            child.SetActive(false);
            yield return new WaitForSeconds(_respawnTime);
            _rigidBody.constraints = RigidbodyConstraints.None;
            _rigidBody.position = _spawnPoint;
            _rigidBody.velocity = Vector3.zero;
            _rigidBody.rotation = Quaternion.identity;
            child.SetActive(true);
            yield return new WaitForFixedUpdate();            
            _isDead = false;
        }

        protected void Awake()
        {
            _rigidBody = GetComponent<Rigidbody>();
            _spawnPoint = _rigidBody.position;
            Platforms.Initialize();
        }

        protected void Update()
        {
            CheckInBounds();
        }

        private void CheckInBounds()
        {
            if (_isDead) { return; }
            if ((_rigidBody.position.y + _outOfBoundsDeathRange) < Platforms.MinY)
            {
                Kill();
            }
        }

    }
}