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

        private void Awake() {
            _door = GetComponentInParent<DoorController>();
            Debug.Assert(_door != null);   
        }

        private void OnTriggerEnter(Collider other) {
            if (other.attachedRigidbody == null) { return; }
            MarbleInventory inventory = other.attachedRigidbody.GetComponent<MarbleInventory>();
            if (inventory != null && inventory.Items.Contains(_key))
            {
                _door.Open();
            }
        }
    
    }
}