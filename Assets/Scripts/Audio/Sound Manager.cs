using UnityEngine;
using System.Collections.Generic;

//This is how you call sounds :  SoundManager.Instance.PlaySound(SoundType.Music, SoundName.BackgroundMusic);
public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance;

    [SerializeField]
    private List<SoundCategory> soundCategories; // List of sound categories with organized sounds

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // Optional: keep SoundManager alive across scenes
        }
        else
        {
            Destroy(gameObject);
        }
    }
 

    /// <summary>
    /// Plays a sound based on the type and name.
    /// </summary>
    /// <param name="soundType">The type of the sound (e.g., Music, Narration, UI).</param>
    /// <param name="soundName">The name of the sound to play.</param>
    public void PlaySound(SoundType soundType, SoundName soundName)
    {
        // Find the category
        var category = soundCategories.Find(cat => cat.type == soundType);
        if (category == null)
        {
            Debug.LogWarning($"Sound category '{soundType}' not found.");
            return;
        }

        // Find the sound
        var sound = category.sounds.Find(s => s.name == soundName);
        if (sound == null || sound.clip == null)
        {
            Debug.LogWarning($"Sound '{soundName}' not found in category '{soundType}'.");
            return;
        }

        // Play sound through SoundPoolManager
       
        SoundPoolManager.Instance.PlaySound(sound.clip, 1f) ;
    }
}

[System.Serializable]
public class SoundCategory
{
    public SoundType type; // Enum for sound category (Music, Narration, UI)
    public List<SoundData> sounds; // List of sounds in this category
}

[System.Serializable]
public class SoundData
{
    public SoundName name; // Enum for specific sound name
    public AudioClip clip; // The AudioClip to play
}

/// <summary>
/// Main categories for sound types.
/// </summary>
public enum SoundType
{
    Music,
    Narration,
    UI
}

/// <summary>
/// Specific names for individual sounds.
/// </summary>
public enum SoundName
{
    BackgroundMusic,
    VictoryTheme,
    ButtonClick,
    Interaction1,
    Interaction2,
    Notification
}