using UnityEngine;
using static UnityEngine.InputSystem.InputAction;
using UnityEngine.SceneManagement;

namespace CrazyMarble.Input
{
    public static class GeneralControls
    {
        private static MarbleInputs s_userInput = null;
        public static MarbleInputs.GeneralControlsActions UserInput
        {
            get
            {
                if (s_userInput == null)
                {
                    s_userInput = new MarbleInputs();
                    s_userInput.GeneralControls.OptionsMenu.started += OpenOptionsMenu;
                    s_userInput.Enable();
                    
                }
                return s_userInput.GeneralControls;
            }
        }

        public static void Initialize() 
        { 
            UserInput.Enable();
        }

        private static void OpenOptionsMenu(CallbackContext context)
        {
            Scene active = SceneManager.GetSceneByName("Options");
            if (active.isLoaded)
            {
                SceneManager.UnloadSceneAsync("Options");
            }
            else
            {
                SceneManager.LoadSceneAsync("Options", LoadSceneMode.Additive);
            }
        }
    }
}