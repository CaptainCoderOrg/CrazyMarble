using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CaptainCoder
{
    public class Toggleable : MonoBehaviour
    {
        public void Toggle()
        {
            gameObject.SetActive(!gameObject.activeInHierarchy);
        }
    }
}