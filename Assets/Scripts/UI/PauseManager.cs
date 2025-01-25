using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PauseManager : MonoBehaviour
{
    public Button pauseButton;
    private bool _isPaused=false;
    public void TogglePause()
    {
        _isPaused = !_isPaused;

        if (_isPaused)
        {
            Time.timeScale = 0;
        }
        else
        {
            Time.timeScale = 1;
        }
    }
}
