using UnityEngine;
using UnityEngine.Video;
using UnityEngine.SceneManagement;

public class SimpleCutsceneAutoPlay : MonoBehaviour
{
    public VideoPlayer videoPlayer;
    public string nextSceneName = "6"; // Put your gameplay scene name here

    void Start()
    {
        if (videoPlayer != null)
        {
            videoPlayer.loopPointReached += OnVideoFinished;
            videoPlayer.Play();
        }
        else
        {
            Debug.LogError("VideoPlayer is not assigned!");
            LoadNextScene();
        }
    }

    void OnVideoFinished(VideoPlayer vp)
    {
        LoadNextScene();
    }

    void LoadNextScene()
    {
        SceneManager.LoadScene(6);
    }
}
