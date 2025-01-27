using UnityEngine;

public class ObjectDisappear : MonoBehaviour
{
    [SerializeField] public GameObject obj;
    [SerializeField] private FloatLogic[] balloons;
    [SerializeField] private MusicSomthing DisableSound;
    public bool IsEnabled = true;
    
    public void ToggleObject()
    {
        IsEnabled = !IsEnabled;
        if (!IsEnabled)
        {
            if (DisableSound)
            {
                DisableSound.PlaySound();
            }
            else
            {
                Debug.LogError("object dissapear had no musicsomthig refrence, from "+ gameObject.name);
            }
           if (balloons.Length > 0) { 
            foreach (FloatLogic baloon in balloons)
            {
                baloon.EnableFloating();
            }
            }
        }

        obj.SetActive(IsEnabled);
        
       
    }
}
