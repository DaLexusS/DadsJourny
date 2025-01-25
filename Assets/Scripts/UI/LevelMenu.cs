using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class LevelMenu : MonoBehaviour
{
    static public UnityAction<int> loadPressedLevel;
    public Button[] levelButtons;
    
    void Start()
    {
        for (int i = 0; i < levelButtons.Length; i++)
        {
            string key= $"Level{i} unlocked!";
            bool isUnlocked = i == 0 || PlayerPrefs.GetInt(key, 0) == 1;
            
            levelButtons[i].interactable = isUnlocked;

            int levelIndex = i + 1;
            
            levelButtons[i].onClick.AddListener(() => LoadLevel(levelIndex));

            Debug.Log(key);
        }
    }

    private void LoadLevel(int levelIndex)
    {
        loadPressedLevel.Invoke(levelIndex + 1);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
