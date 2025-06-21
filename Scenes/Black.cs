using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;
using TMPro;

public class ScarySequenceTrigger : MonoBehaviour
{
    public AudioClip scaryAudioClip;
    public AudioClip extraScaryClip;
    public CanvasGroup fadeCanvas;
    public TextMeshProUGUI scaryText;
    public string nextSceneName = "3";
    public float fadeDuration = 2f;
    public float displayTime = 3f;

    private AudioSource audioSource;

    void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }
        audioSource.playOnAwake = false;
        audioSource.loop = false;
    }

    public void TriggerScarySequence()
    {
        StartCoroutine(PlaySequence());
    }

    private IEnumerator PlaySequence()
    {
        Debug.Log("Scary sequence started.");

        // Start fade to black and fade in audio
        scaryText.text = "Why do I feel like this? I feel...wrong? Is this normal? I think I may be going insane. Although, they do taste good, why not share?";
        scaryText.enabled = true;

        Debug.Log("Fading to black and fading in audio...");
        StartCoroutine(FadeAudio(0f, 1f, fadeDuration));
        yield return StartCoroutine(FadeCanvas(0, 1, fadeDuration));

        if (scaryAudioClip != null)
        {
            Debug.Log("Playing scary audio...");
            audioSource.clip = scaryAudioClip;
            audioSource.volume = 1f; // ensure volume is 100%
            audioSource.Play();
        }

        yield return new WaitForSeconds(displayTime);

        if (extraScaryClip != null)
        {
            Debug.Log("Playing extra scary audio...");
            audioSource.clip = extraScaryClip;
            audioSource.Play();
        }

        Debug.Log("Fading out black and audio...");
        StartCoroutine(FadeAudio(1f, 0f, fadeDuration));
        yield return StartCoroutine(FadeCanvas(1, 0, fadeDuration));

        scaryText.enabled = false;

        Debug.Log("Loading next scene...");
        SceneManager.LoadScene(3);
    }

    private IEnumerator FadeCanvas(float from, float to, float duration)
    {
        fadeCanvas.alpha = from;
        fadeCanvas.gameObject.SetActive(true);

        float elapsed = 0f;
        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            fadeCanvas.alpha = Mathf.Lerp(from, to, elapsed / duration);
            yield return null;
        }

        fadeCanvas.alpha = to;

        if (to == 0)
            fadeCanvas.gameObject.SetActive(false);
    }

    private IEnumerator FadeAudio(float fromVolume, float toVolume, float duration)
    {
        float elapsed = 0f;
        audioSource.volume = fromVolume;

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            audioSource.volume = Mathf.Lerp(fromVolume, toVolume, elapsed / duration);
            yield return null;
        }

        audioSource.volume = toVolume;
    }
}
