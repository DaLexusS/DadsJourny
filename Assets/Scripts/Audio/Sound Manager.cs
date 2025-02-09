using UnityEngine;
using System.Collections.Generic;

//This is how you call sounds :  SoundManager.Instance.PlaySound(SoundType.Music, SoundName.BackgroundMusic);
public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance;

    [SerializeField]
    private List<SoundCategory> soundCategories; // List of sound categories with organized sounds
    [SerializeField] private bool MuteSounds = false;
    [SerializeField] private bool MuteMusic = false;
    [SerializeField] AudioSource MusicSource;

    private SoundName lastPlayedNarrationSound = SoundName.BackgroundMusic; // Default value
    private float lastNarrationPlayTime = 0.1f; // Ensures first play is unrestricted
    private float narrationCooldown = 2f; // Cooldown in seconds for narration sounds
    private bool IsNarrationSound= false;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        CheckIfMutedMusic();
    }

    private void CheckIfMutedMusic()
    {
       
            MusicSource.mute = MuteMusic;
    }
    public void ToggleMuteMusic() 
    {
        MuteMusic = !MuteMusic;
        CheckIfMutedMusic();
    }

    public void ToggleMuteSounds()
    {
        MuteSounds = !MuteSounds;
    }
    public void PlaySound(SoundType soundType, SoundName soundName, float volume = 1f)
    {
        CheckIfMutedMusic();
      if (MuteSounds && soundType != SoundType.Music) { return; }

        if (soundType == SoundType.Narration)
        {
            // if this happens we want the last narration sound to stop
            if (soundName != SoundName.Game_Won&& soundName != SoundName.Level_Won)
            {
                IsNarrationSound = true;
            }
            if (soundName == lastPlayedNarrationSound && Time.time - lastNarrationPlayTime < narrationCooldown)
            {
                Debug.Log($"Narration sound '{soundName}' skipped due to cooldown.");
                return; // Skip playing if the same sound is within the cooldown
            }

            lastPlayedNarrationSound = soundName;
            lastNarrationPlayTime = Time.time; // Update the last played time
        }
        else
        {
            IsNarrationSound = false;
        }

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

        // Play the sound
        SoundPoolManager.Instance.PlaySound(sound.clip, volume, IsNarrationSound);
       // Debug.Log($"Played sound '{soundName}' of type '{soundType}'.");
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
    UI,
    PlayerSounds
}

/// <summary>
/// Specific names for individual sounds.
/// </summary>
public enum SoundName
{
    BackgroundMusic,
   Bubble_Dad,Bubble_Flag,Bubble_Stone,Level_Won,Level_Lost,Bubble_Spike,Bubble_Bulder,Bubble_Box,Game_Won,
   Click_OnBubble, Erase, Click_OnButton,
   Player_FootStep, Player_JumpStart, Player_Landing, Player_BoxLending,Bubble_Daddy,
}