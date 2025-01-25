using UnityEngine;
using UnityEngine.SceneManagement;


public class Respawner : MonoBehaviour
{
    private Vector2 _startPosition;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // store initial position
        _startPosition = transform.position;
        
    }
    
    public void RespawnPlayer() // respawn method
    {
        //respawn animation
        PlayRespawnAnimation();
        
        //respawn at initial point if a checkpoint doesn't exist
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        Debug.Log("player spawned at initial position" + transform.position);
        
    }
    public void PlayRespawnAnimation()
    {
        Debug.Log("Playing respawn animation");
    }

    public void Die()
    {
        Debug.Log("Player died!");
        RespawnPlayer();
    }
}
