using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class LevelMenu : MonoBehaviour
{
    public static UnityAction<int> loadPressedLevel;
    public Button[] levelButtons;

    private void Awake()
    {
        if (!PlayerPrefs.HasKey("Level"))
        {
            PlayerPrefs.SetInt("Level", 1);
            PlayerPrefs.Save();
        }
    }

    private void Start()
    {
        int playerLevel = PlayerPrefs.GetInt("Level");


        for (int i = 0; i < levelButtons.Length; i++)
        {
            if (i <= playerLevel - 1)
            {
                int levelIndex = i + 1;
                levelButtons[i].interactable = true;
                levelButtons[i].onClick.AddListener(() => LoadLevel(levelIndex));
            }
            else
            {
                levelButtons[i].interactable = false;
            }
        }
    }

    private void LoadLevel(int levelIndex)
    {
        SoundManager.Instance.PlaySound(SoundType.UI, SoundName.Click_OnButton, 0.4f);
        loadPressedLevel?.Invoke(levelIndex);
    }

    public void QuitGame()
    {
        SoundManager.Instance.PlaySound(SoundType.UI, SoundName.Click_OnButton, 0.4f);
        Application.Quit();
    }
}
