using UnityEngine;
using UnityEngine.Video;
using UnityEngine.SceneManagement;
using System.Collections;

public class SceneAutoLoaderAfterVideo : MonoBehaviour
{
    public string nextSceneName = "5";        // Scene to load after video
    public VideoPlayer videoPlayer;           // Assign via Inspector
    public AudioClip cutsceneAudio;           // Assign your audio clip
    public float fadeDuration = 1.5f;         // How long the audio fade lasts

    private AudioSource audioSource;

    void Start()
    {
        if (videoPlayer == null)
        {
            videoPlayer = GetComponent<VideoPlayer>();
        }

        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.playOnAwake = false;
        audioSource.loop = false;

        if (cutsceneAudio != null)
        {
            audioSource.clip = cutsceneAudio;
            audioSource.Play();
        }

        if (videoPlayer != null)
        {
            videoPlayer.loopPointReached += OnVideoEnd;
            videoPlayer.Play();
        }
        else
        {
            Debug.LogError("No VideoPlayer assigned or found on this GameObject.");
        }
    }

    void OnVideoEnd(VideoPlayer vp)
    {
        StartCoroutine(FadeOutAudioAndLoadScene());
    }

    private IEnumerator FadeOutAudioAndLoadScene()
    {
        float startVolume = audioSource.volume;

        float elapsed = 0f;
        while (elapsed < fadeDuration)
        {
            elapsed += Time.deltaTime;
            audioSource.volume = Mathf.Lerp(startVolume, 0f, elapsed / fadeDuration);
            yield return null;
        }

        audioSource.Stop();
        SceneManager.LoadScene(5);
    }
}



