using UnityEngine;

public class MusicSomthing : MonoBehaviour
{
   [SerializeField] SoundType soundType;
   [SerializeField] SoundName SoundName;

    private void OnDisable()
    {
        SoundManager.Instance.PlaySound(soundType, SoundName, 10f);
    }
}
