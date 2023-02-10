using System.Collections;
using System.Collections.Generic;
using CrazyMarble.UI;
using UnityEngine;

namespace CrazyMarble
{
    [RequireComponent(typeof(Collider))]
    public class TextTrigger : MonoBehaviour
    {
        [SerializeField]
        private string _text;

        private void OnTriggerStay(Collider other) {
            if(other?.attachedRigidbody.GetComponent<MarbleEntity>() == null) { return; }
            HUD.ShowInfoText(_text, 0.5f);
        }
    }
}