using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CrazyMarble
{
    [RequireComponent(typeof(Collider))]
    public class CheckPoint : MonoBehaviour
    {
        private void OnTriggerEnter(Collider other) {
            if(other.attachedRigidbody == null) { return; }
            MarbleEntity entity = other.attachedRigidbody.GetComponent<MarbleEntity>();
            if (entity == null) { return; }
            entity.SpawnPoint = transform.position;       
        }
    }
}