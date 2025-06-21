using UnityEngine;

public class AmbiencePlayer : MonoBehaviour
{
    public AudioClip ambienceClip;     // Assign your ambient audio clip
    [Range(0f, 1f)]
    public float volume = 0.5f;

    private AudioSource audioSource;

    void Awake()
    {
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.clip = ambienceClip;
        audioSource.volume = volume;
        audioSource.loop = true;
        audioSource.playOnAwake = true;

        if (ambienceClip != null)
        {
            audioSource.Play();
        }
        else
        {
            Debug.LogWarning("No ambience clip assigned to AmbiencePlayer.");
        }
    }
}

