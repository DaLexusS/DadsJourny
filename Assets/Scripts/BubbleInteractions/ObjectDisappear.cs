using UnityEngine;

public class ObjectDisappear : MonoBehaviour
{
    [SerializeField] public GameObject obj;
    [SerializeField] private FloatLogic[] balloons;
    public bool IsEnabled = true;
    
    public void ToggleObject()
    {
        IsEnabled = !IsEnabled;
        obj.SetActive(IsEnabled);
        
        if (!IsEnabled)
        {
            foreach (FloatLogic baloon in balloons)
            {
                baloon.EnableFloating();
            }
            
        }
    }
}
