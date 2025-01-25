using UnityEngine;

public class PopBaloon : MonoBehaviour
{
    [SerializeField] public bool PopTheBalloon = false;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("PlayerCharacter")) // Ensure it's the player
        {
            PopTheBalloon = true;
        }
    }


}
