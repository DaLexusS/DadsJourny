using Unity.VisualScripting;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;
    [SerializeField] PlayerSettings playerSettings;

    private void LateUpdate()
    {
        Vector3 desiredPosition = target.position + playerSettings.offset;

        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, playerSettings.smoothSpeed);

        transform.position = smoothedPosition;
    }
}
