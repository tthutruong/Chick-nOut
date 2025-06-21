using UnityEngine;
using UnityEngine.SceneManagement;

public class AutoSceneTransition : MonoBehaviour
{
    [Tooltip("Scene name to load after delay")]
    public string sceneToLoad = "6";

    [Tooltip("Delay before scene loads (in seconds)")]
    public float delay = 3f;

    private void Start()
    {
        Invoke("6", delay);
    }

    private void LoadNextScene()
    {
        SceneManager.LoadScene(6);
    }
}

