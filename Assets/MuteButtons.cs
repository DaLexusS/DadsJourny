using UnityEngine;
using UnityEngine.UI;

public class MuteButtons : MonoBehaviour
{
    Button ThisButton;
    Image ThisImage;

    SoundManager soundManager;
    [SerializeField] bool MusicButton;
    [SerializeField] Sprite OffSprite;
    [SerializeField] Sprite OnSprite;


    // Start is called once before the first execution of Update after the MonoBehaviour is created

    private void OnEnable()
    {
        print("onenable -> getting refrences and adding onclick");
        GetRefrences();
        AddOnclick();
    }
    private void Start()
    {
        UpdateMySprite();
    }
    private void UpdateMySprite()
    {
        print("updating sprites");

        bool Muted = SoundManager.Instance.MuteSounds;
        if (MusicButton)
        {
             Muted = SoundManager.Instance.MuteMusic;
        }
        



        if (Muted)
        {
            ThisImage.sprite = OffSprite;
        }
        else
        {
            ThisImage.sprite = OnSprite;
        }

        ThisButton.targetGraphic = ThisImage;


    }

    private void AddOnclick()
    {
        print("Adding Onclick");
        GetRefrences();
        ThisButton.onClick.RemoveAllListeners();

        if (ThisButton != null && soundManager != null)
        {
            if (MusicButton)
            {
                ThisButton.onClick.AddListener(soundManager.ToggleMuteMusic);
            }
            else
            {
                ThisButton.onClick.AddListener(soundManager.ToggleMuteSounds);
            }
            ThisButton.onClick.AddListener(this.UpdateMySprite);
        }
        else
        {
            Debug.LogError("Button or PlayerMovement reference missing!");
        }

    }

    private void GetRefrences()
    {
        print("getting refrences");
     
            ThisButton = GetComponent<Button>(); // ✅ Auto-assign if it's on the same GameObject
        
        if (soundManager == null)
        
            soundManager = GameObject.FindAnyObjectByType<SoundManager>();
        
        if (ThisImage== null)
        
            ThisImage = GetComponent<Image>(); // ✅ Auto-assign if it's on the same GameObject

        
    }
}


