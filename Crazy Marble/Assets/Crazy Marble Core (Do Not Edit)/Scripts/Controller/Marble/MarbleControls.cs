using UnityEngine;
using CrazyMarble.Input;

namespace CrazyMarble
{
    public static class MarbleControls
    {
        private static MarbleInputs _userInput;
        public static MarbleInputs UserInput
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

        public static void OnEnable()
        {
            UserInput.Enable();
        }

        public static void OnDisable()
        {
            UserInput.Disable();
        }
    }
}