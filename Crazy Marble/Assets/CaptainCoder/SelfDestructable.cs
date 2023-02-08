using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CaptainCoder
{
    public class SelfDestructable : MonoBehaviour
    {
        [SerializeField]
        private float _destructAfter = 3f;

        public void Awake()
        {
            Invoke(nameof(SelfDestruct), _destructAfter);
        }

        private void SelfDestruct() => Destroy(gameObject);

        public void SelfDestructIn(float seconds)
        {
            Invoke(nameof(SelfDestruct), seconds);
        }
    }
}