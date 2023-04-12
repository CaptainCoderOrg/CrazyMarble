using UnityEngine;

namespace CaptainCoder
{
    public static class GameObjectExtensions
    {
        public static GameObject InstantiateAt(this GameObject toCopy, Vector3 position, bool active = true)
        {
            GameObject copy = GameObject.Instantiate(toCopy);
            copy.transform.position = position;
            copy.SetActive(active);
            return copy;
        }
    }
}