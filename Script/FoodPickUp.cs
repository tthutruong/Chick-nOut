using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodPickUp : MonoBehaviour
{
    public GameObject handUI;
    public GameObject plate;
    public FoodIdentity.FoodType plateFoodType; 

    private bool inReach;
    public static GameObject currentPlate;
    public static FoodIdentity.FoodType currentHeldFoodType; 

    void Start()
    {
        handUI.SetActive(false);
        if (plate != null)
        {
            plate.SetActive(false);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Reach")
        {
            inReach = true;
            handUI.SetActive(true);
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Reach")
        {
            inReach = false;
            handUI.SetActive(false);
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && currentPlate != null)
        {
            currentPlate.SetActive(false);
            currentPlate = null;
            currentHeldFoodType = plate != null ? plateFoodType : FoodIdentity.FoodType.Balls;
            return;
        }

        if (inReach && Input.GetButtonDown("Interact"))
        {
            if (currentPlate != null)
            {
                currentPlate.SetActive(false);
                currentHeldFoodType = FoodIdentity.FoodType.Balls; 
            }
            
            if (plate != null && currentPlate != plate)
            {
                plate.SetActive(true);
                currentPlate = plate;
                currentHeldFoodType = plateFoodType;
            }
            else
            {
                currentPlate = null;
                currentHeldFoodType = FoodIdentity.FoodType.Balls; 
            }
            
            handUI.SetActive(false);
        }
    }
}