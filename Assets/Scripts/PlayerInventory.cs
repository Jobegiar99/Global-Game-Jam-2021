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

    /// <summary>
    ///Handles changes to the inventory
    ///<para name = "action">Action to be done in the inventory</para>
    ///<para name = "actionIngredient">The ingredient which will suffer changes</para>
    /// </summary>
    public void UpdateInventory( string action , GameObject actionIngredient )
    {
        switch( action)
        {
            case "Add":
                AddItem(action, actionIngredient);
                break;
            case "Remove":
                RemoveItem(actionIngredient);
                break;
        }
    }

    /// <summary>
    ///Increases the quantity of an ingredient or creates it if it does not exists
    ///<para name = "action">Action to be done in the inventory</para>
    ///<para name = "actionIngredient">The ingredient which will suffer changes</para>
    /// </summary>
    private void AddItem( string action , GameObject actionIngredient )
    {
        bool ingredientExists =PlayerIngredients.Contains( actionIngredient );
        if( ingredientExists)
        {
            PlayerIngredients[ PlayerIngredients.IndexOf( actionIngredient ) ].GetComponent< InventoryIngredient >().ModifyQuantity( action , 1 );
        }
        else
        {
            PlayerIngredients.Add( actionIngredient );
        }
       
    }

    /// <summary>
    ///Removes an item from the inventory.
    ///<para name = "action">Action to be done in the inventory</para>
    ///<para name = "actionIngredient">The ingredient which will suffer changes</para>
    /// </summary>
    private void RemoveItem( GameObject actionIngredient )
    {
        bool ingredientExists = PlayerIngredients.Contains( actionIngredient );
        if( ingredientExists )
        {
            PlayerIngredients.Remove( actionIngredient );
        }
    }

}
