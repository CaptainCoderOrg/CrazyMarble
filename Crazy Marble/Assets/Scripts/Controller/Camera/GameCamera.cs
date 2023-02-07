using System.Collections;
using Cinemachine;
using UnityEngine;

[RequireComponent(typeof(CinemachineFreeLook))]
public class GameCamera : MonoBehaviour
{

    public static GameCamera CurrentCamera { get; private set; }
    private CinemachineFreeLook _cam;
    private bool _isShaking = false;

    public void Awake()
    {
        CurrentCamera = this;
        _cam = GetComponent<CinemachineFreeLook>();
    }

    public void Shake(float duration, float intensity)
    {
        if (_isShaking) { return; }
        StartCoroutine(StartShake(duration, intensity));
    }

    private IEnumerator StartShake(float duration, float intensity)
    {
        _isShaking = true;
        SetShake(intensity);
        yield return new WaitForSeconds(duration);
        SetShake(0);
        _isShaking = false;
    }

    private void SetShake(float intensity)
    {
        for (int i = 0; i < 3; i++)
        {
            var perlin = _cam.GetRig(i).GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
            perlin.m_AmplitudeGain = intensity;
            perlin.m_FrequencyGain = intensity;
        }
    }

}