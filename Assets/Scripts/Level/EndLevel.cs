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

    void LoadNextLevel()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        int nextSceneIndex = currentSceneIndex + 1;

        if (nextSceneIndex < SceneManager.sceneCountInBuildSettings)
        {
            callNextLevel.Invoke(nextSceneIndex);
        }
    }

    void MarkLevelComplete()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        int temp = PlayerPrefs.GetInt("Level") + 1;
        PlayerPrefs.SetInt("Level", temp);
        PlayerPrefs.Save();
    }
}
