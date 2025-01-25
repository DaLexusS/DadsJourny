using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class EndLevel : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("PlayerCharacter"))
        {
            MarkLevelComplete();
            LoadNextLevel();
        }
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void LoadNextLevel()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        int nextSceneIndex = currentSceneIndex + 1;

        if (nextSceneIndex < SceneManager.sceneCountInBuildSettings)
        {
            SceneManager.LoadScene(nextSceneIndex);
            Debug.Log("moved to next level");
        }
        else
        {
            Debug.Log("no more levels");
        }
    }

    void MarkLevelComplete()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        string key= $"Level_ ${currentSceneIndex}_Unlocked";
        
        PlayerPrefs.SetInt(key, 1 );
        PlayerPrefs.Save();
        
        Debug.Log($"Level {currentSceneIndex} completed. Unlocking Level {currentSceneIndex + 1}.");
    }
}
