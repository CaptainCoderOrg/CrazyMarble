using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CrazyMarble
{
    [RequireComponent(typeof(Collider))]
    public class DoorChecker : MonoBehaviour
    {
        [SerializeField]
        private MarbleItem _key;
        private DoorController _door;
        private bool _started = false;

        private void Awake() {
            _door = GetComponentInParent<DoorController>();
            Debug.Assert(_door != null);   
        }

        private void OnTriggerEnter(Collider other) {
            if (_started) { return; }
            if (other.attachedRigidbody == null) { return; }
            MarbleInventory inventory = other.attachedRigidbody.GetComponent<MarbleInventory>();
            if (inventory != null && inventory.Items.Contains(_key))
            {
                _started = true;
                _door.Open();
            }
        }
    
    }
}