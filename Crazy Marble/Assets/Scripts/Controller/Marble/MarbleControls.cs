using UnityEngine;
using CrazyMarble.Input;

namespace CrazyMarble
{
    [RequireComponent(typeof(Rigidbody))]
    public class MarbleControls : MonoBehaviour
    {
        private MarbleInputs _userInput;
        public MarbleInputs UserInput
        {
            get
            {
                if (_userInput == null)
                {
                    _userInput = new MarbleInputs();
                }
                return _userInput;
            }
        }

        protected void OnEnable()
        {
            UserInput.Enable();
        }

        protected void OnDisable()
        {
            UserInput.Disable();
        }
    }
}