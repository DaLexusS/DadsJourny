using System.Numerics;
using UnityEngine;
using Vector2 = System.Numerics.Vector2;

public class EnemyAI : MonoBehaviour
{
    public Transform player; // player's transform
    public float shootingRange = 10f; //range within enemy will shoot
    private bool hasShot = false;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (player == null) // checks for player reference
        {
            Debug.LogWarning("player transform not assigned");
        }
        
        Vector2 playerPosition = new Vector2(player.position.x, player.position.y); // player position
        Vector2 enemyPosition = new Vector2(transform.position.x, transform.position.y); // enemy position
        
        
        float distanceToPlayer = Vector2.Distance(enemyPosition, playerPosition); // calculate distance in 2d enviorment

        if (distanceToPlayer <= shootingRange) // check if within shooting range
        {
            if (distanceToPlayer <= shootingRange && !hasShot)
            {
                Shoot();
                hasShot = true; // Set the flag to true after shooting
            }
            else if (distanceToPlayer > shootingRange && hasShot)
            {
                hasShot = false; // Reset the flag if the player moves out of range
            }
        }
    }

    void Shoot()
    {
        Debug.Log("shoot"); // placeholder for shooting logic
    }
}
