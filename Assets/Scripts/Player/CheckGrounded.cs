using UnityEngine;
using UnityEngine.Events;

public class CheckGrounded : MonoBehaviour
{
    public bool isGrounded = false;
    public bool isCollidingGround = false;

    static public UnityAction onLanded;
    static public UnityAction<bool> AirBorn;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isCollidingGround = true;
        }
        else
        {
            isCollidingGround = false;
        }

        isGrounded = true;
        AirBorn.Invoke(false);
        onLanded.Invoke();

    }

    private void OnCollisionExit2D(Collision2D collision)
    {  
        isGrounded = false;
        AirBorn.Invoke(true);
    }

}
