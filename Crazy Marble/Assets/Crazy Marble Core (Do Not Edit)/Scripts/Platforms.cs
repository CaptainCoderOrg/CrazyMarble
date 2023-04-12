
using System.Collections.Generic;
using UnityEngine;

namespace CrazyMarble
{
    public static class Platforms
    {
        public static GameObject[] All { get; private set; }

        public static float MinY { get; private set; }

        public static void Initialize()
        {
            All = GameObject.FindGameObjectsWithTag(Tag.Platform);
            foreach (GameObject obj in All)
            {
                UpdateMinY(obj);
                Renderer renderer = obj.GetComponent<Renderer>();
                foreach (Transform child in obj.transform)
                {
                    UpdateMinY(child.gameObject);
                }
                
            }
        }

        private static void UpdateMinY(GameObject obj)
        {
            Renderer renderer = obj.GetComponent<Renderer>();
            if (renderer == null) { return; }
            MinY = Mathf.Min(MinY, renderer.bounds.min.y);
        }
    }
}