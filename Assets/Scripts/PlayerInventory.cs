using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utilities.UI.Animation;

public class PlayerInventory : MonoBehaviour
{
    /// <summary>
    /// List of all the ingredients that the player has.
    /// </summary>
    private List<InventoryIngredient> PlayerIngredients = new List<InventoryIngredient>();

    public Dictionary<GameIngredientInfo, int> Items { get; set; } = new Dictionary<GameIngredientInfo, int>();

    public System.Action<GameIngredientInfo, int> OnAddItem { get; set; }

    public System.Action<GameIngredientInfo> OnRemoveItem { get; set; }

    public UIAnimator lostScreen;
    public UIAnimator winScreen;

    public GameIngredientInfo winIngredient;

    private void Start()
    {
    }

    public void AddItem(GameIngredientInfo ingredient, int amount = 1)
    {
        if (Items.ContainsKey(ingredient)) Items[ingredient] += amount;
        else Items.Add(ingredient, amount);
        OnAddItem?.Invoke(ingredient, amount);
        if (ingredient == winIngredient) { winScreen.Show(); }
    }

    public List<FarmSpot> farms;
    public CraftingUI crafting;

    public void RemoveItem(GameIngredientInfo ingredient)
    {
        Items[ingredient]--;
        if (Items[ingredient] <= 0) Items.Remove(ingredient);
        OnRemoveItem?.Invoke(ingredient);

        TestLost();
    }

    public void TestLost()
    {
        int farmCount = 0;
        farms.ForEach(farm =>
        {
            if (farm.PlantedIngredient != null) { farmCount++; }
        });

        int craftCount = 0;
        if (crafting.slot1.ingredient != null) craftCount++;
        if (crafting.slot2.ingredient != null) craftCount++;
        if (crafting.collectSlot.ingredient != null) craftCount++;

        if ((Items.Count + farmCount + craftCount) <= 1) { lostScreen.Show(); }
    }

    public int Amount(GameIngredientInfo ingredient)
    {
        if (Items.ContainsKey(ingredient)) return Items[ingredient];
        return 0;
    }

    /// <summary>
    ///Handles changes to the inventory
    ///<para name = "action">Action to be done in the inventory</para>
    ///<para name = "actionIngredient">The ingredient which will suffer changes</para>
    /// </summary>
    public void UpdateInventory(string action, InventoryIngredient actionIngredient)
    {
        switch (action)
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
    private void AddItem(string action, InventoryIngredient actionIngredient)
    {
        bool ingredientExists = PlayerIngredients.Contains(actionIngredient);
        if (ingredientExists)
        {
            PlayerIngredients[PlayerIngredients.IndexOf(actionIngredient)].ModifyQuantity(action, 1);
        }
        else
        {
            PlayerIngredients.Add(actionIngredient);
        }
    }

    /// <summary>
    ///Removes an item from the inventory.
    ///<para name = "action">Action to be done in the inventory</para>
    ///<para name = "actionIngredient">The ingredient which will suffer changes</para>
    /// </summary>
    private void RemoveItem(InventoryIngredient actionIngredient)
    {
        bool ingredientExists = PlayerIngredients.Contains(actionIngredient);
        if (ingredientExists)
        {
            PlayerIngredients.Remove(actionIngredient);
        }
    }
}