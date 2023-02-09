using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CaptainCoder.Audio
{
    public static class SFXController
    {
        private static float? _volume;
        public static float Volume 
        { 
            get 
            {
                if (_volume == null)
                {
                    _volume = PlayerPrefs.GetFloat("SFXVolume", 0.5f);
                }
                return _volume.Value;
            }
            set 
            {
                _volume = Mathf.Clamp(value, 0, 1);
                PlayerPrefs.SetFloat("SFXVolume", _volume.Value);

            }
        }
        
        public static void Play(AudioClip clip)
        {
            GameObject go = new ($"Audio Clip: {clip.name}");
            SelfDestructable selfDestructable = go.AddComponent<SelfDestructable>();
            selfDestructable.SelfDestructIn(clip.length + 0.1f);
            AudioSource audioSource = go.AddComponent<AudioSource>();
            audioSource.volume = _volume.Value;
            audioSource.clip = clip;
            audioSource.Play();
        }
    }
}