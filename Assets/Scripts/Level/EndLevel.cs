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
    [SerializeField] GameObject EndingVidDisplayTexture;

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
            // ✅ Ensure the video UI is active before playing
            if (EndingVidDisplayTexture)
            {
                EndingVidDisplayTexture.SetActive(true);
            }

            // ✅ Play the Ending Video
            gameEndingSequence.StartEndingSequence();
        }
    }

    public void OnGameEndingFinished()
    {
        Debug.Log("Game Ending Finished - Moving to Main Menu");

        // ✅ Ensure we move to the Main Menu (Scene 0)
        if (callNextLevel != null)
        {
            callNextLevel.Invoke(0);
        }
        else
        {
            SceneManager.LoadScene(0); // Fallback if event is missing
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
