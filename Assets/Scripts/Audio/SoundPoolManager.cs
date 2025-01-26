using UnityEngine;

public class SoundPoolManager : MonoBehaviour
{
    public static SoundPoolManager Instance;

    private AudioSource[] soundPool;
    private int poolSize = 50;

    private void Awake()
    {
        Instance = this;
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

    public void PlaySound(AudioClip clip, float volume = 1.0f)
    {
       // Debug.Log($"sound pool Playing sound: {clip.name}, Volume: {volume}");
        if (clip == null) return;

        foreach (AudioSource source in soundPool)
        {
            if (source == null) return;
            if (!source.isPlaying)
                {
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
}
