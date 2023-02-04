namespace CrazyMarble.Input
{
    public static class CameraControls
    {
        private static MarbleInputs s_userInput = null;
        public static MarbleInputs.CameraControlsActions UserInput
        {
            get
            {
                if (s_userInput == null)
                {
                    s_userInput = new MarbleInputs();
                    s_userInput.Enable();
                }
                return s_userInput.CameraControls;
            }
        }
    }
}