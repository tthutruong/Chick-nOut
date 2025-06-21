using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;
using UnityEngine.SceneManagement;
using System.Collections;

public class CutsceneSceneController : MonoBehaviour
{
    public CanvasGroup fadeCanvas;
    public VideoPlayer videoPlayer;
    public float fadeDuration = 2f;
    public string nextSceneName = "4"; // Or your next scene

    void Start()
    {
        StartCoroutine(PlayCutsceneSequence());
    }

    private IEnumerator PlayCutsceneSequence()
    {
        // Make sure canvas is fully black at start
        fadeCanvas.alpha = 1f;
        fadeCanvas.gameObject.SetActive(true);

        // Small delay for loading safety
        yield return new WaitForSeconds(0.5f);

        // Fade in from black
        yield return StartCoroutine(FadeCanvas(1f, 0f, fadeDuration));

        // Play cutscene
        if (videoPlayer != null)
        {
            videoPlayer.Play();

            // Wait for video to finish
            while (videoPlayer.isPlaying)
                yield return null;
        }

        // Optional: fade to black after video
        yield return StartCoroutine(FadeCanvas(0f, 1f, fadeDuration));

        // Then load the next scene
        SceneManager.LoadScene(4);
    }

    private IEnumerator FadeCanvas(float from, float to, float duration)
    {
        float elapsed = 0f;
        fadeCanvas.alpha = from;

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            fadeCanvas.alpha = Mathf.Lerp(from, to, elapsed / duration);
            yield return null;
        }

        fadeCanvas.alpha = to;
    }
}
