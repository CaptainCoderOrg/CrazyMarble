using System.Collections;
using CrazyMarble.Audio;
using UnityEngine;

namespace CrazyMarble.Hazard
{
    public class FlameThrowerArray : MonoBehaviour
    {

        [SerializeField]
        private FlameThrower[] _flameThrowers;
        [SerializeField]
        private float _onDuration = 5;
        [SerializeField]
        private float _offDuration = 2;
        private SoundEffect _flameSound;

        public void Awake()
        {
            _flameThrowers = GetComponentsInChildren<FlameThrower>();
            _flameSound = GetComponentInChildren<SoundEffect>();
            StartCoroutine(FlameThrowerLoop());
        }

        public IEnumerator FlameThrowerLoop()
        {
            while (true)
            {
                TurnOn();
                yield return new WaitForSeconds(_onDuration);
                TurnOff();
                yield return new WaitForSeconds(_offDuration);
            }
        }

        public void TurnOn()
        {
            _flameSound?.Play();
            foreach (FlameThrower ft in _flameThrowers)
            {
                ft.TurnOn();
            }
        }

        public void TurnOff()
        {
            _flameSound?.Stop();
            foreach (FlameThrower ft in _flameThrowers)
            {
                ft.TurnOff();
            }
        }
    }
}