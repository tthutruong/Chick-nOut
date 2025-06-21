using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class AutoSceneFade : MonoBehaviour
{
    public CanvasGroup fadeCanvas;             // Drag your FadeCanvas here
    public AudioClip scarySound;               // Drag your scary sound clip here
    public float fadeDuration = 2f;
    public float delayBeforeFade = 1f;
    public string nextScene = "8"; // Replace with actual scene name

    private AudioSource audioSource;

    void Start()
    {
        if (fadeCanvas != null)
        {
            fadeCanvas.alpha = 0f;
            fadeCanvas.gameObject.SetActive(true);
        }

        audioSource = gameObject.AddComponent<AudioSource>();
        StartCoroutine(FadeAndLoad());
    }

    IEnumerator FadeAndLoad()
    {
        yield return new WaitForSeconds(delayBeforeFade);

        // Play sound
        if (scarySound != null)
        {
            audioSource.PlayOneShot(scarySound);
        }

        // Fade to black
        float elapsed = 0f;
        while (elapsed < fadeDuration)
        {
            elapsed += Time.deltaTime;
            fadeCanvas.alpha = Mathf.Clamp01(elapsed / fadeDuration);
            yield return null;
        }

        fadeCanvas.alpha = 1f;

        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene(8); // Must be in Build Settings!
    }
}
