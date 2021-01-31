using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CraftingRecipeList : MonoBehaviour
{
    public System.Action<GameIngredientInfo> OnDiscover { get; set; }

    private void Start()
    {/*
        var inv = GameObject.FindObjectOfType<PlayerInventory>();
        this.ExecuteLater(() =>
        {
            TotalIngredients.ForEach(ing => inv.AddItem(ing.Info, 3));
        }, 1f);*/
    }

    /// <summary>
    /// List of all the recipes that exists in the game;
    /// </summary>
    public List<CraftingRecipe> Recipes;

    public List<GameIngredient> TotalIngredients { get; private set; } = new List<GameIngredient>();

    /// <summary>
    /// The object that is returned when the recipe does not exists
    /// </summary>
    public GameIngredientInfo Trash;

    /// <summary>
    /// <para name = "ingredientA">The first ingredient of the crafting result</para>
    /// <para name = "ingredientB">The second ingredient of the crafting result</para>
    /// </summary>
    public GameIngredientInfo CheckIfRecipeExists(GameIngredientInfo ingredientA, GameIngredientInfo ingredientB)
    {
        foreach (CraftingRecipe recipe in Recipes)
        {
            GameIngredientInfo firstIngredient = recipe.FirstIngredient.Info;
            GameIngredientInfo secondIngredient = recipe.SecondIngredient.Info;
            GameIngredientInfo result = recipe.Result.Info;
            if (firstIngredient == ingredientA && secondIngredient == ingredientB ||
                firstIngredient == ingredientB && secondIngredient == ingredientA)
            {
                OnDiscover?.Invoke(result);
                return result;
            }
        }
        return Trash;
    }

    public GameIngredient GetIngredient(GameIngredientInfo info)
    {
        return TotalIngredients.Find(el => el.Info == info); ;
    }

    /// <summary>
    /// Adds an item to the inventory if it does not exists, if it exists increases the quantity by one
    /// <para name = "result"> Object to add to the inventory if the crafting result is not the Trash object</para>
    /// </summary>
    public void UpdateInventory(GameIngredientInfo result)
    {
        InventoryIngredient inventoryIngredientToAdd = new InventoryIngredient(result);
        GameObject.Find("PlayerInventory").GetComponent<PlayerInventory>().UpdateInventory("Add", inventoryIngredientToAdd);
    }

    public CraftingRecipe FindRecipe(GameIngredient ingredient)
    {
        return FindRecipe(ingredient.Info);
    }

    public CraftingRecipe FindRecipe(GameIngredientInfo ingredient)
    {
        foreach (CraftingRecipe recipe in Recipes)
        {
            GameIngredientInfo result = recipe.Result.Info;
            if (result == ingredient) return recipe;
        }
        return null;
    }

    public bool CheckIfIngredientsFormPartOfARecipe(GameIngredientInfo ingredientA, GameIngredientInfo ingredientB)
    {
        foreach (CraftingRecipe recipe in Recipes)
        {
            GameIngredientInfo firstIngredient = recipe.FirstIngredient.Info;
            GameIngredientInfo secondIngredient = recipe.SecondIngredient.Info;
            if ((ingredientA == firstIngredient && ingredientB == secondIngredient) || (ingredientB == firstIngredient && ingredientA == secondIngredient))
            {
                return true;
            }
        }
        return false;
    }
}