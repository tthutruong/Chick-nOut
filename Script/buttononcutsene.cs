using UnityEngine;
using UnityEngine.Video;
using UnityEngine.SceneManagement;

public class CutsceneController : MonoBehaviour
{
    public VideoPlayer videoPlayer;
    public GameObject continueButton; // Assign in Inspector
    public string nextSceneName = "6"; // Replace with your scene name

    void Start()
    {
        if (continueButton != null)
            continueButton.SetActive(false); // Hide at start

        if (videoPlayer != null)
        {
            videoPlayer.Play();
            videoPlayer.loopPointReached += OnCutsceneFinished;
        }
    }

    void OnCutsceneFinished(VideoPlayer vp)
    {
        if (continueButton != null)
            continueButton.SetActive(true); // Show button when video ends
    }

    // This is called by the button when clicked
    public void LoadNextScene()
    {
        SceneManager.LoadScene(6);
    }
}
