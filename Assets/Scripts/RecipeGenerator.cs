using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class RecipeGenerator : MonoBehaviour
{
    public CraftingRecipeList RecipeList;
    public List<GameIngredient> Ingredients;
    private List<GameIngredient> AvailableIngredients;
    private GameIngredient FirstIngredient;
    private GameIngredient SecondIngredient;
    private GameIngredient ThirdIngredient;

    // Start is called before the first frame update
    void Start()
    {
        RecipeList = GameObject.Find("CraftingRecipeList").GetComponent<CraftingRecipeList>();
        FirstIngredient = null;
        SecondIngredient = null;
        ThirdIngredient = null;
        CreateCombinations();
    }

    private void CreateCombinations()
    {
        ObtainFirstThreeIngredients();
        GenerateRecipes();
    }

    private void GenerateRecipes()
    {
        while( Ingredients.Count > 0)
        {
            GameIngredient ingredientA = AvailableIngredients[Random.Range(0, AvailableIngredients.Count)];
            GameIngredient ingredientB = AvailableIngredients[Random.Range(0, AvailableIngredients.Count)];
            while(ingredientB == ingredientA)
            {
                ingredientB = AvailableIngredients[Random.Range(0, AvailableIngredients.Count)];
            }
            GameIngredient result = Ingredients[Random.Range(0, Ingredients.Count)];
            bool exists = RecipeList.CheckIfIngredientsFormPartOfARecipe(ingredientA, ingredientB);
            if( !exists )
            {
                CraftingRecipe newRecipe = new CraftingRecipe();
                newRecipe.FirstIngredient = ingredientA;
                newRecipe.SecondIngredient = ingredientB;
                AvailableIngredients.Add(result);
                Ingredients.Remove(result);
                RecipeList.Recipes.Add(newRecipe);
            }   
        }
    }

    private void ObtainFirstThreeIngredients()
    {
        FirstIngredient = Ingredients[Random.Range(0, Ingredients.Count)];
        SecondIngredient = FirstIngredient;
        while (SecondIngredient == FirstIngredient)
        {
            SecondIngredient = Ingredients[Random.Range(0, Ingredients.Count)];
        }
        ThirdIngredient = SecondIngredient;
        while (ThirdIngredient == SecondIngredient)
        {
            ThirdIngredient = Ingredients[Random.Range(0, Ingredients.Count)];
        }
        AvailableIngredients.Add(FirstIngredient);
        Ingredients.Remove(FirstIngredient);
        AvailableIngredients.Add(SecondIngredient);
        Ingredients.Remove(SecondIngredient);
        AvailableIngredients.Add(ThirdIngredient);
        Ingredients.Remove(ThirdIngredient);  
    }
}
