using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/InventoryIngredient", order = 1)]
public class InventoryIngredient : ScriptableObject
{
    // Start is called before the first frame update
    public GameIngredient Ingredient;

    private int Quantity = 1;

    public void ModifyQuantity(string action, int magnitude)
    {
        switch (action)
        {
            case "Add":
                Quantity += magnitude;
                break;

            case "Remove":
                if (Quantity - magnitude >= 0)
                    Quantity -= magnitude;
                break;
        }
    }
}