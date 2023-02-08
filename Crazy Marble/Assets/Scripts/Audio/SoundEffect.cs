using System.Collections;
using System.Collections.Generic;
using CaptainCoder.Audio;
using UnityEngine;

namespace CrazyMarble.Audio
{

    [RequireComponent(typeof(AudioSource))]
    public class SoundEffect : MonoBehaviour
    {
        private AudioSource _audioSource;

        public void Awake()
        {
            _audioSource = GetComponent<AudioSource>();
        }

        public void Play()
        {
            _audioSource.volume = SFXController.Volume;
            _audioSource.Play();
        }

        public void Stop()
        {
            _audioSource.Stop();
        }
    }

}
