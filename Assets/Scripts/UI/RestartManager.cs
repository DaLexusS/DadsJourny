using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Events;
using System.Collections;
using Unity.VisualScripting;

public class RestartManager : MonoBehaviour
{
    [SerializeField] GameObject eraseEffect;
    [SerializeField] RawImage rawImage;
    private Color rawImageComponnet;
    private Animator EraseAnimator;
    private float waitTime = 1.3f;
    
    private void Awake()
    {
        EndLevel.callNextLevel += LoadNextLevel;
        DeathOnTouch.onPlayerDeath += RestartScene;
        LevelMenu.loadPressedLevel += LoadFromMenu;

        EraseAnimator = eraseEffect.GetComponent<Animator>();
        rawImageComponnet = rawImage.GetComponent<RawImage>().color;
        rawImageComponnet.a = 1f;
    }

    private void Start()
    {
        EraseAnimator.SetTrigger("OnSceneLoad");
    }

    private void OnDestroy()
    {
        DeathOnTouch.onPlayerDeath -= RestartScene;
        EndLevel.callNextLevel -= LoadNextLevel;
        LevelMenu.loadPressedLevel -= LoadFromMenu;
    }
    public void RestartScene()
    {
        SoundManager.Instance.PlaySound(SoundType.Narration, SoundName.Level_Lost, 0.5f);
        SoundManager.Instance.PlaySound(SoundType.UI, SoundName.Erase, 2);
        
        EraseAnimator.SetTrigger("CallErase");
        Time.timeScale = 1;
        StartCoroutine(WaitAndDoSomething());
    }

    public void LoadNextLevel(int index)
    {
        SoundManager.Instance.PlaySound(SoundType.UI, SoundName.Erase, 0.8f);
        SoundManager.Instance.PlaySound(SoundType.Narration , SoundName.Level_Won, 1);
        EraseAnimator.SetTrigger("CallErase");
        StartCoroutine(WaitToLoadNext(index));
    }

    public void LoadFromMenu(int index)
    {
        EraseAnimator.SetTrigger("CallErase");
        SoundManager.Instance.PlaySound(SoundType.UI, SoundName.Erase, 3);
        StartCoroutine(WaitToLoadNext(index));
    }

    public void BackToMenu()
    {
        EraseAnimator.SetTrigger("CallErase");
        SoundManager.Instance.PlaySound(SoundType.UI, SoundName.Erase, 3);
        Time.timeScale = 1;
        StartCoroutine(WaitAndDoSomething2());
    }
    IEnumerator WaitAndDoSomething()
    {
        yield return new WaitForSeconds(waitTime);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    IEnumerator WaitAndDoSomething2()
    {
        yield return new WaitForSeconds(waitTime);
        SceneManager.LoadScene(0);
    }

    IEnumerator WaitToLoadNext(int index)
    {
        yield return new WaitForSeconds(waitTime);
        SceneManager.LoadScene(index);
    }
}
