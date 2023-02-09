using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CaptainCoder.Audio
{
    public class MusicTrackDatabase : MonoBehaviour
    {
        [field: SerializeField]
        private AudioClip[] Tracks { get; set; }
        public AudioClip Track(int ix) => Tracks[ix % Tracks.Length];
    }
}