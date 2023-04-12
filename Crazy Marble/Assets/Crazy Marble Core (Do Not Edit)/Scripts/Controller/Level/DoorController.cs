using System.Collections;
using System.Collections.Generic;
using CrazyMarble.Audio;
using UnityEngine;
using CaptainCoder.Audio;
namespace CrazyMarble
{
    public class DoorController : MonoBehaviour
    {
        [SerializeField]
        private float _animationTime = 2f;
        private float _transitionStartTime;
        private bool _isOpening;
        public void Open() => StartCoroutine(OpenAnimation());

        private void Update()
        {
            if (!_isOpening) { return; }
            float progress = Mathf.Clamp01((Time.time - _transitionStartTime) / _animationTime);
            transform.localScale = Vector3.one * (1 - progress);
        }

        private IEnumerator OpenAnimation()
        {
            AudioSource audio = GetComponent<AudioSource>();
            SFXController.Play(SFXDatabase.Instance.DoorOpen);
            yield return new WaitForSeconds(.5f);
            GetComponent<SoundEffect>()?.Play();
            audio.volume = SFXController.Volume * 0.1f;
            for (int i = 0; i < 10; i++)
            {
                audio.volume = SFXController.Volume * (0.05f * i);
                GameCamera.CurrentCamera.Shake(.1f, i);
                yield return new WaitForSeconds(.1f);
            }
            GameCamera.CurrentCamera.Shake(_animationTime, 10);
            _isOpening = true;            
            _transitionStartTime = Time.time;
            yield return new WaitForSeconds(_animationTime);
            Destroy(gameObject);
        }

    }
}