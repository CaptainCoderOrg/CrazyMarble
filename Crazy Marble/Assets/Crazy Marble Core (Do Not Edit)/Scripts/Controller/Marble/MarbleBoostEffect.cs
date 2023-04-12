using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace CrazyMarble
{
    public class MarbleBoostEffect : MonoBehaviour
    {
        private TrailRenderer _trail;
        private ParticleSystem _particles;
        private ParticleSystem.MainModule _particlesMain;
        private AudioSource _sfx;

        internal void Awake()
        {
            _trail = GetComponentInChildren<TrailRenderer>();
            Debug.Assert(_trail != null);
            _particles = GetComponentInChildren<ParticleSystem>();
            Debug.Assert(_particles != null);
            _sfx = GetComponent<AudioSource>();
            Debug.Assert(_sfx != null);
            MarbleBoostController _boostController = GetComponentInParent<MarbleBoostController>();
            Debug.Assert(_boostController != null);
            _particlesMain = _particles.main;
            _boostController.OnChange.AddListener(HandleChange);
        }

        private void HandleChange(MarbleBoostController controller)
        {
            _trail.emitting = controller.IsBoosting;
            _particlesMain.loop = controller.IsBoosting;
            if (controller.IsBoosting)
            {
                // _sfx.Play();
                _particles.Play();
            }
            else
            {
                _sfx.Stop();
            }
        }

        
    }
}