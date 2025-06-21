using UnityEngine;

public class FoodIdentity : MonoBehaviour
{
    public enum FoodType { Balls, Tender, Popcorn, Nugget }
    public FoodType foodType;

    public enum FoodState { Raw, Cooked }
    public FoodState foodState;

    public GameObject cookedVersion; 
}