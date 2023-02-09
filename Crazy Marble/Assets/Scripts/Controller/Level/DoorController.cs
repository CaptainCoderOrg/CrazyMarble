using System.Collections;
using System.Collections.Generic;
using CrazyMarble.Audio;
using UnityEngine;

namespace CrazyMarble
{
    public class DoorController : MonoBehaviour
    {
        [SerializeField]
        private float _animationTime = 5f;
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
            GetComponent<SoundEffect>()?.Play();
            GameCamera.CurrentCamera.Shake(_animationTime, 10);
            _isOpening = true;
            _transitionStartTime = Time.time;
            yield return new WaitForSeconds(_animationTime);
            Destroy(gameObject);
        }

    }
}