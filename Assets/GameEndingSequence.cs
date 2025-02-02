using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using UnityEngine.Video;

public class GameEndingSequence : MonoBehaviour
{
    public VideoPlayer EndingVid;
    public Image fadeImage; // UI Image for fading effect
    public float fadeDuration = 2f; // Adjust fade speed
    public UnityEvent onEndingComplete; // Invoked when the ending sequence is done

    public void StartEndingSequence()
    {
        // ✅ Activate and play the video
        EndingVid.gameObject.SetActive(true);
        EndingVid.Play();

        // ✅ Start fading out after video starts
        StartCoroutine(FadeOutVideo());
    }

    private IEnumerator FadeOutVideo()
    {
        float timer = 0f;
        Color fadeColor = fadeImage.color;

        while (timer < fadeDuration)
        {
            timer += Time.deltaTime;
            fadeColor.a = timer / fadeDuration; // Gradually increase transparency
            fadeImage.color = fadeColor;
            yield return null;
        }

        //  Ensure video is fully faded out
        EndingVid.Stop();
        EndingVid.gameObject.SetActive(false);

        // Notify EndLevel script that the ending is done
        onEndingComplete.Invoke();
    }
}
