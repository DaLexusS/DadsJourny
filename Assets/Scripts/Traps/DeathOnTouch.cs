using UnityEngine;
using UnityEngine.Events;

public class DeathOnTouch : MonoBehaviour
{
    static public UnityAction onPlayerDeath;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("Was collided");
        if (!collision.gameObject.CompareTag("PlayerCharacter")) { return; }
        onPlayerDeath.Invoke();
    }
}
