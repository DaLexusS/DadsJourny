using UnityEngine;

public class ObjectDisappear : MonoBehaviour
{
    [SerializeField] public GameObject obj;
    public bool IsEnabled = true;
    
    public void ToggleObject()
    {
        IsEnabled = !IsEnabled;
        obj.SetActive(IsEnabled);
    }
}
