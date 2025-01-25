using UnityEngine;

[CreateAssetMenu(fileName = "PlayerSettings", menuName = "Scriptable Objects/PlayerSettings")]
public class PlayerSettings : ScriptableObject
{
    [Header("Player Settings")]
    [SerializeField] public float WalkSpeed = 50;
    [SerializeField] public float JumpPower = 5;

    [Header("Camera Settings")]
    [SerializeField] public float smoothSpeed = 0.125f;
    [SerializeField] public Vector3 offset;

    [Header("Shake Settings")]
    [SerializeField] public float shakeDuration = 0.5f;
    [SerializeField] public float shakeMagnitude = 0.2f;
    [SerializeField] public float shakeFrequency = 0.1f;

    [Header("Bubble Settings")]
    [SerializeField] public float BubbleDragSmooth = 0.125f;
    [SerializeField] public float borderOffset = 3f;
}
