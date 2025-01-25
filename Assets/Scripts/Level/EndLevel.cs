using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class EndLevel : MonoBehaviour
{
    static public UnityAction<int> callNextLevel;
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
            callNextLevel.Invoke(nextSceneIndex);
            //SceneManager.LoadScene(nextSceneIndex);
            //Debug.Log("moved to next level");
        }
    }

    void MarkLevelComplete()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        string key= $"Level_ ${currentSceneIndex}_Unlocked";
        int currentLevle = PlayerPrefs.GetInt(key);
        PlayerPrefs.SetInt(key, currentLevle++);
        PlayerPrefs.Save();
        
        Debug.Log($"Level {currentSceneIndex} completed. Unlocking Level {currentSceneIndex + 1}.");
    }
}
