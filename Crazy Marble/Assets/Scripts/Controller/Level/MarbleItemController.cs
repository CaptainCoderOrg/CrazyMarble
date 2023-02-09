using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CrazyMarble
{
    public enum MarbleItem { RedKey, GreenKey }

    [RequireComponent(typeof(Collider))]
    public class MarbleItemController : MonoBehaviour
    {
        [field: SerializeField]
        public MarbleItem Item { get; private set; }

        private void OnTriggerEnter(Collider other) {
            if (other.attachedRigidbody == null) { return; }
            MarbleInventory inventory = other.attachedRigidbody.GetComponent<MarbleInventory>();
            if (inventory != null)
            {
                inventory.Add(Item);
                Destroy(gameObject);
            }
        }
    }
}