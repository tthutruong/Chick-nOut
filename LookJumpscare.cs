using UnityEngine;
using System.Collections;

public class GazeJumpscare : MonoBehaviour
{
    public GameObject normalObject;
    public GameObject jumpscareObject;
    public float gazeTime = 5f;
    public float scareDuration = 1f;
    public float maxLookAngle = 5f;

    private Camera mainCam;
    private float timer = 0f;
    private bool isScaring = false;

    void Start()
    {
        mainCam = Camera.main;

        if (normalObject != null) normalObject.SetActive(true);
        if (jumpscareObject != null) jumpscareObject.SetActive(false);
    }

    void Update()
    {
        if (isScaring || mainCam == null || normalObject == null || jumpscareObject == null)
            return;

        Vector3 dirToObject = (normalObject.transform.position - mainCam.transform.position).normalized;
        float angle = Vector3.Angle(mainCam.transform.forward, dirToObject);

        if (angle < maxLookAngle)
        {
            timer += Time.deltaTime;

            if (timer >= gazeTime)
            {
                StartCoroutine(DoJumpscare());
            }
        }
        else
        {
            timer = 0f;
        }
    }

    IEnumerator DoJumpscare()
    {
        isScaring = true;

        normalObject.SetActive(false);
        jumpscareObject.SetActive(true);

        yield return new WaitForSeconds(scareDuration);

        jumpscareObject.SetActive(false);
        normalObject.SetActive(true);

        isScaring = false;
        timer = 0f;
    }
}
