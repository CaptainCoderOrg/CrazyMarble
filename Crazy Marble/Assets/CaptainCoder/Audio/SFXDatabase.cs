using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CrazyMarble.Audio
{

    public class SFXDatabase : MonoBehaviour
    {
        private static SFXDatabase s_Instance;
        public static SFXDatabase Instance
        {
            get
            {
                if (s_Instance == null)
                {
                    s_Instance = Resources.Load<SFXDatabase>("Prefabs/SFXDatabase");
                }
                return s_Instance;
            }
        }

        [field: SerializeField]
        public AudioClip LostMarble { get; private set; }
        [field: SerializeField]
        public AudioClip PickUp { get; private set; }
        [field: SerializeField]
        public AudioClip DoorOpen { get; private set; }
    }
}