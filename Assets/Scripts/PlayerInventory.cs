using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    /// <summary>
    /// List of all the ingredients that the player has.
    /// </summary>
    private List<GameObject> PlayerIngredients;

    private void Start()
    {
        PlayerIngredients = new List<GameObject>();

    }

    public void UpdateInventory( string action , GameObject actionIngredient )
    {
        switch( action)
        {
            case "Add":
                break;
            case "Remove":
                break;
        }
    }


    private void AddItem( GameObject actionIngredient )
    {
        bool ingredientExists =PlayerIngredients.Contains( actionIngredient );
        if( ingredientExists)
        {
            PlayerIngredients[PlayerIngredients.IndexOf(actionIngredient)].GetComponent<InventoryIngredient>().ModifyQuantity("Add", 1);
        }
        else
        {
            PlayerIngredients.Add(actionIngredient);
        }
       
    }

    private void RemoveItem(GameObject actionIngredient)
    {
        bool ingredientExists = PlayerIngredients.Contains(actionIngredient);
        if (ingredientExists)
        {
            PlayerIngredients.Remove(actionIngredient);
        }
    }

}
