using UnityEngine;
using UnityEngine.Video;
using UnityEngine.SceneManagement; // If switching scenes

public class CutsceneToGameplay : MonoBehaviour
{
    public VideoPlayer videoPlayer;
    public string gameplaySceneName = "2"; // OR activate gameplay GameObjects

    void Start()
    {
        videoPlayer.loopPointReached += OnVideoEnd;
    }

    void OnVideoEnd(VideoPlayer vp)
    {
        // Option 1: Load another scene
        SceneManager.LoadScene(2);

        // Option 2: Activate gameplay
        // GameObject.Find("GameplayRoot").SetActive(true);
        // gameObject.SetActive(false); // Disable the cutscene player
    }
}

