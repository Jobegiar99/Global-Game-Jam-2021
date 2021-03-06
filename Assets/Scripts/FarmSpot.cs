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

    public PlayerInventory Inventory;
    public GameIngredientInfo PlantedIngredient;

    // Start is called before the first frame update
    private void Start()
    {
        Status = "Empty";
        PlantedIngredient = null;
        Inventory = GameObject.Find("PlayerInventory").GetComponent<PlayerInventory>();
    }

    /// <summary>
    /// Handles the player clicking on the spot
    /// </summary>
    public void OnMouseDown()
    {/*
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
                //Inventory.UpdateInventory("Add", PlantedIngredient);
                PlantedIngredient = null;
                break;
        }*/
    }

    /// <summary>
    /// Makes the planted ingredient seed grow.
    /// </summary>
    /// <returns>The amount of time that it will take to harvest the item</returns>
    private IEnumerator GrowIngredient()
    {
        int harvestTime = PlantedIngredient.HarvestTime;
        int i = 0;
        while (i < 3)
        {
            yield return new WaitForSeconds(harvestTime);
            i++;
            gameObject.GetComponent<SpriteRenderer>().sprite = StatusSprite[i];
        }
        Status = "Ready";
    }

    public System.Action<GameIngredientInfo> OnPlant;
    public System.Action<GameIngredientInfo, int> OnGrowFinish;
    public System.Action<GameIngredientInfo, int> OnCollect;
    public bool growing = false;

    public void Plant(GameIngredientInfo ingredient)
    {
        if (growing || PlantedIngredient != null) return;
        PlantedIngredient = ingredient;
        growing = true;
        OnPlant?.Invoke(ingredient);
        Inventory.RemoveItem(ingredient);
        StartCoroutine(Grow());
    }

    private IEnumerator Grow()
    {
        yield return new WaitForSeconds(PlantedIngredient.HarvestTime);
        OnGrowFinish?.Invoke(PlantedIngredient, PlantedIngredient.HarvestAmount);
        growing = false;
    }

    public void Collect()
    {
        if (growing || PlantedIngredient == null) return;
        OnCollect?.Invoke(PlantedIngredient, PlantedIngredient.HarvestAmount);
        Inventory.AddItem(PlantedIngredient, PlantedIngredient.HarvestAmount);
        PlantedIngredient = null;
    }
}