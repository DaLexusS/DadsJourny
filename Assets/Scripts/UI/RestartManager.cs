using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Events;

public class RestartManager : MonoBehaviour
{
    

    private void Awake()
    {
        DeathOnTouch.onPlayerDeath += RestartScene;
    }

    private void OnDestroy()
    {
        DeathOnTouch.onPlayerDeath -= RestartScene;
    }
    public void RestartScene()
    {
        Debug.Log("Played Died!");
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

}
