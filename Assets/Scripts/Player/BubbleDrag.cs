using UnityEngine;

public class BubbleDrag : MonoBehaviour
{
    public PlayerSettings playerSettings;

    public bool isDragging = false;
    private Camera cam;

    void Start()
    {
        cam = Camera.main;
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            DetectBubble();
        }

        if (isDragging)
        {
            transform.gameObject.GetComponent<PolygonCollider2D>().isTrigger = true;
            Vector3 fixedWithZPosition = GetMouseWorldPosition();
            fixedWithZPosition.z = 0;

            fixedWithZPosition.x = Mathf.Clamp(fixedWithZPosition.x, cam.ViewportToWorldPoint(new Vector3(playerSettings.borderOffset / cam.orthographicSize, 0, 0)).x, cam.ViewportToWorldPoint(new Vector3(1 - playerSettings.borderOffset / cam.orthographicSize, 0, 0)).x);
            fixedWithZPosition.y = Mathf.Clamp(fixedWithZPosition.y, cam.ViewportToWorldPoint(new Vector3(0, playerSettings.borderOffset / cam.orthographicSize, 0)).y, cam.ViewportToWorldPoint(new Vector3(0, 1 - playerSettings.borderOffset / cam.orthographicSize, 0)).y);

            Vector3 smoothedPosition = Vector3.Lerp(transform.position, fixedWithZPosition, playerSettings.BubbleDragSmooth);
            transform.position = smoothedPosition;
        }

        if (Input.GetMouseButtonUp(0))
        {
            isDragging = false;
            transform.gameObject.GetComponent<PolygonCollider2D>().isTrigger = false;
        }
    }

    void DetectBubble()
    {
        Vector2 mousePosition = cam.ScreenToWorldPoint(Input.mousePosition);
        RaycastHit2D hit = Physics2D.Raycast(mousePosition, Vector2.zero);

        if (hit.collider != null && hit.collider.CompareTag("Bubble"))
        {
            isDragging = true;
        }
    }

    private Vector3 GetMouseWorldPosition()
    {
        Vector3 mousePos = Input.mousePosition;
        mousePos.z = 0f;
        return cam.ScreenToWorldPoint(mousePos);
    }
}
