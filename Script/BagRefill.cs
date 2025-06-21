using UnityEngine;

public class BagRefill : MonoBehaviour
{
    public GameObject handUI;
    public GameObject bagModel; // Túi giấy trong scene
    public GameObject closedBag; // Túi đóng trong scene
    public GameObject pickupBag; // Túi có thể nhặt trong scene

    private bool inReach;
    private bool canSpawn = true;

    void Start()
    {
        handUI.SetActive(false);
        if (bagModel != null) bagModel.SetActive(true);
        if (closedBag != null) closedBag.SetActive(false);
        if (pickupBag != null) pickupBag.SetActive(false);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Reach"))
        {
            inReach = true;
            handUI.SetActive(true);
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Reach"))
        {
            inReach = false;
            handUI.SetActive(false);
        }
    }

    void Update()
    {
        if (inReach && Input.GetButtonDown("Interact") && canSpawn)
        {
            handUI.SetActive(false);
            ActivateBagSystem();
        }
    }

    void ActivateBagSystem()
    {
        canSpawn = false;
        if (bagModel != null) bagModel.SetActive(true);
    }

    public void EnableSpawning()
    {
        canSpawn = true;
    }
}