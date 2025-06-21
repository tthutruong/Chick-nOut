using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

public class SimpleCutscenePlayer : MonoBehaviour
{
    public VideoPlayer cutsceneVideo;
    public string gameplaySceneName = "10"; // Set your next scene name here

    void Awake()
    {
        if (cutsceneVideo == null)
        {
            Debug.LogError("Cutscene VideoPlayer not assigned!");
            return;
        }

        cutsceneVideo.loopPointReached += OnCutsceneEnd;
        cutsceneVideo.Play();
    }

    private void OnCutsceneEnd(VideoPlayer vp)
    {
        // Unsubscribe to avoid potential memory leaks
        cutsceneVideo.loopPointReached -= OnCutsceneEnd;

        SceneManager.LoadScene(10);
    }
}

