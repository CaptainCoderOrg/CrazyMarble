using System.Collections;
using System.Collections.Generic;
using CaptainCoder.Audio;
using UnityEngine;

namespace CrazyMarble.Audio
{

    [RequireComponent(typeof(AudioSource))]
    public class RandomSoundEffect : MonoBehaviour
    {
        private AudioSource _audioSource;
        [SerializeField]
        private AudioClip[] _clips;

        public void Awake()
        {
            _audioSource = GetComponent<AudioSource>();
        }

        public void Play()
        {
            AudioClip clip = _clips[Random.Range(0, _clips.Length)];
            _audioSource.clip = clip;
            _audioSource.volume = SFXController.Volume;
            _audioSource.Play();
        }

        public void Stop()
        {
            _audioSource.Stop();
        }
    }

}
