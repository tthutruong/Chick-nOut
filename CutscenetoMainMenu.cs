using UnityEngine;
using UnityEngine.Video;
using UnityEngine.SceneManagement;

public class CutsceneToMainMenu : MonoBehaviour
{
    public VideoPlayer cutscenePlayer;      // Drag your VideoPlayer here
    public string mainMenuSceneName = "0";  // Replace with your actual menu scene name

    void Start()
    {
        if (cutscenePlayer == null)
        {
            cutscenePlayer = GetComponent<VideoPlayer>();
        }

        cutscenePlayer.loopPointReached += OnCutsceneEnd;
    }

    void OnCutsceneEnd(VideoPlayer vp)
    {
        SceneManager.LoadScene(0);
    }
}
