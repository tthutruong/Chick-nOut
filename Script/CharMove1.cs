using UnityEngine;

public class CharMove1 : MonoBehaviour
{
    [Header("Animation Settings")]
    public Animator characterAnimator;
    public string comeAnimation = "Come";
    public string stayAnimation = "Stay";
    public string goAnimation = "Go";
    public float comeDelay = 15f;
    public float comeDuration = 1f;
    public float goDuration = 1f;
    public ScarySequenceTrigger scarySequenceTrigger;

    [Header("UI Settings")]
    public GameObject handUI;

    [Header("Audio Settings")]
    public AudioClip scaryClip;
    public AudioClip arrivalVoiceClip;    // ← NEW
    public AudioClip thankYouClip;        // ← NEW

    [Header("Debug")]
    public bool debugMode = true;

    private bool hasInteracted = false;
    private bool inReach = false;
    private bool comeCompleted = false;
    private bool stayPlaying = false;
    private bool isExiting = false;
    private bool hasStartedCome = false;
    private Renderer[] characterRenderers;

    private static bool scarySoundPlayed = false;
    private AudioSource audioSource;

    void Start()
    {
        characterRenderers = GetComponentsInChildren<Renderer>();
        SetCharacterVisibility(false);

        if (handUI != null) handUI.SetActive(false);
        Invoke("StartComeAnimation", comeDelay);

        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.playOnAwake = false;
    }

    private void SetCharacterVisibility(bool isVisible)
    {
        if (characterRenderers != null)
        {
            foreach (var renderer in characterRenderers)
            {
                renderer.enabled = isVisible;
            }
        }
    }

    void StartComeAnimation()
    {
        if (hasInteracted || isExiting || hasStartedCome) return;

        hasStartedCome = true;
        if (debugMode) Debug.Log("Come animation started");

        SetCharacterVisibility(true);

        if (characterAnimator != null)
        {
            characterAnimator.Play(comeAnimation);
            Invoke("OnComeComplete", comeDuration);
        }
    }

    void OnComeComplete()
    {
        if (hasInteracted || isExiting) return;

        comeCompleted = true;
        if (debugMode) Debug.Log("Come animation completed, ready for interact");

        // 🎤 Play voice line when character arrives
        if (arrivalVoiceClip != null)
        {
            audioSource.PlayOneShot(arrivalVoiceClip);
        }

        if (characterAnimator != null && !stayPlaying)
        {
            characterAnimator.Play(stayAnimation);
            stayPlaying = true;
        }
    }

    void Update()
    {
        if (isExiting) return;

        if (comeCompleted && !hasInteracted)
        {
            bool shouldShowHandUI = inReach && handUI != null && BagPickup.HasBag;

            if (shouldShowHandUI && !handUI.activeSelf)
            {
                handUI.SetActive(true);
                if (debugMode) Debug.Log("Hand UI activated");
            }
            else if (!shouldShowHandUI && handUI != null && handUI.activeSelf)
            {
                handUI.SetActive(false);
            }
        }

        if (inReach && Input.GetButtonDown("Interact") && !hasInteracted && comeCompleted && BagPickup.HasBag)
        {
            if (debugMode) Debug.Log("Interact detected");
            InteractWithCharacter();
        }
    }

    void InteractWithCharacter()
    {
        if (isExiting) return;

        hasInteracted = true;
        isExiting = true;
        comeCompleted = false;

        if (handUI != null) handUI.SetActive(false);

        if (BagPickup.HasBag && BagPickup.CurrentBag != null)
        {
            BagPickup.CurrentBag.SetActive(false);
            BagPickup.DropBag();
        }

        if (characterAnimator != null)
        {
            characterAnimator.Play(goAnimation);
        }

        // 🎧 Play scary clip (only once)
        if (!scarySoundPlayed && scaryClip != null)
        {
            audioSource.clip = scaryClip;
            audioSource.Play();
            scarySoundPlayed = true;
        }

        // 🎤 Play thank you voice
        if (thankYouClip != null)
        {
            audioSource.PlayOneShot(thankYouClip);
        }

        // Call the scary sequence trigger (fade, sound, text, scene change)
        if (scarySequenceTrigger != null)
        {
            scarySequenceTrigger.TriggerScarySequence();
        }

        // Destroy NPC after exit animation duration
        Destroy(gameObject, goDuration);
    }

    void OnTriggerEnter(Collider other)
    {
        if (isExiting) return;

        if (other.CompareTag("Reach") && !hasInteracted && comeCompleted)
        {
            inReach = true;
            if (debugMode) Debug.Log("Entered Reach zone");
            if (handUI != null && BagPickup.HasBag) handUI.SetActive(true);
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (isExiting) return;

        if (other.CompareTag("Reach"))
        {
            inReach = false;
            if (debugMode) Debug.Log("Exited Reach zone");
            if (handUI != null) handUI.SetActive(false);
        }
    }
}

