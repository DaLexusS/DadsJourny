using UnityEngine;
using UnityEngine.UI;

public class AddOnclick : MonoBehaviour
{
    Button thisbutton;
    SoundManager soundManager;
    [SerializeField] bool MusicButton;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
 
    private void Awake()
    {
        if (thisbutton == null)
        {
            thisbutton = GetComponent<Button>(); // ✅ Auto-assign if it's on the same GameObject
        }
        if (soundManager == null)
        {
            soundManager = GameObject.FindAnyObjectByType<SoundManager>();
        }

        if (thisbutton != null && soundManager != null)
        {
            if (MusicButton)
            {
                thisbutton.onClick.AddListener(soundManager.ToggleMuteMusic);
            }
            else
            {
                thisbutton.onClick.AddListener(soundManager.ToggleMuteSounds);
            }
        }
        else
        {
            Debug.LogError("Button or PlayerMovement reference missing!");
        }
    }
}


