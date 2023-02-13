using System.Collections;
using Cinemachine;
using UnityEngine;

[RequireComponent(typeof(CinemachineFreeLook))]
public class GameCamera : MonoBehaviour
{

    public static GameCamera CurrentCamera { get; private set; }
    private CinemachineFreeLook _cam;
    private bool _isShaking = false;
    [SerializeField]
    private float _startYAxisTilt = 0.8f;
    [SerializeField]
    private int _preferredAngle = 5;
    [SerializeField]
    private float _correctionSpeed = 5f;

    public bool IsMoving { get; set; }

    public void Awake()
    {
        CurrentCamera = this;
        _cam = GetComponent<CinemachineFreeLook>();
    }

    public void Update()
    {
        UpdateCameraAngle();

    }

    private void UpdateCameraAngle()
    {
        if (IsMoving) { return; }
        int currentAngle = (int)_cam.m_XAxis.Value;
        if (currentAngle == 0 || currentAngle >= 179 || currentAngle <= -179) { return; }
        int targetAngle = NearestAngle(currentAngle);
        if (currentAngle < targetAngle)
        {
            _cam.m_XAxis.Value += Time.deltaTime * _correctionSpeed;
        }
        else if (currentAngle > targetAngle)
        {
            _cam.m_XAxis.Value -= Time.deltaTime * _correctionSpeed;
        }
    }

    private int NearestAngle(int value)
    {
        if (value >= 179 || value <= -179) { return 0; }
        return (int)System.Math.Round(
                    (value / (double)_preferredAngle),
                    System.MidpointRounding.AwayFromZero
                ) * _preferredAngle;
    }

    public void Start()
    {
        Invoke(nameof(ForceAxis), 0.1f);
    }

    private void ForceAxis()
    {
        _cam.m_YAxis.Value = _startYAxisTilt;
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