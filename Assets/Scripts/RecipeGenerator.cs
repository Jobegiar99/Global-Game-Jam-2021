using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecipeGenerator : MonoBehaviour
{
    [SerializeField] private CraftingRecipeList RecipeList;
    public List<GameIngredientInfo> Ingredients;

    private List<GameIngredient> CurrentOptions;
    public GameIngredientInfo winIngredient;
    public PlayerInventory inventory;

    // Start is called before the first frame update
    private void Start()
    {
        //RecipeList = GameObject.Find("CraftingRecipeList").GetComponent<CraftingRecipeList>();
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
        int[] tierIngredientCount = new int[5] { 8, 7, 5, 3, 1 };
        while (Ingredients.Count > 0)
        {
            List<GameIngredient> nextOptions = new List<GameIngredient>();
            while (tierIngredientCount[tier - 1] > 0)
            {
                GameIngredientInfo ingredientA = CurrentOptions[Random.Range(0, CurrentOptions.Count)].Info;
                GameIngredientInfo ingredientB = CurrentOptions[Random.Range(0, CurrentOptions.Count)].Info;
                while (ingredientB == ingredientA)
                {
                    ingredientB = CurrentOptions[Random.Range(0, CurrentOptions.Count)].Info;
                }
                GameIngredientInfo result = Ingredients[Random.Range(0, Ingredients.Count)];

                if (tier != 5 && result == winIngredient) continue;

                bool exists = RecipeList.CheckIfIngredientsFormPartOfARecipe(ingredientA, ingredientB);
                if (!exists)
                {
                    CraftingRecipe newRecipe = new CraftingRecipe();
                    newRecipe.FirstIngredient = new GameIngredient(ingredientA, tier - 1);
                    newRecipe.SecondIngredient = new GameIngredient(ingredientB, tier - 1);
                    newRecipe.Result = new GameIngredient(result, tier);
                    RecipeList.TotalIngredients.Add(newRecipe.Result);
                    //newRecipe.Tier = tier;
                    nextOptions.Add(new GameIngredient(result));
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
        while (CurrentOptions.Count < 6)
        {
            GameIngredient ingredientToAdd = new GameIngredient(Ingredients[Random.Range(0, Ingredients.Count)]);
            if (winIngredient == ingredientToAdd.Info) continue;
            CurrentOptions.Add(ingredientToAdd);
            Ingredients.Remove(ingredientToAdd.Info);
            RecipeList.TotalIngredients.Add(ingredientToAdd);
            inventory.AddItem(ingredientToAdd.Info);
        }
    }
}