using UnityEngine;
using UnityEngine.Events;

public class DeathOnTouch : MonoBehaviour
{
    static public UnityAction onPlayerDeath;
    private void OnCollisionEnter2D(Collision2D collision)
    {

        if (!collision.gameObject.CompareTag("PlayerCharacter")) { Debug.Log("Was collided with " + collision.gameObject.name + " but not with PlayerCharacter"); return; }
        Debug.Log("collided with player, invoking death");
        if (onPlayerDeath == null)
        {
            Debug.Log("on player death unity action was null");
        }
        else
        {
            onPlayerDeath.Invoke();
        }

    }
}
