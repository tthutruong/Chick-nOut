using UnityEngine;
using System.Collections.Generic;

public class FoodPickUp2 : MonoBehaviour
{
    public GameObject handUI;
    public List<GameObject> cookedFoods;
    private bool inReach;
    private Dictionary<FoodIdentity.FoodType, GameObject> cookedFoodDictionary = new Dictionary<FoodIdentity.FoodType, GameObject>();

    public static bool isHoldingCookedFood { get; set; }  
    private HashSet<FoodIdentity.FoodType> receivedFoods = new HashSet<FoodIdentity.FoodType>();

    void Start()
    {
        if (handUI != null) handUI.SetActive(false);
        isHoldingCookedFood = false;
        
        foreach (var food in cookedFoods)
        {
            var foodIdentity = food.GetComponent<FoodIdentity>();
            if (foodIdentity != null)
            {
                cookedFoodDictionary.Add(foodIdentity.foodType, food);
                food.SetActive(false);
            }
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Reach"))
        {
            inReach = true;
            // Hiển thị UI khi đang cầm bất kỳ đồ ăn nào (bao gồm cả Balls)
            if (FoodPickUp.currentPlate != null && handUI != null)
            {
                handUI.SetActive(true);
            }
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Reach"))
        {
            inReach = false;
            if (handUI != null) handUI.SetActive(false);
        }
    }

    void Update()
    {
        // Cập nhật trạng thái đồ chín đang cầm
        isHoldingCookedFood = (FoodPickUp.currentPlate != null && 
                             FoodPickUp.currentPlate.GetComponent<FoodIdentity>() != null &&
                             FoodPickUp.currentPlate.GetComponent<FoodIdentity>().foodState == FoodIdentity.FoodState.Cooked);

        if (inReach && Input.GetButtonDown("Interact") && FoodPickUp.currentPlate != null)
        {
            var foodIdentity = FoodPickUp.currentPlate.GetComponent<FoodIdentity>();
            
            // Kiểm tra nếu đồ ăn đang cầm là raw và có trong dictionary hoặc là Balls
            if (foodIdentity != null && foodIdentity.foodState == FoodIdentity.FoodState.Raw)
            {
                FoodIdentity.FoodType currentType = foodIdentity.foodType;
                
                // Xử lý cho tất cả loại đồ ăn, bao gồm cả Balls
                if (cookedFoodDictionary.TryGetValue(currentType, out GameObject cookedFood))
                {
                    // Ẩn đồ sống
                    FoodPickUp.currentPlate.SetActive(false);
                    
                    // Hiển thị đồ chín tương ứng
                    cookedFood.SetActive(true);
                    FoodPickUp.currentPlate = cookedFood;
                    
                    // Cập nhật trạng thái foodState thành Cooked
                    var cookedFoodIdentity = cookedFood.GetComponent<FoodIdentity>();
                    if (cookedFoodIdentity != null)
                    {
                        cookedFoodIdentity.foodState = FoodIdentity.FoodState.Cooked;
                    }
                    
                    // Thêm vào danh sách đã nhận
                    receivedFoods.Add(currentType);
                    
                    // Cập nhật loại đồ ăn đang cầm
                    FoodPickUp.currentHeldFoodType = currentType;
                    isHoldingCookedFood = true;
                }
            }
            
            if (handUI != null) handUI.SetActive(false);
        }
    }

    public bool HasReceivedFood(FoodIdentity.FoodType foodType)
    {
        return receivedFoods.Contains(foodType);
    }

    public void ClearReceivedFoods()
    {
        receivedFoods.Clear();
    }
}