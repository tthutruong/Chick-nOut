using UnityEngine;

public class BagPickup : MonoBehaviour
{
    public GameObject bagUI; // Cũng chính là túi vật lý player cầm
    private bool inReach;
    public static bool HasBag { get; private set; }
    public static GameObject CurrentBag { get; private set; }

    void Start()
    {
        if (bagUI != null) bagUI.SetActive(false);
        HasBag = false;
        CurrentBag = null;
    }
void Update()
{
    if (inReach && Input.GetButtonDown("Interact") && !HasBag)
    {
        PickUpBag();
    }
}

void OnTriggerEnter(Collider other)
{
    if (other.CompareTag("Reach"))
    {
        inReach = true;
    }
}

void OnTriggerExit(Collider other)
{
    if (other.CompareTag("Reach"))
    {
        inReach = false;
    }
}
    void PickUpBag()
{
    if (bagUI != null) 
    {
        bagUI.SetActive(true);
        CurrentBag = bagUI;
        HasBag = true; // Đặt HasBag = true ngay khi nhặt
    }
    gameObject.SetActive(false);
}

    public static void DropBag()
    {
        HasBag = false;
        if (CurrentBag != null)
        {
            CurrentBag.SetActive(false); // Ẩn túi UI/vật lý
            CurrentBag = null;
        }
    }
}