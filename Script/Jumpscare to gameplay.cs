using UnityEngine;
using UnityEngine.Video;
using UnityEngine.SceneManagement;

public class AutoSceneVideoPlayerWithJumpscare : MonoBehaviour
{
    public VideoPlayer videoPlayer;
    public string nextSceneName = "7";
    public AudioClip scarySound; // Assign in Inspector

    private AudioSource audioSource;

    void Start()
    {
        if (videoPlayer == null)
        {
            videoPlayer = GetComponent<VideoPlayer>();
        }

        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.playOnAwake = false;

        if (scarySound != null)
        {
            audioSource.clip = scarySound;
            audioSource.Play(); // 🔊 Play audio immediately
        }

        if (videoPlayer != null)
        {
            videoPlayer.loopPointReached += OnVideoEnd;
            videoPlayer.Play();
        }
        else
        {
            Debug.LogError("VideoPlayer not assigned or found.");
        }
    }

    void OnVideoEnd(VideoPlayer vp)
    {
        SceneManager.LoadScene(7);
    }
}



