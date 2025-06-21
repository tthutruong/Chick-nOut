using UnityEngine;
using UnityEngine.SceneManagement;

public class WardrobeJumpscareTrigger : MonoBehaviour
{
    public string jumpscareSceneName = "6"; // Put your jumpscare scene name here
    private bool triggered = false;

    private void OnTriggerEnter(Collider other)
    {
        if (triggered) return; // only trigger once

        if (other.CompareTag("Player"))
        {
            triggered = true;
            SceneManager.LoadScene(6);
        }
    }
}
