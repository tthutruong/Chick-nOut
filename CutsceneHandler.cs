using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

public class CutsceneHandler : MonoBehaviour
{
    public VideoPlayer videoPlayer;
    public string nextScene = "10"; // Change to your scene name (e.g., "MainMenu")

    void Start()
    {
        if (videoPlayer == null)
        {
            Debug.LogError("VideoPlayer not assigned!");
            return;
        }

        videoPlayer.loopPointReached += OnVideoFinished;
        videoPlayer.Play();
    }

    private void OnVideoFinished(VideoPlayer vp)
    {
        videoPlayer.loopPointReached -= OnVideoFinished;
        SceneManager.LoadScene(10);
    }
}
