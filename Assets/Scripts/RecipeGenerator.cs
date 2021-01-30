using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class RecipeGenerator : MonoBehaviour
{
    private CraftingRecipeList RecipeList;
    public List<GameIngredient> Ingredients;
    private List<GameIngredient> CurrentOptions;

    // Start is called before the first frame update
    void Start()
    {
        RecipeList = GameObject.Find("CraftingRecipeList").GetComponent<CraftingRecipeList>();
        CurrentOptions = new List<GameIngredient>();
        CreateCombinations();
    }

    private void CreateCombinations()
    {
        ObtainFirstIngredients();
        GenerateRecipes();
    }

    private void GenerateRecipes()
    {
        int tier = 1;
        int[] tierIngredientCount = new int[5]{ 8, 7, 5, 3, 1 };
        while( Ingredients.Count > 0)
        {
            List<GameIngredient> nextOptions = new List<GameIngredient>();
            while (tierIngredientCount[tier - 1] > 0)
            {
                GameIngredient ingredientA = CurrentOptions[Random.Range(0, CurrentOptions.Count)];
                GameIngredient ingredientB = CurrentOptions[Random.Range(0, CurrentOptions.Count)];
                while (ingredientB == ingredientA)
                {
                    ingredientB = CurrentOptions[Random.Range(0, CurrentOptions.Count)];
                }
                GameIngredient result = Ingredients[Random.Range(0, Ingredients.Count)];
                
                bool exists = RecipeList.CheckIfIngredientsFormPartOfARecipe(ingredientA, ingredientB);
                if (!exists)
                {
                    CraftingRecipe newRecipe = ScriptableObject.CreateInstance<CraftingRecipe>();
                    newRecipe.FirstIngredient = ingredientA;
                    newRecipe.SecondIngredient = ingredientB;
                    newRecipe.Result = result;
                    newRecipe.Tier = tier;
                    nextOptions.Add(result);
                    Ingredients.Remove(result);
                    RecipeList.Recipes.Add(newRecipe);
                    tierIngredientCount[tier - 1]--;
                }
            }
            tier += 1;
            CurrentOptions = nextOptions;
        }
    }

    private void ObtainFirstIngredients()
    {
        while( CurrentOptions.Count < 6 )
        {
            GameIngredient ingredientToAdd = Ingredients[Random.Range(0, Ingredients.Count)];
            CurrentOptions.Add( ingredientToAdd );
            Ingredients.Remove(ingredientToAdd);
            
        }
    }
}
 