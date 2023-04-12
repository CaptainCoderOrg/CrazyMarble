using UnityEngine;
namespace CrazyMarble
{
    public class SkyBox : MonoBehaviour
    {
        [SerializeField]
        private float _rotationSpeed = 0.25f;
        [field: SerializeField]
        private Material _skyBox;
        public void Update()
        {
            _skyBox.SetFloat("_Rotation", Time.time * _rotationSpeed);
        }
    }
}