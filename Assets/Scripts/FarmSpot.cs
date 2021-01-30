﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FarmSpot : MonoBehaviour
{
    /// <summary>
    /// The status of the spot
    /// </summary>
    public string Status;
    /// <summary>
    /// The sprites used to give the player an idea of the status of the planted ingredient
    /// </summary>
    public List<Sprite> StatusSprite;
    
    public InventoryIngredient PlantedIngredient;
    // Start is called before the first frame update
    void Start()
    {
        Status = "Empty";
        PlantedIngredient = null;

    }


    /// <summary>
    /// Handles the player clicking on the spot
    /// </summary>
    public void OnMouseDown()
    {
        switch (Status)
        {
            case "Empty":
                //open inventory to plant item
                PlantedIngredient = null;//obtain inventory item
                Status = "Growing";
                StartCoroutine(GrowIngredient());
                break;

            case "Ready":

                Status = "Empty";
                GameObject.Find("PlayerInventory").GetComponent<PlayerInventory>().UpdateInventory("Add", PlantedIngredient);
                PlantedIngredient = null;
                break;
        }
    }

    /// <summary>
    /// Makes the planted ingredient seed grow.
    /// </summary>
    /// <returns>The amount of time that it will take to harvest the item</returns>
    private IEnumerator GrowIngredient()
    {
        int harvestTime = PlantedIngredient.Ingredient.HarvestTime;
        int i = 0;
        while ( i < 3 )
        {
            yield return new WaitForSeconds( harvestTime );
            i++;
            gameObject.GetComponent<SpriteRenderer>().sprite = StatusSprite[i];
        }
        Status = "Ready";
    }
}
