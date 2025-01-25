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
        EraseAnimator.SetTrigger("CallErase");
        Time.timeScale = 1;
        StartCoroutine(WaitAndDoSomething());
    }

    public void LoadNextLevel(int index)
    {
        EraseAnimator.SetTrigger("CallErase");
        StartCoroutine(WaitToLoadNext(index));
    }

    public void LoadFromMenu(int index)
    {
        EraseAnimator.SetTrigger("CallErase");
        StartCoroutine(WaitToLoadNext(index));
    }

    public void BackToMenu()
    {
        EraseAnimator.SetTrigger("CallErase");
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
