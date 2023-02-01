using UnityEngine;
using UnityEngine.Events;

namespace CaptainCoder
{
    [RequireComponent(typeof(Collider))]
    public class ColliderTrigger : MonoBehaviour
    {
        [field: SerializeField]
        public UnityEvent<Collider> OnEnter { get; private set; }
        [field: SerializeField]
        public UnityEvent<Collider> OnExit { get; private set; }
        [field: SerializeField]
        public UnityEvent<Collider> OnStay { get; private set; }

        private void OnTriggerEnter(Collider other) => OnEnter.Invoke(other);
        private void OnTriggerExit(Collider other) => OnExit.Invoke(other);
        private void OnTriggerStay(Collider other) => OnStay.Invoke(other);
    }
}