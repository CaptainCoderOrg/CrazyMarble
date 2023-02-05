using UnityEngine;

namespace CrazyMarble.Hazard
{
    public class FlameThrower : MonoBehaviour
    {
        private ParticleSystem.MainModule _psMain;
        [SerializeField]
        private Collider _collider;
        private float _turnedOnTime;
        [SerializeField]
        private float _timeToMaxSize = 0.5f;
        private bool _isOn = false;

        [SerializeField]
        private float _maxFlameSize = 3;
        [field: SerializeField]
        public ParticleSystem Particles { get; private set; }

        public float FlameSize
        {
            get
            {
                if (!_isOn) { return 0; }
                float scale = Mathf.Clamp01((Time.time - _turnedOnTime) / _timeToMaxSize);
                return _maxFlameSize * scale;
            }
        }

        public void Awake()
        {
            _psMain = Particles.main;
            _collider.gameObject.SetActive(false);
        }

        public void Update()
        {
            _psMain.startSpeed = FlameSize;
        }

        public void TurnOn()
        {
            _turnedOnTime = Time.time;
            _isOn = true;
            _collider.gameObject.SetActive(true);
        }

        public void TurnOff()
        {
            _isOn = false;
            _collider.gameObject.SetActive(false);
        }
    }
}