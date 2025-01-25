using UnityEngine;
using UnityEngine.Events;

public enum BubbleType
{
    Destroy,
    Bounce,
}

public class BubbleInteraction : MonoBehaviour
{
    static public UnityAction onLose;
    [SerializeField] public BubbleType myBubbleType;

    public BubbleDrag bubbleDrag;
    public GameObject LastObject = null;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (LastObject != null) { return; }
        if (collision.gameObject.CompareTag("Bubble_Interactable") ||
            collision.gameObject.CompareTag("Player") ||
            collision.gameObject.CompareTag("EndGoal"))
        {
            switch (myBubbleType)
            {
                case BubbleType.Destroy:
                    ObjectDisappear bubbleObject = collision.gameObject.GetComponent<ObjectDisappear>();
                    bubbleObject.ToggleObject();
                    LastObject = collision.gameObject;
                    bubbleDrag.SwapBubbleSprite(4);
                    break;
                default:
                    break;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (LastObject == null || other.gameObject != LastObject) { return; }
        if (other.CompareTag("Bubble_Interactable") ||
            other.CompareTag("Player") ||
            other.CompareTag("EndGoal"))
        {
                switch (myBubbleType)
                {
                    case BubbleType.Destroy:
                        ObjectDisappear bubbleObject = other.gameObject.GetComponent<ObjectDisappear>();
                        bubbleObject.ToggleObject();
                        LastObject = null;
                        break;
                    default:
                        break;
                }
        }
    }
}
