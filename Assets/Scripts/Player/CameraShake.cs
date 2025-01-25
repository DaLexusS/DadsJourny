using UnityEngine;
using UnityEngine.Events;

public class CameraShake : MonoBehaviour
{
    [SerializeField] PlayerSettings _settings;
    private Vector3 originalPosition;
    private bool isShaking = false;
    private float shakeTimeRemaining = 0f;
    static public UnityAction OnShake;

    void Update()
    {
        if (isShaking)
        {
            if (shakeTimeRemaining > 0)
            {
                Vector3 shakeOffset = Random.insideUnitSphere * _settings.shakeMagnitude;
                transform.position = originalPosition + shakeOffset;
                shakeTimeRemaining -= Time.deltaTime;

                if (shakeTimeRemaining <= 0)
                {
                    isShaking = false;
                    transform.position = originalPosition;
                }
            }
        }
    }

    public void TriggerShake()
    {
        shakeTimeRemaining = _settings.shakeDuration;
        isShaking = true;
    }
}
