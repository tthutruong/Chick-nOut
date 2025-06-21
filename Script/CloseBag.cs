using UnityEngine;

public class CloseBag : MonoBehaviour
{
    public GameObject handUI;
    public GameObject closedBag;
    private bool inReach;

    void UpdateUI()
    {
        // Chỉ hiển thị UI khi đang cầm đồ ăn chín
        handUI.SetActive(inReach && FoodPickUp2.isHoldingCookedFood);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Reach"))
        {
            inReach = true;
            UpdateUI();
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
        if (inReach && Input.GetButtonDown("Interact") && FoodPickUp2.isHoldingCookedFood)
        {
            // Ẩn món đồ ăn đang cầm
            if (FoodPickUp.currentPlate != null)
            {
                FoodPickUp.currentPlate.SetActive(false);
                FoodPickUp.currentPlate = null;
                // Không cần gán FoodType.None, chỉ cần đặt currentHeldFoodType thành giá trị mặc định (Balls)
                FoodPickUp.currentHeldFoodType = FoodIdentity.FoodType.Balls; // Hoặc không gán gì cả
                FoodPickUp2.isHoldingCookedFood = false;
            }

            // Đóng túi
            handUI.SetActive(false);
            if (closedBag != null) closedBag.SetActive(true);
            
            // Kích hoạt lại spawn nếu có BagRefill
            var refill = FindAnyObjectByType<BagRefill>();
            if (refill != null) refill.EnableSpawning();
            
            gameObject.SetActive(false);
        }
    }
}