using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CrazyMarble.Hazard
{
    [RequireComponent(typeof(Collider))]
    public class InstantDeathTrigger : MonoBehaviour
    {
        public void OnTriggerEnter(Collider other)
        {
            if (other.attachedRigidbody == null) { return; }
            MarbleEntity marble = other.attachedRigidbody.GetComponent<MarbleEntity>();
            marble?.Kill();
        }
    }
}