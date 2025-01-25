using UnityEngine;
using UnityEngine.UI;

public class LevelMenu : MonoBehaviour
{
    public Button[] levelButtons;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        for (int i = 0; i < levelButtons.Length; i++)
        {
            string key= $"Level{i} unlocked!";
            bool isUnlocked = i == 0 || PlayerPrefs.GetInt(key, 0) == 1;
            
            levelButtons[i].interactable = isUnlocked;

            int levelIndex = i + 1;
            Debug.Log($"assigning button {i} to load scene {levelIndex}");
            
            levelButtons[i].onClick.AddListener(() => LoadLevel(levelIndex));
        }
    }

    private void LoadLevel(int levelIndex)
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(levelIndex);
    }
}
