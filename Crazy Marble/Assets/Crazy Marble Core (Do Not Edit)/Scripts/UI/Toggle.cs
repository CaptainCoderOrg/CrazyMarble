using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace CrazyMarble.UI
{
    public class Toggle : MonoBehaviour
    {
        [field: SerializeField]
        public UnityEvent<bool> OnChange { get; private set; }

        void Awake()
        {
            UnityEngine.UI.Toggle toggle = GetComponentInChildren<UnityEngine.UI.Toggle>();
            toggle.onValueChanged.AddListener(OnChange.Invoke);            
        }
    }
}