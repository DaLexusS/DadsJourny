using UnityEngine;
using UnityEngine.Events;

public class DeathOnTouch : MonoBehaviour
{
    static public UnityAction onPlayerDeath;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!collision.gameObject.CompareTag("PlayerCharacter")) { Debug.Log("Was collided but not with PlayerCharacter"); return; }
        Debug.Log("collided with player, invoking death");
        onPlayerDeath.Invoke();
    }
}
