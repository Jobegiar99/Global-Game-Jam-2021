using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryIngredient
{
    // Start is called before the first frame update
    public GameIngredient Ingredient;

    private int Quantity = 1;

    public InventoryIngredient(GameIngredient ingredient, int startAmount = 1)
    {
        this.Ingredient = ingredient;
        this.Quantity = startAmount;
    }

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