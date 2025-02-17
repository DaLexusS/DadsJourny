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

    private void OnEnable()
    {
        print("OnEnable -> Getting references and adding OnClick");
        GetReferences();
        AddOnClick();
        UpdateMySprite(); // ✅ Ensure sprite updates on scene load
    }

    private void GetReferences()
    {
        print("Getting references");

        if (ThisButton == null)
            ThisButton = GetComponent<Button>(); // ✅ Auto-assign if on the same GameObject

        if (soundManager == null)
            soundManager = FindObjectOfType<SoundManager>(); // ✅ Auto-find SoundManager in the scene

        if (ThisImage == null)
            ThisImage = GetComponent<Image>(); // ✅ Auto-assign if on the same GameObject
    }

    private void AddOnClick()
    {
        print("Adding OnClick listeners");

        // ✅ Ensure references exist before proceeding
        GetReferences();

        if (ThisButton == null || soundManager == null)
        {
            Debug.LogError("Button or SoundManager reference missing!");
            return;
        }

        // ✅ Log existing listeners before removing
        Debug.Log("Before removing listeners, listener count: " + ThisButton.onClick.GetPersistentEventCount());

        // ✅ Remove previous listeners before adding new ones
        ThisButton.onClick.RemoveAllListeners();

        if (MusicButton)
        {
            ThisButton.onClick.AddListener(soundManager.ToggleMuteMusic);
        }
        else
        {
            ThisButton.onClick.AddListener(soundManager.ToggleMuteSounds);
        }

        ThisButton.onClick.AddListener(UpdateMySprite);

        // ✅ Log after adding listeners
        Debug.Log("After adding listeners, listener count: " + ThisButton.onClick.GetPersistentEventCount());
    }

    private void UpdateMySprite()
    {
        print("Updating sprites");

        if (SoundManager.Instance == null)
        {
            Debug.LogError("SoundManager Instance is null!");
            return;
        }

        bool Muted = MusicButton ? SoundManager.Instance.MuteMusic : SoundManager.Instance.MuteSounds;

        ThisImage.sprite = Muted ? OffSprite : OnSprite;
        ThisButton.targetGraphic = ThisImage;

        // ✅ Log mute state to confirm it's correctly updating
        Debug.Log("Mute state updated - MusicButton: " + MusicButton + " | Muted: " + Muted);
    }
}
