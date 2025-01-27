using UnityEngine;

public class SoundPoolManager : MonoBehaviour
{
    public static SoundPoolManager Instance;

    private AudioSource[] soundPool;
    private int poolSize = 50;
    AudioClip LastNarrationPlayed; // LIST OF PLAYED NARRATIONS

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
        InitializePool();
    }
    private void InitializePool()
    {
        soundPool = new AudioSource[poolSize];

        for (int i = 0; i < poolSize; i++)
        {
            AudioSource source = transform.gameObject.AddComponent<AudioSource>();
            source.playOnAwake = false;

            soundPool[i] = source;
        }
    }

    public void PlaySound(AudioClip clip, float volume = 1.0f, bool IsNarrationSound = false)
    {
        // Debug.Log($"sound pool Playing sound: {clip.name}, Volume: {volume}");
        if (clip == null) return;

        foreach (AudioSource source in soundPool)
        {
            if (source == null) return;
            if (!source.isPlaying)
            {
                HandleNarrationOverlap(clip, IsNarrationSound);

                source.clip = clip;
                source.volume = volume;
                source.Play();
                return;
            }
        }
        if (soundPool.Length > 0)
        {
            soundPool[0].Stop();
            soundPool[0].clip = clip;
            soundPool[0].volume = volume;
            soundPool[0].Play();
        }

    }

    private void HandleNarrationOverlap(AudioClip clip, bool IsNarrationSound)
    {
        if (IsNarrationSound)
        {
            foreach (AudioSource i in soundPool)
            {
                if (i.clip == LastNarrationPlayed)
                {
                    i.Stop();
                }
            }
            LastNarrationPlayed = clip;
        }
    }
}
