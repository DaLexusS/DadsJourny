using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

public class EndLevel : MonoBehaviour
{
    static public UnityAction<int> callNextLevel;
    [SerializeField] Tutorial tutorial;
    [SerializeField] GameEndingSequence gameEndingSequence;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("PlayerCharacter"))
        {
            if (tutorial)
            {
                tutorial.Finish();
            }
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
        else
        {
            //  Call GameEndingSequence to play the video and fade out
            gameEndingSequence.StartEndingSequence();
            return; // Prevent further scene loading
        }
    }

    public void OnGameEndingFinished()
    {
        // ✅ Called when GameEndingSequence is finished - go to the main menu
        callNextLevel.Invoke(0);
    }

    void MarkLevelComplete()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        int temp = PlayerPrefs.GetInt("Level") + 1;
        PlayerPrefs.SetInt("Level", temp);
        PlayerPrefs.Save();
    }
}
