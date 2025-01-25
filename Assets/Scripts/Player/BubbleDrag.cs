using UnityEngine;
using UnityEngine.U2D;

public class BubbleDrag : MonoBehaviour
{
    public PlayerSettings playerSettings;

    public bool isDragging = false;
    private Camera cam;
    [SerializeField] Sprite DefaultSprite; //texture num 1
    [SerializeField] Sprite WhileMoving;//texture num 2
    //[SerializeField] Sprite Glow_NouseCursor; //texture num 3
    [SerializeField] Sprite ActiveSprite; //texture num 4
    [SerializeField] SpriteRenderer MySpriteRenderer;
    [SerializeField] BubbleInteraction bubbleInteraction;


    public void SwapBubbleSprite(int textureNum)
    {
        // Create a Sprite from the Texture2D based on the input number
        switch (textureNum)
        {
            case 1:
                ComapreAndReplaceSprites(DefaultSprite);
                break;
            case 2:
                ComapreAndReplaceSprites(WhileMoving);
                break;
            case 3:
               // ComapreAndReplaceSprites(Glow_NouseCursor);
                break;
            case 4:
                ComapreAndReplaceSprites(ActiveSprite);
                break;
            default:
                Debug.LogWarning("Invalid texture number.");
                break;
        }
    }
    private void ComapreAndReplaceSprites( Sprite sprite2) 
    {
        if (MySpriteRenderer.sprite == sprite2)
        {
            return;
        }
        else if (MySpriteRenderer.sprite == ActiveSprite  && sprite2 == DefaultSprite)
        {
            return;
        }
        else
        {
            MySpriteRenderer.sprite = sprite2;
            return;
        }
      
    }
    



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
            if (bubbleInteraction.LastObject == null)
            {
                SwapBubbleSprite(2); // ben swaps textures
            }
           
            transform.gameObject.GetComponent<PolygonCollider2D>().isTrigger = true;
            Vector3 fixedWithZPosition = GetMouseWorldPosition();
            fixedWithZPosition.z = 0;

            fixedWithZPosition.x = Mathf.Clamp(fixedWithZPosition.x, cam.ViewportToWorldPoint(new Vector3(playerSettings.borderOffset / cam.orthographicSize, 0, 0)).x, cam.ViewportToWorldPoint(new Vector3(1 - playerSettings.borderOffset / cam.orthographicSize, 0, 0)).x);
            fixedWithZPosition.y = Mathf.Clamp(fixedWithZPosition.y, cam.ViewportToWorldPoint(new Vector3(0, playerSettings.borderOffset / cam.orthographicSize, 0)).y, cam.ViewportToWorldPoint(new Vector3(0, 1 - playerSettings.borderOffset / cam.orthographicSize, 0)).y);

            Vector3 smoothedPosition = Vector3.Lerp(transform.position, fixedWithZPosition, playerSettings.BubbleDragSmooth);
            transform.position = smoothedPosition;
        }
        else
        {
            SwapBubbleSprite(1);
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
            SoundManager.Instance.PlaySound(SoundType.UI, SoundName.Click_OnBubble, 0.5f);
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
