using System.Collections;
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

    public GameObject PlantedIngredient;
    private bool IsGrowing;

    public PlayerInventory Inventory;

    // Start is called before the first frame update
    private void Start()
    {
        Status = "Empty";
        PlantedIngredient = null;
        IsGrowing = false;
        Inventory = GameObject.Find("PlayerInventory").GetComponent<PlayerInventory>();
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
                break;

            case "Growing":
                if (!IsGrowing)
                {
                    IsGrowing = true;
                    StartCoroutine(GrowIngredient());
                }
                break;

            case "Ready":
                IsGrowing = false;
                Status = "Empty";
                Inventory.UpdateInventory("Add", PlantedIngredient);
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
        int harvestTime = PlantedIngredient.GetComponent<InventoryIngredient>().Ingredient.GetComponent<GameIngredient>().HarvestTime;
        int i = 0;
        while (i < 3)
        {
            yield return new WaitForSeconds(harvestTime);
            i++;
            gameObject.GetComponent<SpriteRenderer>().sprite = StatusSprite[i];
        }
        Status = "Ready";
    }
}