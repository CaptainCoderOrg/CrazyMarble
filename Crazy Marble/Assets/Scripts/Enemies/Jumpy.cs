using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CrazyMarble.Enemy
{
    public enum JumpyState { Waiting, Jumping }

    public class Jumpy : MonoBehaviour
    {
        private Rigidbody _rigidBody;

        public void Awake()
        {
            _rigidBody = GetComponentInChildren<Rigidbody>();
            Debug.Assert(_rigidBody != null, "Could not find RigidBody on child");
        }
    }
}
