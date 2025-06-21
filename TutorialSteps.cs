using UnityEngine;
using TMPro;

public class TutorialStepsUI_TMP : MonoBehaviour
{
    public TMP_Text tutorialText;        // Drag your TextMeshProUGUI object here
    public GameObject customerObject;    // Drag your customer NPC object here

    void Update()
    {
        if (customerObject == null && tutorialText != null && tutorialText.enabled)
        {
            tutorialText.enabled = false; // Hide the text when the customer is gone
        }
    }
}
