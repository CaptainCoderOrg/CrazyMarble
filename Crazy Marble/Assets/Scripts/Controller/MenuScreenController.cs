using UnityEngine;
using CaptainCoder.Audio;

namespace CrazyMarble
{

    public class MenuScreenController : MonoBehaviour
    {
        // Start is called before the first frame update
        void Start()
        {
            MusicController.Instance.StartTrack(0);
        }
    }

}