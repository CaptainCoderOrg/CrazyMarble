using System.Collections;
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

        public void Awake()
        {
            _flameThrowers = GetComponentsInChildren<FlameThrower>();
            StartCoroutine(nameof(FlameThrowerLoop));
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
            foreach (FlameThrower ft in _flameThrowers)
            {
                ft.TurnOn();
            }
        }

        public void TurnOff()
        {
            foreach (FlameThrower ft in _flameThrowers)
            {
                ft.TurnOff();
            }
        }
    }
}