using UnityEngine;
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

        private static int? _invertXAxis;
        public static bool InvertXAxis
        { 
            get 
            {
                if (_invertXAxis == null)
                {
                    _invertXAxis = PlayerPrefs.GetInt("InvertXAxis", 1);
                }
                return _invertXAxis == 1;         
            }
            set
            {
                _invertXAxis = value ? 1 : 0;
                PlayerPrefs.SetInt("InvertXAxis", _invertYAxis.Value);
            }
        }
        
        private static int? _invertYAxis;
        public static bool InvertYAxis
        { 
            get 
            {
                if (_invertYAxis == null)
                {
                    _invertYAxis = PlayerPrefs.GetInt("InvertYAxis", 1);
                }
                return _invertYAxis == 1;         
            }
            set
            {
                _invertYAxis = value ? 1 : 0;
                PlayerPrefs.SetInt("InvertYAxis", _invertYAxis.Value);
            }
        }
        private static float?  _mouseSensitivity;
        public static float MouseSensitivity 
        { 
            get 
            {
                if (_mouseSensitivity == null)
                {
                    _mouseSensitivity = PlayerPrefs.GetFloat("MouseSensitivity", 0.5f);
                }
                return _mouseSensitivity.Value;                
            }
            set
            {
                _mouseSensitivity = value;
                PlayerPrefs.SetFloat("MouseSensitivity", _mouseSensitivity.Value);
            }
        }
    }
}