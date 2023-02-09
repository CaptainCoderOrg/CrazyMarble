using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using CaptainCoder;
using CaptainCoder.Audio;
using CrazyMarble.Audio;
using CrazyMarble.UI;

namespace CrazyMarble
{
    [RequireComponent(typeof(Rigidbody))]
    public class MarbleEntity : MonoBehaviour
    {
        public static MarbleEntity Instance { get; private set; }
        private MarbleInventory _inventory;
        private Vector3 _spawnPoint;
        [SerializeField]
        private float _respawnTime = 1.44f;
        [SerializeField]
        private float _outOfBoundsDeathRange = 10f;
        private bool _isDead;
        [SerializeField]
        private GameObject _deathParticles;

        [field: SerializeField]
        public int LostCount { get; private set; } = 0;

        [field: SerializeField]
        public UnityEvent<MarbleEntity> OnDeath { get; private set; }
        [field: SerializeField]
        public UnityEvent OnSpawn { get; private set; }
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
            LostCount++;
            _isDead = true;
            _deathParticles.InstantiateAt(transform.position);
            OnDeath.Invoke(this);
            SFXController.Play(SFXDatabase.Instance.LostMarble);
            StartCoroutine(Respawn());
        }

        public IEnumerator Respawn()
        {
            RigidBody.constraints = RigidbodyConstraints.FreezeAll;
            GameObject child = GetComponentInChildren<Renderer>().gameObject;
            child?.SetActive(false);
            yield return new WaitForSeconds(_respawnTime);
            RigidBody.constraints = RigidbodyConstraints.None;
            RigidBody.position = _spawnPoint;
            RigidBody.velocity = Vector3.zero;
            RigidBody.rotation = Quaternion.identity;
            child?.SetActive(true);
            yield return new WaitForFixedUpdate();
            _isDead = false;
            _inventory.Clear();
            OnSpawn.Invoke();
        }

        protected void Awake()
        {
            // TODO: Potentially add warning about multiple instances
            Instance = this;
            RigidBody = GetComponent<Rigidbody>();
            _inventory = GetComponent<MarbleInventory>();
            _spawnPoint = RigidBody.position;
            OnDeath.AddListener(HUD.RenderLostMarbles);
            Platforms.Initialize();
            OnSpawn.Invoke();
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