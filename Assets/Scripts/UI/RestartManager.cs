using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Events;
using System.Collections;

public class RestartManager : MonoBehaviour
{
    [SerializeField] RawImage eraseEffect;
    private Animator EraseAnimator;
    private float waitTime = 1f;
    private void Awake()
    {
        DeathOnTouch.onPlayerDeath += RestartScene;
        EraseAnimator = eraseEffect.GetComponent<Animator>();
    }

    private void OnDestroy()
    {
        DeathOnTouch.onPlayerDeath -= RestartScene;
    }
    public void RestartScene()
    {
        EraseAnimator.SetTrigger("CallErase");
        Time.timeScale = 1;
        StartCoroutine(WaitAndDoSomething());
    }

    IEnumerator WaitAndDoSomething()
    {
        yield return new WaitForSeconds(waitTime);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

}
