using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CaptainCoder.Audio
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
        public AudioClip Explosion { get; private set; }
        [field: SerializeField]
        public AudioClip Bounce { get; private set; }
    }
}