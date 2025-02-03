using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Video;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameEndingSequence : MonoBehaviour
{
    public VideoPlayer EndingVid;
    public RawImage EndingVidDisplayTexture; // UI Image that shows the video
    public RenderTexture EndingVideoTexture; // Assigned in the Inspector
    public UnityEvent onEndingComplete; // Event to trigger scene transition

    public void StartEndingSequence()
    {
        // ✅ Pause the game
        Time.timeScale = 0;

        // ✅ Assign the Render Texture (Prevents blank screen issues)
        EndingVid.targetTexture = EndingVideoTexture;
        EndingVidDisplayTexture.texture = EndingVideoTexture;

        // ✅ Ensure the `RawImage` is fully hidden
        EndingVidDisplayTexture.gameObject.SetActive(false);

        // ✅ Set first frame ready events
        EndingVid.sendFrameReadyEvents = true;
        EndingVid.frameReady += OnFirstFrameReady;

        // ✅ Prepare the video
        EndingVid.Prepare();

        // ✅ Start waiting for the video to load
        StartCoroutine(PrepareAndPlayVideo());
    }

    private void OnFirstFrameReady(VideoPlayer source, long frameIdx)
    {
        // ✅ Ensure the first frame is properly assigned before making the video visible
        EndingVidDisplayTexture.gameObject.SetActive(true);

        // ✅ Remove event listener after the first frame is ready
        EndingVid.frameReady -= OnFirstFrameReady;
    }

    private IEnumerator PrepareAndPlayVideo()
    {
        // ✅ Wait until the video is fully prepared
        while (!EndingVid.isPrepared)
        {
            yield return null;
        }

        // ✅ Ensure the video starts from the beginning
        EndingVid.time = 0;
        EndingVid.frame = 0;

        // ✅ Ensure Unity fully updates before making the video visible
        yield return new WaitForEndOfFrame();

        // ✅ Play the video
        EndingVid.Play();

        // ✅ Now wait for it to finish
        StartCoroutine(WaitForVideoToEnd());
    }

    private IEnumerator WaitForVideoToEnd()
    {
        // ✅ Wait until the video actually starts playing
        while (!EndingVid.isPlaying)
        {
            yield return null;
        }

        // ✅ Wait until the video finishes
        while (EndingVid.isPlaying)
        {
            yield return null;
        }

        // ✅ Stop video properly and clear the screen
        EndingVid.Stop();
        EndingVid.targetTexture.Release(); // Clears the video display
        EndingVidDisplayTexture.gameObject.SetActive(false);

        // ✅ Hide the video object safely
        EndingVid.gameObject.SetActive(false);

        // ✅ Unpause the game
        Time.timeScale = 1;

        // ✅ Ensure the game moves to the main menu
        if (onEndingComplete != null)
        {
            onEndingComplete.Invoke();
        }
        else
        {
            Debug.LogWarning("onEndingComplete event is not set! Manually loading Scene 0.");
            SceneManager.LoadScene(0); // ✅ Fallback to manually load the main menu
        }
    }
}
