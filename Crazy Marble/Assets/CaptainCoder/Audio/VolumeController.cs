using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CaptainCoder.Audio
{

    public class VolumeController : MonoBehaviour
    {
        public void SetMusicVolume(float value) => MusicController.Instance.MusicVolume = value;
        public void SetSoundEffectsVolume(float value) => SFXController.Volume = value;
    }

}