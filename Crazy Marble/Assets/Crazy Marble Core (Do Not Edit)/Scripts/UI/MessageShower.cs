using System.Collections;
using System.Collections.Generic;
using CrazyMarble.UI;
using UnityEngine;

namespace CrazyMarble
{
    public class MessageShower : MonoBehaviour
    {
        [SerializeField]
        private string _message;
        [SerializeField]
        private float _duration;

        public void DisplayMessage() => HUD.ShowInfoText(_message, _duration);
    }
}