using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CraftingRecipeList : MonoBehaviour
{
    /// <summary>
    /// List of all the recipes that exists in the game;
    /// </summary>
    public List<GameObject> Recipes;
    /// <summary>
    /// The object that is returned when the recipe does not exists
    /// </summary>
    public GameObject Trash;

    /// <summary>
    /// <para name = "ingredientA">The first ingredient of the crafting result</para>
    /// <para name = "ingredientB">The second ingredient of the crafting result</para>
    /// </summary>
    public GameObject CheckIfRecipeExists( GameObject ingredientA, GameObject ingredientB )
    {
        foreach( GameObject recipe in Recipes)
        {
            GameObject firstIngredient = recipe.GetComponent<CraftingRecipe>().FirstIngredient;
            GameObject secondIngredient = recipe.GetComponent<CraftingRecipe>().SecondIngredient;
            GameObject result = recipe.GetComponent<CraftingRecipe>().Result;
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
    public void UpdateInventory( GameObject result )
    {

    }
}
