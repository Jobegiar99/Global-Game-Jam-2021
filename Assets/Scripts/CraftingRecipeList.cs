using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CraftingRecipeList : MonoBehaviour
{
    /// <summary>
    /// List of all the recipes that exists in the game;
    /// </summary>
    public List<CraftingRecipe> Recipes;
    /// <summary>
    /// The object that is returned when the recipe does not exists
    /// </summary>
    public GameIngredient Trash;

    /// <summary>
    /// <para name = "ingredientA">The first ingredient of the crafting result</para>
    /// <para name = "ingredientB">The second ingredient of the crafting result</para>
    /// </summary>
    public GameIngredient CheckIfRecipeExists( InventoryIngredient ingredientA, InventoryIngredient ingredientB )
    {
        foreach( CraftingRecipe recipe in Recipes)
        {
            GameIngredient firstIngredient = recipe.FirstIngredient;
            GameIngredient secondIngredient = recipe.SecondIngredient;
            GameIngredient result = recipe.Result;
            if( firstIngredient == ingredientA && secondIngredient == ingredientB || firstIngredient == ingredientB && secondIngredient == ingredientA)
            {
                UpdateInventory(result);
                return result;
            }
        }
        return Trash;
    }


    /// <summary>
    /// Adds an item to the inventory if it does not exists, if it exists increases the quantity by one
    /// <para name = "result"> Object to add to the inventory if the crafting result is not the Trash object</para>
    /// </summary>
    public void UpdateInventory(GameIngredient result )
    {
        InventoryIngredient inventoryIngredientToAdd = new InventoryIngredient();
        inventoryIngredientToAdd.Ingredient = result;
        GameObject.Find("PlayerInventory").GetComponent<PlayerInventory>().UpdateInventory( "Add" , inventoryIngredientToAdd);
    }

    public bool CheckIfIngredientsFormPartOfARecipe( GameIngredient ingredientA, GameIngredient ingredientB)
    {
        foreach( CraftingRecipe recipe in Recipes)
        {
            GameIngredient firstIngredient = recipe.FirstIngredient;
            GameIngredient secondIngredient = recipe.SecondIngredient;
            if(( ingredientA == firstIngredient && ingredientB == secondIngredient) || ( ingredientB == firstIngredient && ingredientA == secondIngredient) )
            {
                return true;
            }
        }
        return false;
    }
}
