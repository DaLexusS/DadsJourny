using UnityEngine;

public class MusicSomthing : MonoBehaviour
{
   [SerializeField] SoundType soundType;
   [SerializeField] SoundName SoundName;

   /* private void OnDisable()
    {
        SoundManager.Instance.PlaySound(soundType, SoundName, 10f);
        Debug.Log(gameObject.name+"is trying to play ondisable");
    }*/

    public void PlaySound() 
    {
        SoundManager.Instance.PlaySound(soundType, SoundName, 10f);
        Debug.Log(gameObject.name + "is trying to playSound");
    }
}
